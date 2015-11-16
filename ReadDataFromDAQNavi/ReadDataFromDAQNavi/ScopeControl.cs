/* 
 * Copyright (c)2007-2008, Dustin Spicuzza
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * The name of the Dustin Spicuzza may not be used to endorse or promote 
 *       products derived from this software without specific prior written 
 *       permission.
 *
 * THIS SOFTWARE IS PROVIDED BY Dustin Spicuzza ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL Dustin Spicuzza BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;


namespace ReadDataFromDAQNavi {

	internal delegate void VoidInvoke();

	/// <summary>
	/// Oscilloscope drawing control. :) 
	/// </summary>
	public partial class ScopeControl : UserControl {

		// variables needed for drawing stuff
		Graphics realGraphic = null;

		Pen backPen;
		Pen gridBackPen;
		Pen gridPen;

		RectangleF gridLocation;

		// background image
		Bitmap bgImage;

		// image currently being displayed
		Bitmap currentScopeImage;

		PointF zeroPosition;

		// where to align the controls on the left.. or something like that.
		float leftAlign = 10;

		Stopwatch traceTime = new Stopwatch();

		// locks
		// don't allow the timer to reenter itself
		object timerLock = new object();

		// this locks starting, stopping, and adding points
		object traceLock = new object();


		/// <summary>
		/// Constructor the ScopeControl
		/// </summary>
		public ScopeControl() {

			// these reduce flicker, which is definitely a good thing
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);

			// these initializations don't matter at this point
			zeroPosition = new PointF(0, 0);

			// initialize pens
			backPen = new Pen(BackColor);
			gridBackPen = new Pen(gridBackColor);
			gridPen = new Pen(gridColor);

			// initialize event handler
			Traces.OnChange += new ListChangedEventHandler(Traces_OnChange);

			InitializeComponent();

			InitTimeDivision();

		}

		/// <summary>
		/// Called anytime a trace is added/removed
		/// </summary>
		void Traces_OnChange(object sender, ListChangedEventArgs e) {

			if ( InvokeRequired ) {
				Invoke(new ListChangedEventHandler(Traces_OnChange), sender, e);
				return;
			}

			switch ( e.ListChangedType ) {

				case ListChangedType.ItemDeleted:
					Controls.Remove(Traces[e.NewIndex].traceLabel);
					Traces[e.NewIndex].OnChange -= new VoidInvoke(ReinitializeScopeControl);
					break;

				case ListChangedType.ItemAdded:
					Controls.Add(Traces[e.NewIndex].traceLabel);
					Traces[e.NewIndex].OnChange += new VoidInvoke(ReinitializeScopeControl);
					break;
			}

			// reposition anything that needs to be repositioned
			ReinitializeScopeControl();

		}

		private void Scope_Load(object sender, EventArgs e) {

			realGraphic = this.CreateGraphics();
			ReinitializeScopeControl();

		}

		/// <summary>
		/// Call this to begin sending points to the scope. This flushes the internal
		/// buffers and begins drawing traces from t=0
		/// </summary>
		public void Start() {
			
			lock ( traceLock ) {

				if ( IsRunning )
					return;

				foreach ( Trace trace in Traces )
					trace.TracePoints.Clear();
			}

			Interlocked.Exchange(ref isRunning, 1);

			historyScrollBar.Visible = false;

			traceTime.Reset();
			traceTime.Start();
			drawTimer.Start();
		}

		/// <summary>
		/// Call this to signal the end of incoming points to the scope
		/// </summary>
		public void Stop() {

			Interlocked.Exchange(ref isRunning, 0);

			lock ( traceLock ) {
				drawTimer.Stop();
				traceTime.Stop();
			}

			if ( enableHistory && Traces.Count > 0) {
				// this needs to be thought through some more, I think
				// note: the scrollbar can never get to its maximum position, its always
				// maximum - largechange

				// this depends on the count - itemwindow being positive
				// TODO: This needs to be fixed, badly!!
				try {
					historyScrollBar.Maximum = historyScrollBar.LargeChange + Traces[0].TracePoints.Count - Traces[0].TraceItemWindow;
					historyScrollBar.Value = historyScrollBar.Maximum - historyScrollBar.LargeChange;
					historyScrollBar.Visible = true;
				} catch {
					// screw it. 
				}
			}
		}



		private void drawTimer_Tick(object sender, EventArgs e) {

			// don't let calls overlap -- yes, this can happen
			lock ( timerLock ) {
				DrawTraces();
			}
		}

		/// <summary>
		/// Call this to draw the trace(s) that are currently enabled
		/// </summary>
		private void DrawTraces() {


			Graphics bufferGraphic;
			int i,j;
			PointF[] points;

			long currentMilliseconds;
			int tracePointsCount = 0;

			double usPerPoint;
			int calculatedPoints;
			int pointsToPlot;		// points that should be plotted
			int maxPointsToPlot;	// ensure this doesn't exceed that number
			int bufferWindow;		// the range inside the buffer that will be plotted 
			int bufferIncrement;	// the number of items that should be skipped

			float traceYmult;		// constants per trace, calculate only once instead of many times
			float traceYaddend;		

			float incrementX;
			float X;


			lock ( traceLock ) {

				// milliseconds should be good enough
				currentMilliseconds = traceTime.ElapsedMilliseconds;

				// get count under lock
				if ( Traces.Count > 0 )
					tracePointsCount = Traces[0].TracePoints.Count;
			}

			if ( currentScopeImage != null )
				currentScopeImage.Dispose();

			currentScopeImage = new Bitmap(bgImage);

			bufferGraphic = Graphics.FromImage(currentScopeImage);

			if ( Traces == null )
				return;

			// don't draw too many points, skip some in that case
			maxPointsToPlot = (int)(gridLocation.Width * 2);

			for ( j = 0; j < Traces.Count; j++ ) {

				Trace trace = Traces[j];

				if ( j == 0 )
					lblSamples.Text = tracePointsCount.ToString() + " / " + currentMilliseconds.ToString() + " = " + ( (double)tracePointsCount / (double)currentMilliseconds ).ToString("f");

				if ( !trace.Visible && trace.TraceItemWindow > 0 )
					continue;

				// if there are no points, theres nothing to draw
				if ( tracePointsCount < 1 )
					continue;

				// figure out what needs to be drawn, and scale it properly

				// the challenge here is that we should draw *something* at all times
				// and that the something should always stretch across the whole screen

				// another problem is that for some reason the tracing freezes when we're
				// drawing, and that needs to be fixed too


				// find the number of microseconds per point -- for obvious reasons
				// we're going to be doing quite horribly at microsecond resolution, but
				// it will have to be good enough
				usPerPoint = ( currentMilliseconds * 1000 ) / tracePointsCount;

				// find out how many points we could plot
				calculatedPoints =
					pointsToPlot =
					bufferWindow = (int)Math.Ceiling(( microSecondsPerUnit * UnitsX ) / usPerPoint);

				// this is always left justified
				if ( pointsToPlot >= tracePointsCount )
					pointsToPlot = bufferWindow = tracePointsCount;
				else
					// otherwise, move this over one
					pointsToPlot = bufferWindow += 1;


				// if its too many, then we need to adjust it
				if ( pointsToPlot > maxPointsToPlot ) {

					// adjust the buffer increment value
					bufferIncrement = (int)Math.Ceiling((double)calculatedPoints / maxPointsToPlot);

					// set this to the max
					pointsToPlot = (int)Math.Ceiling((double)pointsToPlot / bufferIncrement);

				} else {
					bufferIncrement = 1;
				}

				incrementX = (float)( ( gridLocation.Width * bufferIncrement ) / calculatedPoints );

				// find the starting X value
				X = gridLocation.Left;// -someOffset;

				if ( tracePointsCount < bufferWindow )
					throw new Exception("Hmm");

				// if its running 
				if ( isRunning == 1) {
					trace.TraceStartIndex = tracePointsCount - bufferWindow;
                    trace.TraceEndIndex = tracePointsCount;

					// or if the time window isn't correct
				} else if ( trace.TraceItemWindow != bufferWindow ) {

					// expand/shrink to the right, its more intuitive
					trace.TraceEndIndex = bufferWindow + trace.TraceStartIndex;

					// if its too big, then shift the window to the left
					if ( trace.TraceEndIndex > tracePointsCount ) {
						trace.TraceEndIndex = tracePointsCount;
						trace.TraceStartIndex = trace.TraceEndIndex - bufferWindow;

						// ensure that it wasn't too small 
						if ( trace.TraceStartIndex < 0 )
							trace.TraceStartIndex = 0;
					}
				}

				// these save time on calculations
				traceYmult = ( -1000F * unitLength ) / trace.MilliPerUnit;
				traceYaddend = zeroPosition.Y + trace.ZeroPositionY * unitLength;

				if ( pointsToPlot < 2 ) {

					// if theres only one point to draw, then draw a straight line, its
					// a pretty good approximation. :p

					points = new PointF[2];
					points[0].X = gridLocation.Left;
					points[1].X = gridLocation.Right;
					points[0].Y = points[1].Y = DrawTraces_ClipY(trace.TracePoints[trace.TraceEndIndex - 1] * traceYmult + traceYaddend);

				} else {

					// points to plot should always be at least two
					points = new PointF[pointsToPlot];


					int k = 0;

					for ( i = trace.TraceStartIndex; i < trace.TraceEndIndex; i += bufferIncrement ) {

						points[k].X = X;
						points[k].Y = trace.TracePoints[i] * traceYmult + traceYaddend;

						// TODO: sometimes we interpolate the first value (not values!)
						// TODO: not sure if this actually works, it may not
						if ( points[k].X < gridLocation.Left ) {

							// we have to calculate this again at the next iteration, but I can live with that
							float y1 = trace.TracePoints[i + bufferIncrement] * traceYmult + traceYaddend;

							// y0 = (y1-y0)*((x1-z)/(x1-x0))
							points[k].Y = ( y1 - points[k].Y ) *
								( ( X + incrementX - gridLocation.Left ) /
								 ( incrementX ) );

							points[k].X = gridLocation.Left;
						}

						// ensure valid Y values
						points[k].Y = DrawTraces_ClipY(points[k].Y);

						k += 1;
						X += incrementX;
					}
				}

				// this should not fail, most of the time
				if ( points.Length > 1 )
					bufferGraphic.DrawCurve(trace.Pen, points);
			}
			

			// draw the scope on the control
			if ( this.ParentForm != null ) {
				
				if ( realGraphic == null )
					realGraphic = this.CreateGraphics();

				realGraphic.DrawImage(currentScopeImage, 0, 0);

			} else if (realGraphic != null ){
				realGraphic.Dispose();
				realGraphic = null;
			}

			bufferGraphic.Dispose();



            // draw the left time buffer (TODO: this needs to be refined)
            timeLeftLabel.Visible = false;
            timeRightLabel.Visible = false;
            if ( tracePointsCount != 0 ) {

				// left side first
				double leftSide = ( (double)( Traces[0].TraceStartIndex * currentMilliseconds ) / 1000 ) / ( (double)tracePointsCount );
				timeLeftLabel.Text = String.Format("{0:0.###}s", leftSide);

				// right side second
				timeRightLabel.Text = String.Format("{0:0.###}s",
					leftSide + ( (double)(microSecondsPerUnit * UnitsX)) / 1000000.0);
				timeRightLabel.Left = (int)gridLocation.Right - timeRightLabel.Width;

			}else{
				timeLeftLabel.Text = "";
				timeRightLabel.Text = "";
			}
		}

		/// <summary>
		/// Don't call this from anywhere except DrawTraces. Ensures the Y
		/// value is valid. 
		/// </summary>
		/// <param name="y"></param>
		/// <returns></returns>
		private float DrawTraces_ClipY(float y) {
			if (y < gridLocation.Top)
				return gridLocation.Top;
			
			if (y > gridLocation.Bottom)
				return gridLocation.Bottom;

			if (float.IsNaN(y))
				return zeroPosition.Y;

			return y;
		}

		/// <summary>
		/// This recalculates all of the grid parameters whenever one of the attributes
		/// have been changed. You should only write the grid parameters through the
		/// associated properties, otherwise things get out of sync. 
		/// </summary>
		private void recalculateGridParameters() {

			unitLength = Math.Max(0,gridLocation.Height / unitsY);
			zeroPosition.Y = ( UnitsY / 2 ) * unitLength + gridLocation.Top;
			zeroPosition.X = ( UnitsX / 2 ) * unitLength + gridLocation.Left;

			ReinitializeScopeControl();
		}

		/// <summary>
		/// Repositions controls on the form
		/// </summary>
		void ReinitializeScopeControl() {

			// do this first
			CreateControlBackground();

			timeUnitLabel.Font = Font;
			timeUnitLabel.ForeColor = ForeColor;
			timeUnitLabel.BackColor = BackColor;

			// position the history scrollbar
			historyScrollBar.Top = (int)Math.Ceiling(gridLocation.Bottom) + 3;
			historyScrollBar.Left = (int)Math.Ceiling(gridLocation.Left);
			historyScrollBar.Width = (int)Math.Ceiling(gridLocation.Width);
			historyScrollBar.Height = timeUnitLabel.Height;

			// position the trace controls and such
			int spacer = historyScrollBar.Height / 4;
			int nextLeft = historyScrollBar.Left;
			int nextTop = historyScrollBar.Top + historyScrollBar.Height + spacer;
			int rightLimit;

			// first, position the time
			timeUnitLabel.Top = nextTop;
			timeUnitLabel.Left = historyScrollBar.Right - timeUnitLabel.Width;
			rightLimit = timeUnitLabel.Left - spacer;

			for ( int i = 0; i < Traces.Count; i++ ) {

				Trace td = Traces[i];

				Label label = td.traceLabel;

				if (td.MilliPerUnit < 500)
					label.Text = String.Format("Ch{0}: {1}m{2}", i, td.MilliPerUnit, td.UnitName);
				else
					label.Text = String.Format("Ch{0}: {1:.###}{2}", i, ((double)td.MilliPerUnit)/1000, td.UnitName);

				if ( td.Visible )
					label.BackColor = td.TraceColor;
				else
					label.BackColor = Color.Gray;

				label.ForeColor = BackColor;
				label.Font = Font;

				if ( nextLeft + label.Width > rightLimit ) {
					nextTop += label.Height + spacer;
					nextLeft = historyScrollBar.Left;
				}

				label.Top = nextTop;
				label.Left = nextLeft;

				nextLeft = label.Right + spacer;
			}

			DrawTraces();

		}

		/// <summary>
		/// This creates the background bitmap and scales it properly. Only
		/// call this from ReinitializeScopeControl
		/// </summary>
		private void CreateControlBackground() {

			Graphics graphic;

			if ( realGraphic == null || Width < 1 || Height < 1)
				return;

			// create the background image for it, setup buffers and such
			bgImage = new Bitmap((int)Width,(int)Height);
		
			// TODO: add margins, text, and such
			graphic = Graphics.FromImage(bgImage);

			// control background
			graphic.FillRectangle(backPen.Brush, this.ClientRectangle);

			// grid background
			graphic.FillRectangle(gridBackPen.Brush, gridLocation.X, gridLocation.Y, gridLocation.Width + 1, gridLocation.Height + 1);

			// grid border
			graphic.DrawRectangle(gridPen, gridLocation.X - 1, gridLocation.Y - 1, gridLocation.Width + 2, gridLocation.Height + 2);

			// draw the grid
			if ( gridVisible)
				DrawGrid(graphic, gridLocation, gridPen);
			
			// finish this off, don't need it anymore
			graphic.Dispose();

			// draw any traces that need to be drawn
			DrawTraces();
		}

		/// <summary>
		/// Draws a grid, starting from the center
		/// </summary>
		private void DrawGrid(Graphics graphic, RectangleF location, Pen pen) {

			int  xunits, yunits, ix, iy, k;
			float x, y, xstart, t, xt, yt;

			bool AtTopEdge, AtBottomEdge, AtLeftEdge, AtRightEdge, AtCenterX, AtCenterY;

			// dash length
			float majorHalfLength = 4, minorHalfLength = 2;
            
			// dot size
			float dotW = 1, dotH = 1, dotX = 0.5F, dotY = 0.5F;

			// draw X direction, except for the edges and center
			
			// Xunits 
			xunits = (int)Math.Ceiling(( zeroPosition.X - location.Left ) / unitLength);
			yunits = (int)Math.Ceiling(( zeroPosition.Y - location.Top ) / unitLength);

			// X starting position
			xstart = zeroPosition.X - xunits * unitLength;
			y = zeroPosition.Y - yunits * unitLength;

			t = unitLength / 5;
			iy = 0;

			// draw from the top left corner.. 
			while ( iy <= yunits * 2 ) {

				// more efficient to calculate this here
				AtTopEdge = y <= location.Top + majorHalfLength ? true : false;
				AtBottomEdge = y >= location.Bottom - majorHalfLength ? true : false;
				AtCenterY = iy == yunits;


				ix = 0;
				x = xstart;
				while ( ix <= xunits * 2 ) {

					// more efficient to calculate this here
					AtLeftEdge = x <= location.Left + majorHalfLength ? true : false;
					AtRightEdge = x >= location.Right - majorHalfLength ? true : false;
					AtCenterX = ix == xunits;

					// draw minor points in Y direction
					for ( k = 0; k < 5; k++ ) {

						// value of y for this increment
						yt = y + t * k;

						// skip certain values
                        if ((k == 0 && AtCenterY && !AtCenterX && !AtRightEdge && !AtLeftEdge) || yt < location.Top || yt > location.Bottom)
							continue;

						// left lines
                        else if (AtLeftEdge){
							if ( k == 0 )
								graphic.DrawLine(pen, location.Left, yt, location.Left + majorHalfLength, yt);
							else
								graphic.DrawLine(pen, location.Left, yt, location.Left + minorHalfLength, yt);
						
						// right lines
						}else if (AtRightEdge){
							if ( k == 0 )
								graphic.DrawLine(pen, location.Right - majorHalfLength, yt, location.Right, yt);
							else
								graphic.DrawLine(pen, location.Right - minorHalfLength, yt, location.Right, yt);

						// middle lines
						} else if (AtCenterX) {
							if (k == 0)
								graphic.DrawLine(pen, x - majorHalfLength, yt, x + majorHalfLength, yt);
							else
								graphic.DrawLine(pen, x - minorHalfLength, yt, x + minorHalfLength, yt);

						// anything else
						} else
							graphic.DrawEllipse(pen, x - dotX, yt - dotY, dotW, dotH);

					}


					// draw minor points in X direction
					for ( k = 0; k < 5; k++ ) {

						// value of x for this increment
						xt = x + t * k;

						// skip certain values
						if ((k == 0 && AtCenterX && !AtCenterY && !AtTopEdge && !AtBottomEdge) || xt < location.Left || xt > location.Right)
							continue;

						// top lines
						else if (AtTopEdge) {

							if (k == 0)
								graphic.DrawLine(pen, xt, location.Top, xt, location.Top + majorHalfLength);
							else
								graphic.DrawLine(pen, xt, location.Top, xt, location.Top + minorHalfLength);

						// bottom lines
						}else if (AtBottomEdge) {

							if (k == 0)
								graphic.DrawLine(pen, xt, location.Bottom - majorHalfLength, xt, location.Bottom);
							else
								graphic.DrawLine(pen, xt, location.Bottom - minorHalfLength, xt, location.Bottom);

						// middle line
						}else if (AtCenterY) {

							if ( k == 0 )
								graphic.DrawLine(pen, xt, y - majorHalfLength, xt, y + majorHalfLength);
							else
								graphic.DrawLine(pen, xt, y - minorHalfLength, xt, y + minorHalfLength);

						// anything else
						} else
							graphic.DrawEllipse(pen, xt - dotX, y - dotY, dotW, dotH);

					}

					// next X line
					x += unitLength;
					ix += 1;
				}

				// next Y line
				y += unitLength;
				iy += 1;
			}

		}

		// this needs to be far more intelligent than it is currently
		private void SetGridLocation() {

			timeRightLabel.Top = timeLeftLabel.Top = (int)leftAlign/2;
			timeLeftLabel.Left = (int)leftAlign;
			timeRightLabel.Left = (int)gridLocation.Right - timeRightLabel.Width;

			float b = timeLeftLabel.Bottom + leftAlign / 2;
			gridLocation = new RectangleF(leftAlign, b, Width -20, Height - b - (3 + timeLeftLabel.Height * 4));
			recalculateGridParameters();
		}

		/// <summary>
		/// Called on resize
		/// </summary>
		protected override void OnResize(EventArgs e) {

			Graphics old = realGraphic;

			// need to get a different graphics handle
			realGraphic = this.CreateGraphics();

			// get rid of the old one
			if (old != null)
				old.Dispose();
			
			SetGridLocation();
			ReinitializeScopeControl();

		}

		/// <summary>
		/// Called to paint
		/// </summary>
		protected override void OnPaint(PaintEventArgs e) {
			if ( currentScopeImage != null && realGraphic != null )
				//realGraphic.DrawImage(currentScopeImage, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
				e.Graphics.DrawImageUnscaled(currentScopeImage, 0, 0);
		}

		#region Overridden control default properties


		/// <summary>
		/// Gets or sets the primary background color of control
		/// </summary>
		public override Color BackColor {
			get { return base.BackColor; }
			set {
				base.BackColor = value;

				if ( backPen != null )
					backPen.Dispose();
				backPen = new Pen(value);

				ReinitializeScopeControl();
			}
		}


		/// <summary>
		/// Gets or sets the primary text color of control
		/// </summary>
		public override Color ForeColor {
			get {
				return base.ForeColor;
			}
			set {
				base.ForeColor = value;
				ReinitializeScopeControl();
			}
		}

		/// <summary>
		/// Gets or sets the primary font for the control
		/// </summary>
		public override Font Font {
			get {
				return base.Font;
			}
			set {
				base.Font = value;
				SetGridLocation();
			}
		}

		#endregion


		#region Properties

		Color gridBackColor = Color.Black;

		/// <summary>
		/// Background color of the grid
		/// </summary>
		[DefaultValue(typeof(Color), "Black")]
		public Color GridBackColor {
			get { return gridBackColor; }
			set { 
				gridBackColor = value;

				if ( gridBackPen != null )
					gridBackPen.Dispose();
				gridBackPen = new Pen(value);

				CreateControlBackground();
			}
		}


		Color gridColor = Color.White;

		/// <summary>
		/// Color of major grid lines
		/// </summary>
		[DefaultValue(typeof(Color), "GreenYellow")] 
		public Color GridColor {
			get { return gridColor; }
			set { 
				gridColor = value;

				if ( gridPen != null )
					gridPen.Dispose();
				gridPen = new Pen(value);

				CreateControlBackground();
			}
		}

		int microSecondsPerUnit = 100000;

		/// <summary>
		/// How many microseconds are represented by an X unit
		/// </summary>
		[DefaultValue(100000)]
		public int MicroSecondsPerUnit {
			get { return microSecondsPerUnit; }
			set { 
				microSecondsPerUnit = value;
				InitTimeDivision();
			}
		}

		private void InitTimeDivision() {

			if ( InvokeRequired ) {
				Invoke(new VoidInvoke(InitTimeDivision));
				return;
			}

			if ( microSecondsPerUnit < 1000 )
				timeUnitLabel.Text = microSecondsPerUnit.ToString() + "\u00B5" + "s";
			else if ( microSecondsPerUnit < 1000000 )
				timeUnitLabel.Text = ( microSecondsPerUnit / 1000 ).ToString() + "ms";
			else
				timeUnitLabel.Text = ( microSecondsPerUnit / 1000000 ).ToString() + "s";

            timeUnitLabel.Visible = false;
			// redraw the traces
			DrawTraces();

			ReinitializeScopeControl();
		}


		float gridSpacing = 1;

		/// <summary>
		/// Number of units major gridlines are spaced at
		/// </summary>
		[DefaultValue(1)]
		public float GridSpacing{
			get { return gridSpacing; }
			set { 
				gridSpacing = value; 
				CreateControlBackground(); 
			}
		}

		bool gridVisible = true;

		/// <summary>
		/// Set this to true to show major grid lines
		/// </summary>
		[DefaultValue(true)]
		public bool GridVisible {
			get { return gridVisible; }
			set { 
				gridVisible = value; 
				CreateControlBackground(); 
			}
		}
		

		float unitLength = 1;

		float unitsY = 8;

		/// <summary>
		/// Gets or Sets the number of vertical units that are drawn by the scope. This should be a multiple of two.
		/// </summary>
		[DefaultValue(8)]
		public float UnitsY {
			get { return unitsY; }
			set { 
				unitsY = value;
				recalculateGridParameters();
			}
		}

		/// <summary>
		/// Gets the number of units across that are drawn by the scope
		/// </summary>
		[ReadOnly(true)]
		public float UnitsX {
			get {
				if ( unitLength == 0 )
					return 0;
				return gridLocation.Width / unitLength;
			}
		}

		bool enableHistory = true;

		/// <summary>
		/// Enables the ability to go back and forth with the last
		/// oscope trace
		/// </summary>
		[DefaultValue(true)]
		public bool EnableHistory {
			get { return enableHistory; }
			set { 
				enableHistory = value;
				ReinitializeScopeControl();
			}
		}

		private int isRunning = 0;

		/// <summary>
		/// This is set to true when the scope is "running", ie., you can call addpoints
		/// </summary>
		[ReadOnly(true)]
		public bool IsRunning {
			get { return isRunning == 1; }
		}


		#endregion


		//private void ScopeControl_MouseMove(object sender, MouseEventArgs e) {
		//	locLabel.Text = e.X.ToString() + ", " + e.Y.ToString();
			
		//}

		// makes the history function work
		private void historyScrollBar_ValueChanged(object sender, EventArgs e) {

			Trace t = Traces[0];
			int val = historyScrollBar.Value;

			// TODO: Validate this function

			foreach ( Trace td in Traces ) {

				// the order matters! this one should be first
				td.TraceEndIndex = val + t.TraceItemWindow;

				if ( td.TraceEndIndex > td.TracePoints.Count )
					td.TraceEndIndex = td.TracePoints.Count;

				td.TraceStartIndex = val;

				if ( td.TraceStartIndex > td.TraceEndIndex )
					td.TraceStartIndex = td.TraceEndIndex - 1;

			}

			timeLeftLabel.Text = val.ToString() + " " + historyScrollBar.Maximum.ToString();

			DrawTraces();
		}

		#region Trace manipulation

		// Note: having this as a property causes problems
		/// <summary>
		/// This is an array of all traces, and their attributes
		/// </summary>
		public TraceCollection Traces = new TraceCollection();

		List<float> tempAddPoint = new List<float>();

		// the point of this is to ensure that all points have
		// the same number of nodes in them. This makes a lot of 
		// things easier. 


		/// <summary>
		/// Begins an 'addpoint' operation
		/// </summary>
		public void BeginAddPoint() {
			if ( tempAddPoint.Count != Traces.Count ) {
				tempAddPoint = new List<float>(Traces.Count);
				for ( int i = 0; i < Traces.Count; i++ )
					tempAddPoint.Add(0);

			} else
				for ( int i = 0; i < Traces.Count; i++ )
					tempAddPoint[i] = 0;
		}

		/// <summary>
		/// Add a point related to a specific trace to the display. This should be 
		/// preceded with BeginAddPoint and ended with EndAddPoint
		/// </summary>
		/// <param name="traceIndex"></param>
		/// <param name="point"></param>
		public void AddPoint(int traceIndex, float point) {
			tempAddPoint[traceIndex] = point;
		}

		/// <summary>
		/// Commits the last set of points added with addpoint
		/// </summary>
		public void EndAddPoint() {
			lock ( traceLock ) {
				if ( isRunning == 1)
					for ( int i = 0; i < tempAddPoint.Count; i++ )
						Traces[i].TracePoints.Add(tempAddPoint[i]);
			}
		}


		/// <summary>
		/// Add data to all traces simultaeneously. This does not require 
		/// BeginAddPoint or EndAddPoint
		/// </summary>
		/// <param name="points"></param>
		public void AddPoints(params float[] points) {

			// don't bother checking for exceptions..  
			// if it throws an exception, then apparently you forgot to add
			// traces to the scope. :)
			lock ( traceLock ) {
				if ( isRunning == 1 )
					for ( int i = 0; i < points.Length; i++ )
						Traces[i].TracePoints.Add(points[i]);
			}

		}

		#endregion


	}



}
