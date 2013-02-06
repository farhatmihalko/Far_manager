using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Text;
using System.ComponentModel;

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
                setPathAf(_path);
            }
        }
        private void setPathAf(string _path)
        {
            this.CURR_PATH = _path;
            kit.setPos(0, this.draw_y);
            DirectoryInfo dr = new DirectoryInfo(_path);
            if (_path.Length > 25)
                kit.writeString(dr.FullName.Substring(0, 10) + "..." + dr.FullName.Substring(dr.FullName.Length - 10, 10));
            else
                kit.writeString(dr.FullName);
            kit.writeString(" ->");
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
            if (this.CURR_STRING.Length > 0)
            {
                //we have command string
                cmd_processing(CURR_STRING.ToString().Split(' '));
            }
            this.CURR_STRING.Remove(0, this.CURR_STRING.Length);
        }
        private void cmd_processing(string[] args)
        {
            //command
            string command = args[0];
            switch (command)
            {
                case "exit" :
                    Environment.Exit(0);
                    break;
                case "cd" :
                    string path = args[1];
                    setPathAf(path);
                    break;
                case "rm" :
                    string target = args[1];
                    break;
                default :
                    break;
            }
        }
    }
}