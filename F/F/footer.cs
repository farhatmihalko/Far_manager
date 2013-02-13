using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F
{
    /**
     * Class to control the footer string
     */
    partial class Footer
    {
        public int x;
        public int y;
        public int part_size;
        private int space = 0;
        public Footer(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.part_size = Properties.WIDTH / 10 - 1;

            //Allow to paint
            this.Painter();
        }
    }

    partial class Footer
    {
        /**
         * Paint all subfooter
         */
        private void Painter()
        {
            this.fillBg();
            this.drawFooterSubMenu();
        }
        private void fillBg()
        {
            @kit.setBackgroundColor(ConsoleColor.DarkCyan);
            @kit.draw(0, Properties.HEIGHT - 1, Properties.WIDTH, Properties.HEIGHT, ' ');
            @kit.setBackgroundColor(Properties.BG);
        }
        /**
         * Painter
         */
        private void drawFooterSubMenu()
        {
            @kit.setPosition(this.x, this.y);
            drawNumberWithString("F1", "Left");
            drawNumberWithString(" F2", "Right");
            drawNumberWithString(" F3", "View");
            drawNumberWithString(" F4", "Edit");
            drawNumberWithString(" F5", "Copy");
            drawNumberWithString(" F6", "Remove");
            drawNumberWithString(" F7", "MkDir");
            drawNumberWithString(" F8", "Plugins");
            drawNumberWithString(" F9", "Reset");
            drawNumberWithString(" F10", "Exit");
        }
        /**
         * Draw the part of submenu
         * @number to output
         * @text to output
         */
        private void drawNumberWithString(string number, string text)
        {
            @kit.setBackgroundColor(ConsoleColor.Black);
            @kit.setFontColor(ConsoleColor.White);
            @kit.writeString(number);
            @kit.setBackgroundColor(ConsoleColor.DarkCyan);
            @kit.setFontColor(Properties.FONT);
            if (text.Length < this.part_size)
            {
                string str = text;
                int len = this.part_size - text.Length;
                for (int i = 0; i < len; i++)
                    str += " ";
                text = str;
            }
            else
                text = text.Substring(0, this.part_size);
            @kit.writeString(text);
            this.space += text.Length;
            @kit.setPosition(this.space, this.y);
            @kit.setFontColor(Properties.FONT);
            @kit.setBackgroundColor(Properties.BG);
        }
    }
}
