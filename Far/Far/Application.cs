using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Far
{
    class Application
    {
        public int width;
        public int height;

        public Application(int _width = 70, int _height = 50)
        {
            this.width = _width;
            this.height = _height;
            
            /*
             * Initliaze all components =>
             * 1) body
             * 2) footer
             */
            this.init();
        }
        public void init()
        {
            /*
             * 
             * initialize the far-window
             * 
             */
            Console.SetBufferSize(this.width, this.height);
            Console.SetWindowSize(this.width, this.height);

            //draw background color
            kit.backgroundColor(ConsoleColor.DarkBlue);
            kit.setPos(0, 0);
            Console.Write(' ');
            for (var i = 0; i < this.height; i++)
                Console.WriteLine("\n");

            @body body = new @body();
            @footer footer = new @footer(this.width, this.height);

            this.controller(body, footer);
        }

        private void controller(@body _body, @footer _footer)
        {
            bool ST_CODE = true;
            _body.setPath(@"C:\");

            while (ST_CODE)
            {
                int left_before_push = Console.CursorLeft;
                int top_before_push = Console.CursorTop;


                ConsoleKeyInfo btn = Console.ReadKey();
                ConsoleKey btn_char = btn.Key;

                int left_after_push = Console.CursorLeft;
                int top_after_push = Console.CursorTop;

                switch (btn_char)
                {
                    case ConsoleKey.UpArrow :
                        _body.moveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        _body.moveDown();
                        break;
                    case ConsoleKey.Enter :
                        _body.open();
                        break;
                    default :
                        break;
                }
            }
        }
    }
}