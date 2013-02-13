using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F
{
    /*
     * 
     * Global controller, construct all application,
     * get the defaults params from => Properties class
     * 
     */
    partial class Application
    {
        public Panel _right;
        public Panel _left;
        public Panel _current;
        public string _identification;

        private int footerPos = 0;
        private int progressive = 0;

        public object locker =  new Object();

        //constructor
        public Application()
        {

        }

        public void init()
        {
            //name of window and cursor size
            Console.Title = Properties.TITLE;
            Console.CursorSize = Properties.CURSOR_SIZE;

            //without any scrolls
            Console.SetWindowSize(Properties.WIDTH, Properties.HEIGHT);
            Console.SetBufferSize(Properties.WIDTH, Properties.HEIGHT);

            //call painter
            this.Painter();

            //call panel creates
            this.PanelController();

            //call keyboad controller
            this.Controller();
        }

        //reinialization
        public void reInitialization()
        {
        }
        public void reInitialization(string pathLeft, string pathRight)
        {
            this.Painter();
            @kit.setBackgroundColor(Properties.BG);

            //call panel creates
            this.PanelController();
            
            this.Controller();
        }
        //loading new driver
        public void newDeviceInit(string _choose)
        {
            
            if (_choose == "left")
            {
                this._current = this._right;
            }
            else if (_choose == "right")
            {
                this._current = this._left;
            }
            this._current.selectDriver();
        }
    }

    /*
     * 
     * Painter controller, queue of painting is next:
     * 1) drawBackground
     *
     */
    partial class Application
    {
        public void Painter()
        {
            this.drawBackground();
            this.drawSubFooter();
            this.drawFooter();
        }

        //private methods
        private void drawSubFooter()
        {
            Footer ft = new Footer(0, Properties.HEIGHT - 1);

        }
        private void drawBackground()
        {
            @kit.setBackgroundColor(Properties.BG);
            @kit.setFontColor(Properties.FONT);
            @kit.setPosition(0, 0);
            @kit.writeChar(' ');
            for (int i = 0; i < Properties.HEIGHT; i++)
            {
                Console.WriteLine("\n");
            }
        }
        private void drawFooter()
        {
            @kit.setPosition(0, Properties.HEIGHT - 2);
            @kit.setBackgroundColor(ConsoleColor.Black);
            @kit.draw(0, Properties.HEIGHT - 2, Properties.WIDTH, Properties.HEIGHT - 1, ' ');
            @kit.setBackgroundColor(Properties.BG);
            //set position
            @kit.setPosition(0, Properties.HEIGHT - 2);
        }
    }

    partial class Application
    {
        private void Controller()
        {
            @kit.setPosition(0, Properties.HEIGHT - 2);
            this.setToFooterString();
            while (true)
            {
                @kit.setBackgroundColor(ConsoleColor.Black);
                ConsoleKeyInfo btn = Console.ReadKey();

                //suppose we draw all interface
                switch (btn.Key)
                {
                    case ConsoleKey.Enter:
                        this._current.open();
                        this.setToFooterString();
                        break;
                    case ConsoleKey.UpArrow:
                        this._current.up();
                        break;
                    case ConsoleKey.DownArrow:
                        this._current.down();
                        break;
                    case ConsoleKey.Tab:
                        this.TabPanel();
                        break;
                    case ConsoleKey.F1 :
                        this.newDeviceInit("left");
                        break;
                    case ConsoleKey.F2:
                        this.newDeviceInit("right");
                        break;
                    case ConsoleKey.F11:
                        this.reInitialization();
                        break;
                    case ConsoleKey.F6:
                        this._current.removeSelection();
                        break;
                    case ConsoleKey.F12:
                        errorBox error = new errorBox(Properties.WIDTH / 4, Properties.HEIGHT / 4, Properties.WIDTH /4 * 2, 8, 
                            this._current.current_path + ", debag mode! Please push enter or escape to return to app", this);
                        error.init();
                        break;
                    case ConsoleKey.F10:
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.F9:
                        this._current.refresh();
                        break;
                    case ConsoleKey.Backspace :
                        int left_before_back = Console.CursorLeft;
                        int top_before_back = Console.CursorTop;
                        @kit.setPosition(left_before_back, top_before_back);
                        @kit.writeChar(' ');
                        this.progressive --;
                        break;
                    default :
                        int left_before = Console.CursorLeft;
                        int top_before = Console.CursorTop;
                        if (this.progressive > Properties.WIDTH - 20)
                        {
                            @kit.setPosition(left_before - 1, top_before);
                            @kit.writeChar(' ');
                            @kit.setPosition(left_before - 1, top_before);
                            break;
                        }
                        bool finder = false;
                        char _char_ = '?';
                        for (int i = 0; i < Properties.chars.Length; i++)
                            if (Properties.chars[i] == btn.KeyChar)
                            {
                                finder = true;
                                _char_ = Properties.chars[i];
                                break;
                            }
                        if (finder)
                        {
                            @kit.setPosition(left_before - 1, top_before);
                            @kit.writeChar(_char_);
                            this.progressive++;
                        }
                        else
                        {
                            @kit.setPosition(left_before - 1, top_before);
                            @kit.writeChar(' ');
                            @kit.setPosition(left_before - 1, top_before);
                        }
                        break;
                }
                if (this.progressive < 0)
                    this.progressive = 0;
                @kit.setPosition(this.footerPos + 1 + this.progressive, Properties.HEIGHT - 2);
            }
        }
    }

    partial class Application
    {
        public void PanelController()
        {
            //creating panel
            this._right = new Panel(0, 0, Properties.WIDTH / 2, Properties.HEIGHT - 2, this);
            this._left = new Panel(Properties.WIDTH / 2 + 1, 0, Properties.WIDTH / 2 - 1, Properties.HEIGHT - 2, this);
            this._current = this._right;
            this._identification = "right";

            /**
             * Working only with current
             */
            this._current.commander(@"C:\Intel");
            this._left.commander(@"D:\");
        }
        public void TabPanel()
        {
            if (this._identification == "right")
            {
                this._identification = "left";
                this._current = this._left;
            }
            else
            {
                this._identification = "right";
                this._current = this._right;
            }

            //adding to footer name
            this.setToFooterString();
        }
    }

    //working with footer
    partial class Application
    {
        private void setToFooterString()
        {
            string footerPath = this._current.current_path;
            @kit.setBackgroundColor(ConsoleColor.Black);
            @kit.draw(0, Properties.HEIGHT - 2, Properties.WIDTH, Properties.HEIGHT - 1, ' ');

            @kit.setPosition(0, Properties.HEIGHT - 2);
            @kit.writeLine(footerPath);
            this.footerPos = footerPath.Length;
            @kit.setPosition(this.footerPos + 1, Properties.HEIGHT - 2);
            this.progressive = 0;
        }
    }
}


