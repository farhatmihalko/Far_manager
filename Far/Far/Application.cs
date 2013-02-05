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
            _footer.setPath(@"C:\Intel");
            _footer.setPath(@"C:\Intel\Logs");

            while (ST_CODE)
            {
                int left_before_push = Console.CursorLeft;
                int top_before_push = Console.CursorTop;


                ConsoleKeyInfo btn = Console.ReadKey();
                ConsoleKey btn_char = btn.Key;

                int left_after_push = Console.CursorLeft;
                int top_after_push = Console.CursorTop;

                if (left_after_push > _footer.rightMinimalCmd())
                    kit.clearChar(left_before_push, top_before_push);

                if (left_after_push < _footer.leftMinimalCmd())
                    kit.setPos(left_before_push, top_before_push);

                switch (btn_char)
                {
                    case ConsoleKey.Enter:
                        _footer.cmd();
                        kit.draw(_footer.leftMinimalCmd(), top_before_push, _footer.rightMinimalCmd(), top_before_push + 1, ' ');
                        kit.setPos(_footer.leftMinimalCmd(), top_before_push);
                        break;
                    case ConsoleKey.Backspace:
                        kit.writeChar(' ');
                        kit.setPos(Math.Max(_footer.leftMinimalCmd(), left_after_push), top_after_push);
                        if(_footer.CURR_STRING.Length > 0)
                            _footer.CURR_STRING.Remove(_footer.CURR_STRING.Length - 1, 1);
                        break;
                    default:
                        if (@params.chars.Contains(btn.KeyChar))
                        {
                            //s
                            _footer.CURR_STRING.Append(btn.KeyChar);
                        }
                        else
                            kit.clearChar(left_before_push, top_before_push);
                        break;
                }
            }
        }
    }
}