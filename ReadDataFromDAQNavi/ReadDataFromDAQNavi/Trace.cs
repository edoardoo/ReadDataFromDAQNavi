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

namespace ReadDataFromDAQNavi {
	
	/// <summary>
	/// Holds data to describe each trace on the scope control
	/// </summary>
	public class Trace {

		/// <summary>
		/// Constructor. 
		/// </summary>
		public Trace() {
			RecalculatePen();
			traceLabel.AutoSize = true;
			traceLabel.Text = milliPerUnit.ToString();
		}

		#region Internal properties and methods


		// the scope hooks the textchanged event, and retexts it. 
		internal Label traceLabel = new Label();

		/// <summary>
		/// Called when the trace changes
		/// </summary>
		internal event VoidInvoke OnChange = delegate { };


		/// <summary>
		/// This is a list of the points that the scope should draw.
		/// </summary>
		internal List<float> TracePoints = new List<float>();

		// record keeping items

        internal int TraceStartIndex = 0;
        internal int TraceEndIndex = 0;

		internal int TraceItemWindow {
			get {
				if ( TraceEndIndex - TraceStartIndex < 0 )
					throw new Exception("WTF");

				return TraceEndIndex - TraceStartIndex;
			}
		}

		/// <summary>
		/// Pen that is used to draw the trace on something
		/// </summary>
		internal Pen Pen = null;

		#endregion


		#region Public properties and methods

		string unitName = "V";

		/// <summary>
		/// Metric name of the unit this represents
		/// </summary>
		public string UnitName {
			get { return unitName; }
			set { 
				unitName = value;
				OnChange();
			}
		}
		


		int milliPerUnit = 1000;

		/// <summary>
		/// Number of milli units displayed per major unit of the scope
		/// </summary>
		public int MilliPerUnit {
			get { return milliPerUnit; }
			set {
				milliPerUnit = value;
				OnChange();
			}
		}

		int zeroPositionY = 0;

		/// <summary>
		/// Position that the trace's zero position is at relative to the
		/// main zero position
		/// </summary>
		public int ZeroPositionY {
			get { return zeroPositionY; }
			set {
				zeroPositionY = value;
				OnChange();
			}
		}

		Color traceColor = Color.Green;

		/// <summary>
		/// Color of the line drawn when drawing this trace
		/// </summary>
		public Color TraceColor {
			get { return traceColor; }
			set {
				traceColor = value;
				RecalculatePen();
			}
		}

		float lineSize = 2F;

		/// <summary>
		/// Size of the line drawn when drawing this trace
		/// </summary>
		public float LineSize {
			get { return lineSize; }
			set { lineSize = value; RecalculatePen(); }
		}


		bool visible = true;

		/// <summary>
		/// Set to true when the trace should be drawn, false otherwise
		/// </summary>
		public bool Visible {
			get { return visible; }
			set {
				visible = value;
				OnChange();
			}
		}

		#endregion



		void RecalculatePen() {
			if ( Pen != null )
				Pen.Dispose();
			Pen = new Pen(traceColor, lineSize);
			OnChange();
		}
	}



	/// <summary>
	/// A strongly typed collection of Traces
	/// </summary>
	public class TraceCollection : System.Collections.ObjectModel.Collection<Trace> {

		internal event ListChangedEventHandler OnChange = null;

		/// <summary>
		/// Called when an item is inserted
		/// </summary>
		protected override void InsertItem(int index, Trace item) {
			base.InsertItem(index, item);
			OnChange(this, new ListChangedEventArgs(ListChangedType.ItemAdded, index));
		}

		/// <summary>
		/// Called when the items are cleared
		/// </summary>
		protected override void ClearItems() {
			base.ClearItems();
			OnChange(this, new ListChangedEventArgs(ListChangedType.Reset, 0));
		}

		/// <summary>
		/// Called when an item is set
		/// </summary>
		protected override void SetItem(int index, Trace item) {
			base.SetItem(index, item);
			OnChange(this, new ListChangedEventArgs(ListChangedType.ItemChanged, index));
		}

		/// <summary>
		/// Called when an item is removed 
		/// </summary>
		/// <param name="index"></param>
		protected override void RemoveItem(int index) {
			OnChange(this, new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
			base.RemoveItem(index);
		}
	}
}
