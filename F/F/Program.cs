using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F
{
    class Program
    {
        static void Main(string[] args)
        {
            Application app = new Application();
            app.init();
        }
    }

    /**
     * General settings for console application
     * @TITLE the title of window
     * @WIDTH number of width pixels
     * @HEIGHT number of height pixels
     */
    class Properties
    {
        //default names and titles
        public static string TITLE = "Far Manager";

        //default sizes
        public static int WIDTH = 120; //must be even number
        public static int HEIGHT = 36; //must be even number
        
        //default colors
        public static ConsoleColor BG = ConsoleColor.DarkBlue;
        public static ConsoleColor FONT = ConsoleColor.White;

        //other
        public static int CURSOR_SIZE = 1;

        //chars that user can enter 
        public static char[] chars = new char[]{ 
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l',
            'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
            'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
            'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
            'W', 'X', 'Y', 'Z', '_', '\\', '1', '2', '3', '4', '5', '6',
            '7', '8', '9', '0', '.', ',', '-', '=', '+', ' ', ':'
        };
    }
}
