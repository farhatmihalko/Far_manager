using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Far
{
    class @footer
    {
        /*
         * 
         * Working with directory and cmd
         * 
         */
        public string CURR_PATH;
        public string CMD_PR = "->";
        public StringBuilder CURR_STRING;

        /*
         * 
         * Window sizes
         * 
         */ 
        public int width;
        public int height;

        public int draw_y;
        public int draw_x;

        public @footer(int width = 100, int height = 50)
        {
            this.width = width;
            this.height = height;
            this.draw_x = 0;
            this.draw_y = height - 2;
            this.draw();

            //initialize
            this.CURR_STRING = new StringBuilder();
        }

        private void draw()
        {
            /*
             * 
             * drawing footer
             * 
             */
            kit.setPos(0, height - 2);
            //footer cmd
            kit.fontColor(ConsoleColor.White);
            kit.backgroundColor(ConsoleColor.Black);
            kit.draw(0, this.height - 2, this.width, this.height - 1, ' ');
            kit.fontColor(ConsoleColor.Green);
            kit.draw(this.width - 2, this.height - 2, this.width-1, this.height - 1, '↑');
            kit.fontColor(ConsoleColor.White);
            kit.setPos(0, this.height - 2);
        }


        public void setPath(string _path)
        {
            //refactoring
            if (Directory.Exists(_path))
            {
                this.CURR_PATH = _path;
                kit.setPos(0, this.draw_y);
                DirectoryInfo dr = new DirectoryInfo(_path);
                kit.writeString(dr.FullName + CMD_PR + " ");
            }
        }
        public int leftMinimalCmd()
        {
            return (this.CURR_PATH + CMD_PR).Length + 1;
        }
        public int rightMinimalCmd()
        {
            return this.width - 10;
        } 

        public void cmd()
        {
            //System.Diagnostics.Process.Start(CURR_STRING.ToString());
            /*
            //refactoring
            //we can parse commands
            if (this.CURR_STRING.Length > 0)
            {
                string[] patt = this.CURR_STRING.ToString().Split(' ');
                if (patt[0].Equals("cd") && patt.Length == 2)
                {
                    if (patt[1].Equals(".."))
                    {
                        try
                        {
                            this.setPath(Directory.GetParent(this.CURR_PATH).FullName);
                        }
                        catch (NullReferenceException ex)
                        {
                        }
                    }
                    else
                    {
                        string pp = this.CURR_PATH + patt[1];
                        if (Directory.Exists(@patt[1]))
                            this.setPath(@patt[1]);
                        else if (Directory.Exists(@pp))
                        {
                            this.setPath(this.CURR_PATH + patt[1]);
                        }
                    }
                }
            }
            //clear cmd memory
           */
            this.CURR_STRING.Remove(0, this.CURR_STRING.Length);
        }
    }
}