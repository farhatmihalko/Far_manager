using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace F
{
    class line
    {
        /**
         * Line class
         * @x left position in console
         * @y top position in console
         * @length of line
         * @current shows that this line is selected
         * @name name of this line
         * @destination describes the full path of item
         */
        public int x;
        public int y;
        public int length;
        public bool current;
        public string name;
        public string destination;

        /**
         * Constructor of current class
         * @x left position
         * @y top position
         * @length length of current line
         * @isCurrent show that the item is selected
         * @name name of line
         * @destination full path
         */
        public line(int x, int y, int length, bool isCurrent, string name, string destination)
        {
            this.x = x;
            this.y = y;
            this.length = length;
            this.current = isCurrent;
            this.name = name;
            this.destination = destination;
        }

        /**
         * Check if that line is directory
         * @return(bool) if line is directory
         */
        public bool isDirectory()
        {
            if (Directory.Exists(this.destination))
                return true;
            return false;
        }
        /**
         * Check if that line is file
         * @return(bool) if line is file
         */
        public bool isFile()
        {
            if (File.Exists(this.destination))
                return true;
            return false;
        }
        /**
         * If line was selected or disselected, we change
         * the background of item
         */
        public void changeBackground(ConsoleColor color)
        {
            @kit.setPosition(this.x, this.y);
            @kit.setBackgroundColor(color);
            if (this.isFile())
                @kit.setFontColor(this.getExtensionColor((new FileInfo(this.destination)).Extension));
            if (this.isDirectory())
            {
                DirectoryInfo dr = new DirectoryInfo(this.destination);
                if ((dr.Attributes & FileAttributes.Hidden) != 0)
                {
                    if (this.current)
                        @kit.setFontColor(ConsoleColor.Yellow);
                    else
                        @kit.setFontColor(ConsoleColor.DarkCyan);
                }
            }
            @kit.writeLine(this.fullString(this.name, this.length));
            //to default colors
            @kit.setFontColor(Properties.FONT);
            @kit.setBackgroundColor(Properties.BG);
        }
        public void changeBackground(ConsoleColor color, ConsoleColor font)
        {
            @kit.setPosition(this.x, this.y);
            @kit.setBackgroundColor(color);
            @kit.setFontColor(font);

            @kit.writeLine(this.fullString(this.name, this.length));
            //to default colors
            @kit.setFontColor(Properties.FONT);
            @kit.setBackgroundColor(Properties.BG);
        }
        /*
         * Clearing line
         */
        public void clearLine(ConsoleColor color)
        {
            @kit.setPosition(this.x, this.y);
            @kit.setBackgroundColor(color);
            @kit.writeLine(this.fullString("", this.length));
            @kit.setBackgroundColor(Properties.BG);
        }
        /**
         * Drawing this line
         */
        public void drawLine()
        {
            string drawing = fullString(this.name, this.length);
            @kit.setPosition(this.x, this.y);
            if (this.current)
                @kit.setBackgroundColor(ConsoleColor.DarkCyan);
            if (this.isFile())
                @kit.setFontColor(this.getExtensionColor((new FileInfo(this.destination)).Extension));
            if (this.isDirectory())
            {
                DirectoryInfo dr = new DirectoryInfo(this.destination);
                if ((dr.Attributes & FileAttributes.Hidden) != 0)
                {
                    if (this.current)
                        @kit.setFontColor(ConsoleColor.Yellow);
                    else
                        @kit.setFontColor(ConsoleColor.DarkCyan);
                }
            }
            @kit.writeLine(drawing);

            //to default colors
            @kit.setFontColor(Properties.FONT);
            @kit.setBackgroundColor(Properties.BG);
        }
        /**
         * Removing the file or directory
         */
        public void removeCurrent()
        {
            try
            {
                var dest = this.destination;
                if (this.isFile())
                {
                    FileInfo file = new FileInfo(dest);
                    if (!file.IsReadOnly)
                        File.Delete(dest);
                    this.clearLine(Properties.BG);
                    this.deepClear();
                }
                else if (this.isDirectory())
                {
                    DirectoryInfo dir = new DirectoryInfo(dest);
                }
            }
            catch (IOException e)
            {
                //do something
            }
        }
        /**
         * Cut the chars that not visible
         * @return(String)
         */
        private string fullString(string line, int length)
        {
            if (line.Length > length)
                return line.Substring(0, length);
            else if (line.Length == length)
                return line;
            else
            {
                //refectaring
                string tp = "";
                for (int i = 0; i < length - line.Length; i++)
                    tp += " ";
                return line + tp;
            }
        }
        /**
         * Set the color that depends on file extension
         */
        private ConsoleColor getExtensionColor(string _ex_)
        {
            ConsoleColor output;
            switch (_ex_)
            {
                case ".exe":
                    output = ConsoleColor.Green;
                    break;
                case ".cs":
                    output = ConsoleColor.DarkGray;
                    break;
                case ".sys":
                    output = ConsoleColor.Green;
                    break;
                case ".ini":
                    output = ConsoleColor.DarkRed;
                    break;
                default:
                    output = ConsoleColor.Cyan;
                    break;
            }
            return output;
        }

        /**
         * Clearing the backgound
         */
        public void deepClear()
        {
            @kit.setPosition(this.x, this.y);
            for (int i = 0; i < this.length; i++)
                @kit.writeChar(' ');
        }
    }
}
