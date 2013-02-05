using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Far
{
    class kit
    {
        public static int _width;
        public static int _height;
        public static void setParams(int _width, int _height)
        {
            kit._width = _width;
            kit._height = _height;
        }

        public static bool setPos(int _x, int _y)
        {
            if (_x >= 0 && _x <= kit._width
                && _y >= 0 && _y <= kit._height)
            {
                Console.SetCursorPosition(_x, _y);
                return true;
            }
            return false;
        }
        public static void writeChar(char _alpha)
        {
            Console.Write(_alpha);
        }
        public static bool writeLine(string line)
        {
            return false;
        }
    }
}
