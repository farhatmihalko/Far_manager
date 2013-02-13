using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace F
{
   partial class observer
    {
        /**
         * Observer class
         * @x - left starting position
         * @y - top starting position
         * @width - width of working area
         * @height - height of working area
         */
        public int x;
        public int y;
        public int width;
        public int height;
        public ArrayList LL_list;
        private Panel parent;
        /**
         * Constructor
         */
        public observer(int x, int y, int width, int height, Panel parent)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.parent = parent;
            LL_list = new ArrayList();
        }
    }
   partial class observer
   {
       /**
        * Drawing the current directory
        */
       public void draw(string path)
       {
           if(@kit.isDir(path))
           {
               
               //clearing the memory
               LL_list.Clear();

               try
               {
                   //get the information about current directory
                   DirectoryInfo current_dir = new DirectoryInfo(path);


                   //files and directories in this dir
                   FileInfo[] files = current_dir.GetFiles();
                   DirectoryInfo[] dirs = current_dir.GetDirectories();

                   //params to good grid
                   int _x_ = 0;
                   int _y_ = 1;
                   int _max_height = this.height - 6;
                   bool _overloading = false;
                   int maximal = 0;

                   //adding root path to container
                   LL_list.Add(new line(this.x + _x_, this.y + _y_ - 1, this.width / 2 - 1, true, "..", this.getParent(current_dir)));

                   //get dirs
                   foreach (DirectoryInfo iteration in dirs)
                   {
                       if (_y_ > _max_height)
                       {
                           _x_ = this.width / 2 + 1;
                           _y_ = 0;
                           _overloading = true;
                       }
                       line item = new line(this.x + _x_, this.y + _y_++, this.width / 2 - 1, false, iteration.Name, iteration.FullName);

                       //Adding item to container
                       LL_list.Add(item);
                   }

                   //get files from current directory and calculate the size
                   long files_size = 0;
                   int files_number = 0;
                   foreach (FileInfo iteration in files)
                   {
                       if (_y_ > _max_height)
                       {
                           _x_ = this.width / 2 + 1;
                           _y_ = 0;
                           _overloading = true;
                       }
                       files_size += iteration.Length;
                       files_number++;
                       line item = new line(this.x + _x_, this.y + _y_++, this.width / 2 - 1, false, iteration.Name, iteration.FullName);

                       //adding item to line container
                       LL_list.Add(item);
                   }

                   //adding path to header to panel
                   this.parent.setHeader(path);

                   //adding the size to footer
                   this.parent.setFooter(files_size.ToString(), files_number);

                   //working with received files and dirs
                   this.Operate();
               }
               catch (UnauthorizedAccessException e)
               {
                   this.parent.refresh();
               }
           }     
       }

       private string getParent(DirectoryInfo dir)
       {
           if (dir.Parent == null)
           {
               //this is root
               return dir.FullName;
           }
           else
               return dir.Parent.FullName;
       }
       private void Operate()
       {
           for (int i = 0; i < LL_list.Count; i++)
           {
               line current = (line) LL_list[i];
               if(i < this.height*2 - 10)
                   current.drawLine();
           }
       }
   }

   partial class observer
   {
       public void up()
       {
           for (int i = 0; i < LL_list.Count; i++)
           {
               line ll = (line) LL_list[i];
               if (ll.current)
               {
                   ll.current = false;
                   ll.changeBackground(Properties.BG);
                   line nnll = (line)LL_list[(i==0?LL_list.Count-1:i-1)];
                   nnll.current = true;
                   nnll.changeBackground(ConsoleColor.DarkCyan);
                   @kit.setBackgroundColor(Properties.BG);
                   //adding to subfooter file name
                   this.setChoosedFile(nnll);
                   break;
               }
           }
       }
       public void down()
       {
           for (int i = 0; i < LL_list.Count; i++)
           {
               line ll = (line)LL_list[i];
               if (ll.current)
               {
                   ll.current = false;
                   ll.changeBackground(Properties.BG);
                   line nnll = (line) LL_list[(i + 1) % LL_list.Count];
                   nnll.current = true;
                   nnll.changeBackground(ConsoleColor.DarkCyan);
                   @kit.setBackgroundColor(Properties.BG);
                   //adding to subfooter file name
                   this.setChoosedFile(nnll);
                   break;
               }
           }
       }
       public string open()
       {
           string path = "";
           bool clear = true;
           for (int i = 0; i < LL_list.Count; i++)
           {
               line cc = (line)LL_list[i];
               if (cc.current)
               {
                   if(cc.isDirectory())
                       path = cc.destination;
                   else if (cc.isFile())
                   {
                       clear = false;
                       System.Diagnostics.Process.Start("notepad", cc.destination);
                   }
               }
           }
           if (clear)
           {
               for (int i = 0; i < LL_list.Count; i++)
               {
                   line cc = (line)LL_list[i];
                   cc.clearLine(Properties.BG);
               }
               LL_list.Clear();
               this.draw(@path);
           }
           return path;
       }
       private void setChoosedFile(line choose)
       {
           this.parent.setSubFooterName(choose.name);
           if (choose.isDirectory())
           {
               DirectoryInfo dr = new DirectoryInfo(choose.destination);
               this.parent.setSubFooterDate(dr.CreationTime.ToString());
           }
           else if (choose.isFile())
           {
               FileInfo fi = new FileInfo(choose.destination);
               this.parent.setSubFooterDate(fi.CreationTime.ToString());
           }
           
       }
   }


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
            if(this.isFile())
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
                case ".exe" :
                    output = ConsoleColor.Green;
                    break;
                case ".cs" :
                    output = ConsoleColor.DarkGray;
                    break;
                case ".sys" :
                    output = ConsoleColor.Green;
                    break;
                default :
                    output = ConsoleColor.Cyan;
                    break;
            }
            return output;
        }
    }
}
