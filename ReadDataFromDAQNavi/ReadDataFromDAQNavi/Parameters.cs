using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDataFromDAQNavi {
    class Parameters {
        // el path del fichero de configuraion
        private static string CONFIG_FILE = "..\\..\\..\\..\\PARAMS.CFG";
        //la estructura basica de la entidad de parametros
        private List<ParamsSection> parameters = new List<ParamsSection>();
        public Parameters() {
            readFile();
        }
        private void readFile() {
            //lee el fichero y compone una lista con una linea cada elemento
            try {
                using (StreamReader sr = new StreamReader(CONFIG_FILE)) {
                    //Leemos el Stream del fichero

                    int counter = 0;
                    string line;
                    List<string> listOfLines = new List<string>();
                   
                    while ((line = sr.ReadLine()) != null) {
                        listOfLines.Add(line);
                        counter++;
                    }

                    sr.Close();                   
                    parseParams(listOfLines);
                    
                }
            } catch (Exception e) {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);

            }
            
        }
     
        private void parseParams(List<string> paramFileLines) {

            //lee y enterpreta los parametros de el fichero y popula la estructura basica
            
            int counterSection = 0;

            for (int i = 0; i < paramFileLines.Count(); i++) {
                string stringedName = paramFileLines[i].ToString();
                
                if (stringedName.Contains("[") || stringedName.Contains("]")) {
                    string name = cleanSectionName( stringedName );
                    ParamsSection ps = new ParamsSection(name);

                    parameters.Add(ps);
                    
                } else if(stringedName.Split(';')[0].Contains('=')  ) {
                    string [] paramKeyValue = getKeyValueParameter(stringedName);
                    int sectionIndex = parameters.Count();
                    parameters[sectionIndex-1].addParameter(paramKeyValue[0], paramKeyValue[1]);
                }
                


            }

               
            
        }
        private string cleanSectionName( string dirtyName ) {
            //limpia los nombres de la sectiones 
            return dirtyName.Replace("[", "").Replace("]", "").Replace("_", " ").Trim();
        
        }


        private string [] getKeyValueParameter( string wholeParameter) {
            //compone un array con la clave y el valore 
            string[] splitted = wholeParameter.Split('=');
            splitted[1] = splitted[1].Split(';')[0].Trim();
            splitted[0] = splitted[0].Trim();      
            return splitted;
        }
        public string getFormattedParams() {
            //prepara una stringa formattada por el campo de texto
          
            string parametersText = "";
            string newLine = Environment.NewLine;
            for( int j = 0; j< parameters.Count(); j++) {
                parametersText += newLine + parameters[j].getName() + newLine;
                parametersText += "----------------------------------" + newLine;

                for (int i = 0; i < parameters[j].getAllParameters().Count(); i++) {
                    string name = parameters[j].getAllParameters()[i].getKey();
                    string value = parameters[j].getAllParameters()[i].getValue();
                    parametersText += name + " : " + value + newLine;

                }
            }
            return parametersText;
        }

    }
    
}
