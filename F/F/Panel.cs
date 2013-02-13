using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F
{
    /*
     * 
     * General definition of class
     * @x - starting position (x-coordinate)
     * @y - starting position (y-coordinate)
     * @width - total width of panel
     * @height - total height of panel
     * 
     * 
     */
    partial class Panel
    {
        public int x;
        public int y;
        public int width;
        public int height;
        public observer ob_left;

        public string current_path = "";

        //constructor
        public Panel(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;

            //drawing panel interface
            this.draw();

            //adding footer and header text
          
            this.setSubFooterName("Path name");
            this.setSubFooterDate("Date of creation");

            //create observer
            this.ob_left = new observer(this.x + 1, this.y + 2, this.width - 2, this.height, this);
        }
    }

    /*
     * 
     * Drawing the panel
     * 
     */
    partial class Panel
    {
        private void draw()
        {
            //set color
            @kit.setFontColor(ConsoleColor.Cyan);

            //left and right borders
            @kit.draw(this.x, this.y, this.x + 1, this.y + this.height, '║');
            @kit.draw(this.x - 1 + this.width, this.y, this.x + this.width, this.y + this.height, '║');

            //top and bottom borders
            @kit.draw(this.x, this.y, this.x + this.width, this.y + 1, '═');
            @kit.draw(this.x, this.y - 1 + this.height, this.x + this.width, this.y + this.height, '═');

            //set eagles of borders
            @kit.setPosition(this.x, this.y);
            @kit.writeChar('╔');
            @kit.setPosition(this.x + this.width - 1, this.y);
            @kit.writeChar('╗');
            @kit.setPosition(this.x, this.y + this.height - 1);
            @kit.writeChar('╚');
            @kit.setPosition(this.x + this.width - 1, this.y + this.height - 1);
            @kit.writeChar('╝');

            //drawind subfooter
            @kit.draw(this.x + 1, this.y + this.height - 3, this.x + this.width - 1, this.y + this.height - 2, '─');

            //set color to default
            @kit.setFontColor(Properties.FONT);

            //adding name string to header
            @kit.setFontColor(ConsoleColor.Yellow);
            @kit.setPosition(this.x + this.width / 4, this.y + 1);
            @kit.writeLine("Name");
            @kit.setPosition(this.x + this.width / 4 * 3, this.y + 1);
            @kit.writeLine("Name");
            @kit.setFontColor(Properties.FONT);

            //drawing the observer borders
            @kit.setFontColor(ConsoleColor.Cyan);
            @kit.draw(this.x + this.width / 2, this.y, this.x + this.width / 2 + 1, this.y + this.height - 2, '│');
            @kit.setPosition(this.x + this.width / 2, this.y + this.height - 3);
            @kit.writeChar('┴');
            @kit.setFontColor(Properties.FONT);
        }
    }

    partial class Panel
    {
        /**
         * Set header title string
         * @_name_ the string to output
         */
        public void setHeader(string _name_)
        {
            @kit.setFontColor(ConsoleColor.Cyan);
            @kit.draw(this.x + 1, this.y, this.x + this.width - 1, this.y + 1, '═');
            int length = _name_.Length;
            if (length > this.width - 1)
            {
                _name_ = _name_.Substring(0, this.width - 7) + "...";
                length = this.width - 4;
            }
            @kit.setBackgroundColor(ConsoleColor.DarkCyan);
            @kit.setFontColor(ConsoleColor.Black);
            @kit.setPosition(this.x + this.width/2 - length/2, this.y);
            @kit.writeLine(_name_);
            @kit.setFontColor(Properties.FONT);
            @kit.setBackgroundColor(Properties.BG);
        }
        /**
         * Set footer title string
         * @size total size of current directory files
         * @fl_number number of files in current directory
         */
        public void setFooter(string size, int fl_number)
        {
            @kit.setFontColor(ConsoleColor.Cyan);
            @kit.draw(this.x + 1, this.y - 1 + this.height, this.x + this.width - 1, this.y + this.height, '═');
            string res_string = " " + this.sizeReduce(size) + " in " + fl_number + " files ";
            @kit.setFontColor(ConsoleColor.Cyan);
            @kit.setPosition(this.x + this.width/2 - res_string.Length/2, this.y + this.height - 1);
            @kit.writeLine(res_string);
            @kit.setFontColor(Properties.FONT);
        }
        /**
         * Method to reduse size
         * GB > MB > KB > B
         */
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
    }

    partial class Panel
    {
        /**
         * Set selected file or directory name to subfooter
         * @_name_ the name of selected file or directory
         */
        public void setSubFooterName(string _name_)
        {
            //before adding clearing
            @kit.setBackgroundColor(Properties.BG);
            @kit.draw(this.x + 1, this.y + this.height - 2, this.x + this.width / 2, this.height - 1, ' ');
            //maximal length
            int len = this.width / 2 - 4;
            if (_name_.Length > len)
            {
                _name_ = _name_.Substring(0, len) + "...";
            }
            @kit.setPosition(this.x + 1, this.y + this.height - 2);
            @kit.writeLine(_name_);
        }
        /**
         * Set selected creation data
         * @_date_ creation of file or directory
         */
        public void setSubFooterDate(string _date_)
        {
            @kit.setPosition(this.x + this.width - _date_.Length - 3, this.y + this.height - 2);
            @kit.writeLine(_date_);
        }
    }

    partial class Panel
    {
        /**
         * Observing files and directories
         */
        public void commander(string _path_)
        {
            //we have path
            this.setHeader(_path_);
            this.ob_left.draw(_path_);

            //set to default path
            this.current_path = this.setPathInFooter(_path_);
        }
    }

    /**
    * This part describes all moves in interface
    * up, down
    */
    partial class Panel
    {
        /**
         * When user pressed UpArrow button
         */
        public void up()
        {
            this.ob_left.up();
        }
        /**
         * When user pressed DownArrow button
         */
        public void down()
        {
            this.ob_left.down();
        }
        /**
         * When user pressed Enter button
         */
        public void open()
        {
            string path = this.ob_left.open();
            string pos = this.setPathInFooter(path);
            
            //save the path
            this.current_path = pos;
        }
    }

    /**
     * Footer and cmd controller
     */
    partial class Panel
    {
        /**
         * Adding path string to footer
         * @_path_ string path
         */
        public string setPathInFooter(string _path_)
        {
            int _path_max_len = 40;
            if (_path_.Length > _path_max_len)
                _path_ = _path_.Substring(0, _path_max_len);
            _path_ += ">";
            return _path_;
        }
    }
}