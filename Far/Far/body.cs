using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Far
{
    class @body
    {
        _panel right;
        _panel left;

        public @body()
        {
            this.draw();
            this.content();
        }


        /*
         * 
         * Public methods
         * 
         */
        public void setPath(string path)
        {

        }

        /*
         * 
         * Private methods
         * 
         */
        private void draw()
        {

            kit.fontColor(ConsoleColor.Cyan);

            kit.draw(1, 0, @params.WIDTH-1, 1, '═');
            kit.draw(1, @params.HEIGHT - 3, @params.WIDTH - 1, @params.HEIGHT - 2, '═');
            kit.draw(0, 1, 1, @params.HEIGHT - 3, '│');
            kit.draw(@params.WIDTH - 1, 1, @params.WIDTH, @params.HEIGHT - 3, '│');

            //drawing borders
            kit.setPos(0, 0);
            kit.writeChar('╒');
            kit.setPos(0, @params.HEIGHT-3);
            kit.writeChar('╘');
            kit.setPos(@params.WIDTH -1, 0);
            kit.writeChar('╕');
            kit.setPos(@params.WIDTH -1, @params.HEIGHT-3);
            kit.writeChar('╛');

            //divide into two windows
            kit.draw(@params.WIDTH / 2, 1, @params.WIDTH / 2 + 1, @params.HEIGHT - 3, '║');
            kit.setPos(@params.WIDTH/2, 0);
            kit.writeChar('╦');
            kit.setPos(@params.WIDTH/2, @params.HEIGHT -3);
            kit.writeChar('╩');
        }
        private void content()
        {
            this.right = new _panel(1, 1, @params.WIDTH / 2 -1, @params.HEIGHT - 5);
            this.left = new _panel(@params.WIDTH /2 + 1, 1, @params.WIDTH /2 - 2, @params.HEIGHT -5);
            //now set timerwidget
            this.setTimerWidget();
        }
        private void setTimerWidget()
        {
            string current_time = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
            ConsoleColor before_font = kit.FontColor;
            ConsoleColor before_bg = kit.BGColor;
            kit.colors(ConsoleColor.DarkCyan, ConsoleColor.Black) ;
            kit.setPos(0, 0);
            kit.writeString(current_time);
            kit.colors(before_bg, before_font);
        }
    }
}
