using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParking
{
    public class Messenger
    {
        /// <summary>
        /// writes a message. use write information message or write error message insted 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        private static void WriteMessage(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("----------------------------------------------------------------------------------------------");
            Console.WriteLine(text);
            Console.WriteLine("----------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
        }
        /// <summary>
        /// Write error message.
        /// </summary>
        /// <param name="text"></param>
        public static void WriteErrorMessage(string text)
        {
            WriteMessage(text, ConsoleColor.Red);
        }
        /// <summary>
        /// Write Infomation message.
        /// </summary>
        /// <param name="text"></param>
        public static void WriteInformationMessage(string text)
        {
            WriteMessage(text, ConsoleColor.Green);
        }
    }
}
