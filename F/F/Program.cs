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
        public static int WIDTH = 150; //must be even number
        public static int HEIGHT = 35; //must be even number
        
        //default colors
        public static ConsoleColor BG = ConsoleColor.DarkBlue;
        public static ConsoleColor FONT = ConsoleColor.White;

        //other
        public static int CURSOR_SIZE = 1;
    }
}
