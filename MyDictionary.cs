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

        // Order the Dictionary as IS (integer, string)
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


        // To remove duplicates in the dictionary reversing to SI (String Integer)
        public IDictionary<string, int> ReverseUniqueDict(IDictionary<int, string> dict)
        {
            IDictionary<string, int> wlsi = new Dictionary<string, int>();
            // Counting Duplicates and Originals
            int duplwd = 0;
            int origwd = 0;

            // measuring time in the operation
            Stopwatch sw = new Stopwatch();
            sw.Start();

            foreach (KeyValuePair<int, string> kvPair in dict)
            {
                try
                {
                    wlsi.Add(kvPair.Value, kvPair.Key);
                    origwd++;
                    if ((origwd % 10000) == 0)
                    {
                        Console.Write("+");
                    }
                }
                catch (ArgumentException)
                {
                    duplwd++;
                    if ((duplwd % 1000) == 0)
                    {
                        Console.Write("_");
                        // Console.Write(kvPair.Value + " already present! ");
                    }
                }

            }

            sw.Stop();
            Console.WriteLine("found {} original terms and {} duplicates.", origwd, duplwd);
            Console.WriteLine("Dictionary Duplicate Removal elampsed={0}", sw.Elapsed);
            return wlsi;
        }


        // Write the entire word list (mydictionary integer string) into a file
        public void SaveDictionaryis(string myfilepath, string separatedby, IDictionary<int, string> mydict)
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



        // Save the String-Int form of the dictionery (mydictionary string, integer) into a file
        public void SaveDictionarysi(string myfilepath, string separatedby, IDictionary<string, int> mydictsi)
        {
            // measuring time in the operation
            Stopwatch sw = new Stopwatch();
            sw.Start();

            using (StreamWriter fileWriter = new StreamWriter(myfilepath))
            {
                foreach (KeyValuePair<string, int> kvPair in mydictsi)
                {
                    // fileWriter.WriteLine("{0}{1} {2}{3}", kvPair.Key.ToString(), separatedby, kvPair.Value, Environment.NewLine);
                    fileWriter.WriteLine("{0}{1}{2}",
                                         kvPair.Key,
                                         separatedby,
                                         kvPair.Value.ToString()); // the value is integer
                }
                fileWriter.Close();
            }
            sw.Stop();
            Console.WriteLine("File writing elapsed={0}", sw.Elapsed);
        }



        
        


        // Read line by line and remove numeric sequences
        // 542522 1 450/87 4 (5.6802e-06 %)
        // 542522 1 1990 54 (5.6802e-06 %)



        // open csv dictionary file
    }
}
