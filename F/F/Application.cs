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
    public partial class Application
    {
        Panel _right;
        Panel _left;
        Panel _current;
        string _identification;

        private int footerPos = 0;
        private int progressive = 0;


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
    }

    /*
     * 
     * Painter controller, queue of painting is next:
     * 1) drawBackground
     *
     */
    public partial class Application
    {
        private void Painter()
        {
            this.drawBackground();
            this.drawFooter();
        }

        //private methods
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

    public partial class Application
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

    public partial class Application
    {
        public void PanelController()
        {
            //creating panel
            this._right = new Panel(0, 0, Properties.WIDTH / 2, Properties.HEIGHT - 2);
            this._left = new Panel(Properties.WIDTH / 2 + 1, 0, Properties.WIDTH / 2 - 1, Properties.HEIGHT - 2);
            this._current = this._right;
            this._identification = "right";

            /**
             * Working only with current
             */
            this._current.commander(@"C:\Intel");
            this._left.commander(@"C:\Intel");
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


