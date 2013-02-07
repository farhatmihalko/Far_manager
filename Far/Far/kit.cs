using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Far
{ 
    class kit
    {
        public static ConsoleColor FontColor = ConsoleColor.White;
        public static ConsoleColor BGColor = ConsoleColor.Black;

        public static void setPos(int x, int y)
        {
            if (x >= 0 && x <= @params.WIDTH && y >= 0 && y <= @params.HEIGHT)
                Console.SetCursorPosition(x, y);
            else
                throw new Exception("The set position function cannot set position at input");
        }
        public static int getLeft()
        {
            return Console.CursorLeft;
        }
        public static int getTop()
        {
            return Console.CursorTop;
        }
        public static void writeChar(char? alpha)
        {
            if (alpha.HasValue)
                Console.Write(alpha.Value);
            else
                throw new Exception("Empty value in kit => writeChar()");
                
        }
        public static void writeString(object mess)
        {
            string line = mess as string;
            for (int i = 0; i < line.Length; i++)
                writeChar((char)line[i]);
        }
        public static void colors(ConsoleColor colorB, ConsoleColor colorF)
        {
            fontColor(colorF);
            backgroundColor(colorB);
        }
        public static void fontColor(ConsoleColor color = ConsoleColor.White)
        {
            FontColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
        }
        public static void backgroundColor(ConsoleColor color = ConsoleColor.Black)
        {
            BGColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
        }
        public static void draw(int _x_from, int _y_from, int _x_end, int _y_end, char alpha = ' ')
        {
            for (int i = _x_from; i < _x_end; i++)
            {
                for (int j = _y_from; j < _y_end; j++)
                {
                    setPos(i, j);
                    writeChar(alpha);
                }
            }
        }
        public static void clear(int _x_from, int _x_end, int y)
        {
            for (int i = _x_from; i < _x_end; i++)
            {
                setPos(i, y);
                writeChar(' ');
            }
        }
        public static void clearChar(int x, int y)
        {
            setPos(x, y);
            writeChar(' ');
            setPos(x, y);
        }

        public static string addToFull(string line, int max)
        {
            if (line.Length >= max)
                return line;
            else
            {
                for (int i = 0; i < max - line.Length; i++)
                    line += " ";
                return line;
            }
        }
    }
}
