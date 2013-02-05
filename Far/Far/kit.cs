using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Far
{ 
    class kit
    {
        public static void setPos(int x, int y, int width, int height)
        {
            if (x >= 0 && x <= width && y >= 0 && y <= height)
                Console.SetCursorPosition(x, y);
            else
                throw new Exception("The set position function canno't set position at input");
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
    }
}
