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
        public string path = @"C:\";
        public string CMD_PR = "->";
        public string CMD_PATH = "";
        /*
         * 
         * Window sizes
         * 
         */ 
        public int width;
        public int height;
        
        public @footer(int width = 100, int height = 50)
        {
            this.width = width;
            this.height = height;

            this.draw();
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
            this.cmd(height - 3);
        }

        private void cmd(int P_HEIGHT)
        {
            string printer = this.path + this.CMD_PR + " ";
            /*
             * 
             * CMD WORKER => P_HEIGHT is needed param!
             *
             */
            if (Directory.Exists(this.path))
                kit.writeString(printer);
            else
                Directory.CreateDirectory(this.path);

            cmd_line(printer);
        }

        private void cmd_line(string _path)
        {
            StringBuilder cmd_line = new StringBuilder();
            while (true)
            {
                int left_before_push = Console.CursorLeft;
                int top_before_push = Console.CursorTop;
                //press button
                ConsoleKeyInfo btn = Console.ReadKey();
                ConsoleKey btn_char = btn.Key;
                //new position after pressing
                int left_after_push = Console.CursorLeft;
                int top_after_push  = Console.CursorTop;

                if (left_after_push > this.width - _path.Length - 10)
                    kit.setPos(left_before_push, top_before_push);    

                switch (btn_char)
                {
                    case ConsoleKey.Enter:
                        //enter pressed
                        this.cmd_processing(cmd_line.ToString());
                        //clear cmd_line
                        cmd_line.Remove(0, cmd_line.Length);
                        //clear footer line
                        kit.draw(_path.Length, top_before_push, this.width - 2, top_before_push + 1, ' ');
                        kit.setPos(_path.Length, top_before_push);
                        break;
                    case ConsoleKey.Backspace:
                        //backspace pressed
                        if (left_before_push > _path.Length)
                        {
                            kit.writeChar(' ');
                            kit.setPos(left_after_push, top_after_push);
                            cmd_line.Remove(cmd_line.Length - 1, 1);
                        }
                        else
                            kit.setPos(left_before_push, top_before_push);
                        break;
                    default:
                        //otherwise
                        break;
                }

                if (@params.chars.Contains(btn.KeyChar))
                {
                    //if params.char
                    cmd_line = cmd_line.Append(btn.KeyChar);
                  
                }

            }
        }

        private void cmd_processing(string _line)
        {
            string[] patt = _line.Split(' ');
            if (patt[0] == "cd" && patt.Length == 2)
            {
                string @path = patt[1];
                this.path = @path;
                cmd(1);
            }
        }
    }
}
