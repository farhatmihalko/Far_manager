using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Far
{
    class kit
    {
        public static void writeChar(char? alpha)
        {
            if (alpha.HasValue)
                Console.Write(alpha.Value);
            else
                throw new Exception("Empty value in kit => writeChar()");
                
        }
        public static void writeString(string? mess)
        {
            if (mess.HasValue)
                for (int i = 0; i < mess.Value.Length; i++)
                    writeChar((char)mess.Value[i]);
            else
                throw new Exception("Empty value in kit => writeString()");
        }

    }
}
