using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParking
{
    public class Messager
    {
        public static void WriteLineColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteErrorMessage(string text)
        {
            WriteLineColor(text, ConsoleColor.Red);
        }
        public static void WriteInformationMessage(string text)
        {
            WriteLineColor(text, ConsoleColor.Green);
        }
    }
}
