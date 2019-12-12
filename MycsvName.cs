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
           
            do
            {
                Console.Clear();
                Console.WriteLine("This console application requires the full path to the work file.\n" +
                                  "The default path is: " + defaultpath + "\\" + defaultname +" !");
                Console.Write("Do you want change it ? [K / E]:");
                userchoice = Console.ReadLine();

                userchoice = userchoice.ToUpperInvariant();

                if (userchoice == "K")
                {
                    Console.Clear();

                    Console.WriteLine("Please provide the new filepath.");
                    Console.Write("Path to the folder [{0}]: ", defaultpath);
                    newcsvpath = Console.ReadLine();
                    
                    Console.Write("File name [{0}]:", defaultname);
                    newcsvname = Console.ReadLine();

                    Console.Write("Please check the new filepath:\n" +
                        newcsvpath + "\\" + newcsvname +
                        ". Are you sure to proceed [K / E]: ");
                    userchoice = Console.ReadLine();
                    userchoice = userchoice.ToUpperInvariant();

                    if (userchoice == "K")
                    {
                        correct = true;
                    }
                    else
                    {
                        // It's important to get the right path
                        Console.WriteLine("\n Please retry to provide the parameters. \n Press any key to retry.");
                        Console.ReadLine();
                    }
                }
                else if (userchoice == "E")
                {
                    
                    newcsvpath = defaultpath;
                    newcsvname = defaultname;
                    
                    correct = true;
                    // Print to screen for debug
                    Console.Write("\n The program will open the default file: " + newcsvpath + "\\" + newcsvname);
                    Console.ReadLine();

                }
                

            } while (!correct); //// Repeat the cicle if input differet from "K" or "E" 



            // Luodaan tietokantayhteys

            string mycsvfullpath = newcsvpath + "\\" + newcsvname;

            return mycsvfullpath;

        }

    }
}
