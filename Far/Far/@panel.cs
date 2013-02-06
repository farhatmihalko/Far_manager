using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Far
{
    class _panel
    {
        //panel definition
        public int _x;
        public int _width;
        public int _y;
        public int _height;

        //private data
        private int footerTextDirectoryX;
        private int footerTextDirectoryY;



        public _panel(int x, int y, int width, int height)
        {
            this._x = x;
            this._y = y;
            this._width = width;
            this._height = height;
            //draw panel
            this.draw();
        }
        private void draw()
        {
            //drawing header
            kit.draw(_x, _y + _height - 2, _x + _width, _y + _height - 1, '—');
            this.footerTextDirectoryX = _x;
            this.footerTextDirectoryY = _y + _height;

            //WriteM();
            WritePanelHead(@"C:\User");
            updateFooterDirName(@"C:\User");
            updateFooterSize("Folder");
            WritePanelFooter(199, 200000);
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
                    kit.writeString(size);
                
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
    }
}
