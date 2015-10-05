using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cellular_Automata
{
    class Program
    {
        static void Main(string[] args)
        {
            string cell = "10010110010100100110100111101001";
            CellAutomata c = new CellAutomata(90, cell);
            List<string> output = c.Generations(cell.Length / 2);
            CellAutomata d = new CellAutomata(107, cell);
            List<string> doutput = d.Generations(cell.Length / 2);
            foreach (string item in output)
            {
                //if (item != "                                                                                                             ")
                    Console.WriteLine(item);

            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            //foreach (string item in doutput)
            //{
                //if (item != "                                                                                                             ")
                //Console.WriteLine(item);

            //}

            Console.ReadKey();
        }
    }
}
