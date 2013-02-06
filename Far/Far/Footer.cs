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
            

            this.timeWidget();
            this.commandWidget();
        }
        private void timeWidget()
        {
            DateTime now = new DateTime();
            kit.writeChar('1');
            kit.fontColor(ConsoleColor.Green);
            //kit.draw(this.width - 2, this.height - 2, this.width - 1, this.height - 1, '↑');
            kit.fontColor(ConsoleColor.White);
        }
        private void commandWidget()
        {
            kit.setPos(0, this.height - 2);
        }
    }
}