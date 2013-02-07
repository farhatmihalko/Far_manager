using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Far
{
    class _panel
    {
        //panel definition
        public int _x;
        public int _width;
        public int _y;
        public int _height;

        public _panel @instance;

        
        //private data
        private int footerTextDirectoryX;
        private int footerTextDirectoryY;
        
        public _panel(int x, int y, int width, int height)
        {
            @instance = this;

            this._x = x;
            this._y = y;
            this._width = width;
            this._height = height;
            //draw panel
            this.draw();  
            //file contens
             
         }
        private void draw()
        {
            ConsoleColor colorBefore = kit.FontColor;
            ConsoleColor bgcolorBefore = kit.BGColor;
            kit.fontColor(ConsoleColor.Yellow);
            kit.setPos(this._x + this._width / 4 - this.headerName.Length / 2, this._y);
            kit.writeString(this.headerName.Substring(0, 1));
            kit.setPos(this._x + this._width / 4 * 3 - this.headerName.Length / 2, this._y);
            kit.writeString(this.headerName);
            kit.fontColor(colorBefore);
            
            //drawing header
            kit.fontColor(ConsoleColor.White);
            kit.draw(this._x + this._width / 2, _y, this._x + this._width / 2 + 1, this._height + _y - 1, '║');
            kit.fontColor(colorBefore);
            kit.draw(_x, _y + _height - 2, _x + _width, _y + _height - 1, '—');
            this.footerTextDirectoryX = _x;
            this.footerTextDirectoryY = _y + _height;
        }

        #region part_content
        public void WritePanelHead(string _T)
        {
            if (_T.Length > this._width / 2 - 2)
            {
                _T = _T.Substring(0, this._width / 2 - 2);
            }
            kit.setPos(this._x + this._width / 2 - _T.Length / 2, this._y - 1);
            kit.fontColor(ConsoleColor.Black);
            kit.backgroundColor(ConsoleColor.DarkCyan);
            kit.writeString(_T);
            kit.backgroundColor(ConsoleColor.DarkBlue);
            kit.fontColor(ConsoleColor.Cyan);
            
        }
        public void WritePanelFooter(int _file_number = 0, long _total_size = 0)
        {
            string _T = this.sizeReduce(_total_size.ToString()) + " in " + _file_number + " files";
            if (_T.Length > this._width / 2 - 2)
            {
                _T = _T.Substring(0, this._width / 2 - 2);
            }
            kit.setPos(this._x + this._width / 2 - _T.Length / 2, this._y + this._height - 2);
            kit.writeString(_T);
        }
        #endregion

        #region part_footer
        public void updateFooterDirName(string name)
            {
                int llSize = (this._width / 5) * 4;
                if (name.Length > llSize)
                    name = "-> " + name.Substring(0, llSize) + "...";
                kit.fontColor(ConsoleColor.Cyan);
                kit.setPos(this.footerTextDirectoryX, this.footerTextDirectoryY);
                kit.writeString(name);
                kit.fontColor(ConsoleColor.Cyan);
            }
        public void updateFooterSize(string size)
            {
                int kkSize = this.footerTextDirectoryX + (this._width / 5) * 4 + 6;
                kit.setPos(kkSize, this.footerTextDirectoryY);
                
                long result;
                if (long.TryParse(size, out result))
                {
                    kit.writeString(this.sizeReduce(result.ToString()));
                }
                else
                    kit.writeString("");
                
            }
        private string sizeReduce(string size)
            {
                long _size = long.Parse(size);
                string rr = "B";
                if (_size / (1024 * 1024 * 1024) >= 1)
                {
                    _size /= 1024 * 1024 * 1024;
                    rr = "GB";
                }
                else if (_size / (1024 * 1024) >= 1)
                {
                    _size /= 1024 * 1024;
                    rr = "MB";
                }
                else if (_size > 1024 - 1)
                {
                    _size /= 1024;
                    rr = "KB";
                }
                return _size.ToString() + rr;
            }
        #endregion 

        public void setPathAndDraw(string path)
        {
            //file_area.observe(path);
        }


        //controls
        public void up()
        {
            //file_area.up();
        }
        public void down()
        {
            //file_area.down();
        }
        public void open()
        {
            //file_area.open();
        }
    }
}



namespace Far
{
    class file_area
    {


        //control the flow
        public void open()
        {
        }
        public void up()
        {
        }
        public void down()
        {
        }
    }


    class line
    {
        /*
         * 
         * Definition
         * 
         */

        //coordinates and length of line
        public int x;
        public int y;
        public int length;
        //the definition
        public bool current;
        public string name;
        public string destination;

        /*
         * 
         * Constructor
         * 
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

        /*
         * 
         * Additional tools
         * 
         */
        public bool isDirectory()
        {
            if (Directory.Exists(this.destination))
                return true;
            return false;
        }
        public bool isFile()
        {
            if (File.Exists(this.destination))
                return true;
            return false;
        }
        public void changeBackground(ConsoleColor color)
        {
            kit.setPos(this.x, this.y);
            kit.backgroundColor(color);
            kit.writeString(this.fullString(this.name, this.length));
        }
        public void clearLine(ConsoleColor color)
        {
            kit.setPos(this.x, this.y);
            kit.backgroundColor(color);
            kit.writeString(this.fullString("", this.length));
        }

        /*
         * 
         * Private methods
         * 
         */
        private string fullString(string line, int length)
        {
            if (line.Length > length)
                return line.Substring(0, length);
            else
            {
                for (int i = 0; i < length - line.Length; i++)
                    line += " ";
                return line;
            }
        }
    }
}