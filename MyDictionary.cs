using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Diagnostics; // for execution time measurement

namespace DictClean
{
    class MyDictionary
    {
        public IDictionary<int, string> myDictionary { get; private set; }


        // Create lists of strings for each column of the cvs file
        // This file has nr. columns separated by a single blank
        // Some line contains an extra blank
        //  
        // 7 86711 hän (0.4925 %)
        // 8 78076 mutta (0.4434 %)
        // 98657 13 2---0 1---0 (7.3842e-05 %)
        /*
         * first and second column end with a blank
         * the third column ends before the opening parenthesis "("
         * the fourth column ends with a closing parenthesis ")"
         * 
         */


        // Load the entire word list (mydictionary) in memory


        public IDictionary<int, string> LoadDictionary(string myfile, string separatedby)
        {
            // Using encoding to recover scandinavian chars from the file 
            using (var streamdata = new StreamReader(myfile, Encoding.GetEncoding("iso-8859-1")))
            {

                char[] charsToTrim1 = {'@','$','#','%',')','/','\'','<','>','=','[',']','`','-', '~', '|','½', '°', '®', '©', '¡', '±','´','³', '§','£','»', '«','*','&',':','0','1', '2', '3', '4', '5','6', '7', '8', '9', ' ', '+'};
                string strjoiner;
                int i = 1;
                int j = 0;

                IDictionary<int, string> wl = new Dictionary<int, string>();

                
                while (!streamdata.EndOfStream)
                {
                    var readline = streamdata.ReadLine();

                    var values = readline.Split(separatedby);
                    
                    // if the line has more than 3 blanks
                    // so has more than 4 columns
                   
                    j = 2;
                    //
                    strjoiner = "";
                    // Since the file can be irregular (4 or more separated fileds)
                    // and for each line I want the string
                    // contained in column 3 (having index 2) and followings
                    // except the last one (index Length - 1)
                    do
                    {
                        
                        strjoiner = strjoiner + values[j].Trim(charsToTrim1); // eliminating spaces and numbers

                        j++;

                    } while (j == (values.Length - 1));

                    // Removing single and two char words
                    if (!string.IsNullOrWhiteSpace(strjoiner) && (strjoiner.Length > 1)) // eliminating empty lines
                    {
                        wl.Add(new KeyValuePair<int, string>(i, strjoiner.ToUpperInvariant())); //words are made uppercase
                        i++;
                    }

                    
                }

                return wl;
            }
        }


        // Write the entire word list (mydictionary) into a file
        public void SaveDictionary(string myfilepath, string separatedby, IDictionary<int, string> mydict)
        {
            // measuring time in the operation
            Stopwatch sw = new Stopwatch();
            sw.Start();

            using (StreamWriter fileWriter = new StreamWriter(myfilepath))
            {
                foreach (KeyValuePair<int, string> kvPair in mydict)
                {
                    // fileWriter.WriteLine("{0}{1} {2}{3}", kvPair.Key.ToString(), separatedby, kvPair.Value, Environment.NewLine);
                    fileWriter.WriteLine("{0}{1}{2}",
                                         kvPair.Key.ToString(),
                                         separatedby,
                                         kvPair.Value);
                }
                fileWriter.Close();
            }
            sw.Stop();
            Console.WriteLine("File writing elapsed={0}", sw.Elapsed);
        }


   

        // To do the removal of duplicates in the dictionary
        public IDictionary<int, string> CleanDictionary(IDictionary<int, string> dict)
        {
            return dict;
        }

        public IDictionary<int, string> OrderDictionary( /*string sense, string type,*/ IDictionary<int, string> mydict)
        {


            IDictionary<int, string> wl = new Dictionary<int, string>();
            // testing by measuring time
            Stopwatch sw = new Stopwatch();
            sw.Start();

                        // Use OrderBy method.value
            foreach (KeyValuePair<int, string> kvPair in mydict.OrderBy(i => i.Value))
            {
                wl.Add(kvPair);

               // testing by print at console
               // Console.WriteLine(kvPair);
                
            }
            // return time of execution
            sw.Stop();
            Console.WriteLine("Elapsed: {0}", sw.Elapsed);

            //
            return wl;
        }



        // Read line by line and remove numeric sequences
        // 542522 1 450/87 4 (5.6802e-06 %)
        // 542522 1 1990 54 (5.6802e-06 %)



        // open csv dictionary file
    }
}
