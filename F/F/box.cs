using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F
{
    /**
     * Item to show message boxes, error boxes and other
     * @x starting x
     * @y starting y
     * @width total width
     * @height total height
     */
    class box
    {
        public int x;
        public int y;
        public int width;
        public int height;
        public Application app;

        //constructor
        public box(int x, int y, int width, int height, Application app)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.app = app;
        }

        public void init()
        {
            this.draw();
        }
        public void draw()
        {}
        public void borders()
        {}
    }

    /**
     * Extended box class, to show error
     * @content the text of error message
     */
    class errorBox :box
    {
        public string content;
        public errorBox(int x, int y, int width, int height, string content, Application app)
            : base(x, y, width, height, app)
        {
            this.content = content;
        }
        public void init()
        {
            this.draw();
            this.borders();
            this.printText();
            this.commander();
        }
        public void draw()
        {
            @kit.setBackgroundColor(ConsoleColor.DarkRed);
            @kit.setFontColor(ConsoleColor.White);
            @kit.draw(this.x, this.y, this.x + this.width, this.y + this.height, ' ');
        }
        public void borders()
        {
            @kit.draw(this.x, this.y, this.x + 1, this.y + this.height, '║');
            @kit.draw(this.x - 1 + this.width, this.y, this.x + this.width, this.y + this.height, '║');
            @kit.draw(this.x, this.y, this.x + this.width, this.y + 1, '═');
            @kit.draw(this.x, this.y - 1 + this.height, this.x + this.width, this.y + this.height, '═');
            @kit.setPosition(this.x, this.y);
            @kit.writeChar('╔');
            @kit.setPosition(this.x + this.width - 1, this.y);
            @kit.writeChar('╗');
            @kit.setPosition(this.x, this.y + this.height - 1);
            @kit.writeChar('╚');
            @kit.setPosition(this.x + this.width - 1, this.y + this.height - 1);
            @kit.writeChar('╝');
            @kit.setPosition(this.x + this.width / 2 - "Error".Length / 2 - 1, this.y);
            @kit.writeLine("Error");
        }
        private void printText()
        {
            int number_x = this.x + this.width / 6;
            int number_y = this.y + this.height / 10 + 1;
            foreach (char ch in this.content)
            {
                @kit.setPosition(number_x, number_y);
                @kit.writeChar(ch);
                if (number_x > this.width + this.x - this.width / 6)
                {
                    number_y++;
                    number_x = this.x + this.width / 6;
                }
                if (number_y > this.y + this.height - this.height / 10)
                    break;
                number_x++;
            }
        }
        private void commander()
        {
            lock (this.app.locker)
            {
                Console.CursorVisible = false;
                while (true)
                {
                    bool st = false;
                    var key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.Escape:
                        case ConsoleKey.Enter:
                            this.app.init();
                            st = true;
                            break;
                    }
                    if (st)
                        break;
                }
            }
        }
    }

    class selectDriver : box
    {
        public selectDriver(int x, int y, int width, int height, Application app)
            :base(x, y, width, height, app)
        {
        }

        public void init()
        {
        }

        //
        private void draw()
        {
        }
    }
}
