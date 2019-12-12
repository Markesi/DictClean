using System;
using System.Collections.Generic;
using System.Text;

namespace DictClean
{
    class MycsvName
    {

        public string GetcsvPath(string defaultpath, string defaultname)
        {
            string userchoice = "";
            bool correct = false;

            string newcsvpath = "";
            string newcsvname = "";
            // There have to be a chance to modify the connection parameters
            // Requesting Connection Parameters to the user

            do
            {
                Console.Clear();
                Console.WriteLine("This console application corrects a csv file of words in ine language\n" +
                    "and requires the full path to the work file.\n" +
                    "If you do not enter a value, the default parameters will be used.\n" +
                    "The default path is: " + defaultpath + "\\" + defaultname +" !");
                Console.Write("Do you want to set new connection parameters (username, password, databasename) ? [K / E]:");
                userchoice = Console.ReadLine();

                userchoice = userchoice.ToUpperInvariant();

                if (userchoice == "K")
                {
                    Console.Clear();

                    Console.WriteLine("Please provide new path to the csv file.");
                    Console.Write("Database username [{0}]: ", defaultpath);
                    newcsvpath = Console.ReadLine();
                    
                    Console.Write("File name [{0}]:", defaultname);
                    newcsvname = Console.ReadLine();

                    Console.Write("Please check the correctness of the parameters that you gave.\n" +
                        "The file path is: " + newcsvpath + "\\" + newcsvname + " !\n" +
                        "Are you sure to proceed [K / E]: ");
                    userchoice = Console.ReadLine();
                    userchoice = userchoice.ToUpperInvariant();

                    if (userchoice == "K")
                    {
                        correct = true;
                    }
                    else
                    {
                        // It's important to get 
                        Console.WriteLine("\n Please retry to provide the parameters. \n Press any key to retry.");
                        Console.ReadLine();
                    }
                }
                else if (userchoice == "E")
                {
                    Console.WriteLine("\n The program will open the default file: ");
                    newcsvpath = defaultpath;
                    newcsvname = defaultname;
             
                    correct = true;
                    // Print to screen for debug
                    Console.WriteLine("The connection string is: " + newcsvpath + "\\" + newcsvname);
                    Console.ReadLine();

                }
                

            } while (!correct); //// Repeat the cicle if input differet from "K" or "E" 



            // Luodaan tietokantayhteys

            string mycsvfullpath = newcsvpath + "\\" + newcsvname;

            return mycsvfullpath;

            // ** END CHANGES
        }

    }
}
