using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class App1
    {
        public int Result { get; set; } = 1;
        public int Num { get; set; }

        
            
        public void Print() 
        {
            Num = int.Parse(Console.ReadLine());

            for (int i = 1; i <= Num; i++)
            {
                Result = i * Result;
            }
            Console.WriteLine(Result);
        }
    }
}
