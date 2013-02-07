using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Far
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.Read();
            Application app = new Application(@params.WIDTH, @params.HEIGHT);
        }
    }

    class @params
    {
        public static int WIDTH = 125;
        public static int HEIGHT = 38;
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
