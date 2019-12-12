using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics; // for execution time measurement
using System.IO;


/* Project DictClean
 * by Michelangelo Marchesi
 * start date:   28/11/2019
 * last review:  29/11/2091
 */

/* Project specification 
 * and to do list:
 * 

//Dictionary Normalizer
// Given list of words in csv file (comma separated values)
// returns a file that has been cleaned using regular expressions
// removing errors in compilation of the list.
// The check happens on a given column of the csv file.
// The separator in the file can be decided (comma, point, space, tab)
// The file is read, precessed for trimming and number removal
// then is added an index and sorted by the given column
// Some of the features:
// - trimming non numeric and alphabetic beginners and enders (- / > (like: =d, heränneellä/, -pula-aika) deleting line if short remains
// - purely numeric lines are removed (even if contain : , . / --- + or space, ex: 55a, 90 00o:n, 1992 21, 160000:een)
// - text+num are removed (ex. tol9133, 90-luku)
// - travel routes or sport are removed (gulko---bischoff, v4---arvo, ilves---lahden)
// - url type words to be deleted (ex. http://, desc=href=http://)
// - remove words containing ~ (ex. g~nter)
// - remove words containing : (like fst:lle, usa:sa, ky:ssä, ap-dj:lle, gmbh:lta)
// - remove abitation descrition like (2h+k+wc+kph)
// - remove
//
//
//

/* Classes:
 * Dictionary methods: 
 * 
 */


namespace DictClean
{
    class Program
    {
        static void Main(string[] args)
        {
            
            /* TESTER! set the current filepath of the test files under the "Data" directory
             * of  your fork. Rigth click on the file name (in the Solution explorer) and choose
             * properties (Alt+Enter) to find the filepath.
             *
             * example: string myfilepath = @"M:\MichelangeloMarchesi\_CodeDevelopment\source\repos\DictClean\Data\";
             */

            string myfilepath = @"C:\Users\Oma\source\repos\Markesi\DictClean\Data"; //just for testing

            /* this is the right way when the application is delivered as exe file
            string myfilepath = Directory.GetCurrentDirectory() + @"\data\";
            string myfilepath = @"\Data\";
            */

            string myfilename = "parole_frek_Fi_1339787_to_normalize.csv";
            MycsvName fn = new MycsvName();


            string myfullpath = fn.GetcsvPath(myfilepath, myfilename);
            string myseparator = " ";

            IDictionary<int, string> myWordList = new Dictionary<int, string>();

            MyDictionary xm = new MyDictionary();

            //test advice
            
            Console.WriteLine("Loading dictionaryfile: {0}", myfullpath);

            myWordList = xm.LoadDictionary(myfullpath, myseparator);

            // test load printing few lines

            for (int i=1; i<100; i++)
            {
                int jumper = 10000 * i;
                Console.Write(" {0}) {1}{2}", myWordList.Keys.ElementAt(jumper), myWordList.Values.ElementAt(jumper), ";");
            }

            Console.WriteLine("");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            // End loading test
           
            // Testing IDictionary sorting
            Console.WriteLine("Reodering the dictionary by value.");
            myWordList = xm.OrderDictionary(myWordList);
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
            // End sorting test

            // testing file writing of the wordlist to csv file
            Console.WriteLine("Writing {0} words of dictionary to a csv file.", myWordList.Count);
            
            myseparator = ";";
            string mynewfilename = "dict_fi_ordered_asc_01.csv";
            myfullpath = myfilepath + "\\"+ mynewfilename;
            xm.SaveDictionary(myfullpath, myseparator, myWordList);
                        
            Console.WriteLine("Saved {0} words in a csv file (separated by \"{1}\" at: {2}.", myWordList.Count, myseparator, myfullpath);
            
            Console.WriteLine("");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            // End saving to csv file test

        }
    }
}
