using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

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

    /**
     * Driver choosing box
     */
    class selectDriver : box
    {
        public selectDriver(int x, int y, int width, int height, Application app)
            :base(x, y, width, height, app)
        {
        }

        public void init()
        {
            this.draw();
            this.select();
        }

        //
        private void draw()
        {
            @kit.setBackgroundColor(ConsoleColor.DarkCyan);
            @kit.draw(this.x, this.y, this.x + this.width, this.y + this.height, ' ');
            @kit.setBackgroundColor(Properties.BG);
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
        }
        private void select()
        {
            Console.CursorVisible = false;
            bool start = false;
            ArrayList container = load();
            foreach (line ll in container)
            {
                ll.drawLine();
            }
            while (true)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow :
                        this.select(-1, container);
                        break;
                    case ConsoleKey.DownArrow :
                        this.select(+1, container);
                        break;
                    case ConsoleKey.Enter:
                        this.enter(container);
                        start = true;
                        break;
                }
                if (start)
                    break;
            }
            Console.CursorVisible = true;
        }
        private ArrayList load()
        {
            string[] list = Environment.GetLogicalDrives();
            ArrayList container = new ArrayList();
            int _y =  1;
            string last_good = "";
            foreach (string ll in list)
            {
                var drive = new DriveInfo(ll);
                if(drive.IsReady){
                    var information = ll;
                    for (int i = 0; i < this.width / 12 * 3; i++)
                        information += " ";
                    information += @kit.sizeReduce(drive.TotalSize.ToString());
                    for (int i = 0; i < this.width / 12 * 3; i++)
                        information += " ";
                    information += @kit.sizeReduce(drive.AvailableFreeSpace.ToString());
                    container.Add(new line(this.x + 2, this.y + 1 + _y++, this.width - 4, true, information, ll));
                    last_good = ll;
                }
                else
                    container.Add(new line(this.x + 2, this.y + 1 + _y++, this.width - 4, true, ll +" not ready", last_good));
            }
            return container;
        }
        private void select(int number, ArrayList container)
        {
            int index = 0;
            for (int i = 0; i < container.Count; i++)
            {
                line ll = (line)container[i];
                if (ll.current)
                {
                    index = i + number;
                    if(index < 0)
                        index = container.Count - 1;
                    else if(index >= container.Count)
                        index = 0;
                    ll.current = false;
                    ll.changeBackground(ConsoleColor.DarkCyan, ConsoleColor.White);
                    line llnn = (line)container[index];
                    llnn.current = true;
                    llnn.changeBackground(ConsoleColor.Black);
                    break;
                }
            }
        }
        private void enter(ArrayList container)
        {
            for (int i = 0; i < container.Count; i++)
            {
                line ll = (line)container[i];
                if (ll.current)
                {
                    this.app._current.clearing();
                    this.app._current.draw();
                    this.app._current.ob_left.draw(ll.destination);
                    break;
                }
            }
        }
    }
}
