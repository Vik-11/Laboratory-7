using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Задания_1_10
{
    internal class InputValidation
    {
       
            static public int InputIntegerWithValidation(string s, int left, int right)                                                                         
            {
                bool isValid;
                int a;
                do
                {
                    Console.Write(s);
                    isValid = int.TryParse(Console.ReadLine(), out a);
                    if (isValid)
                        if (a < left || a > right)
                            isValid = false;
                    if (!isValid)
                    {
                        Console.WriteLine($"\nInput data is not integer or do not belong to [{left}; {right}]");
                        Console.WriteLine("Enter integer again.\n");
                    }
                } while (!isValid);
                return a;
            }

    
    }
}
