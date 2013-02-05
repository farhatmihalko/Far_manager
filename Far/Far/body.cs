using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Far
{
    class @body
    {
        public @body()
        {
            this.draw();
        }

        private void draw()
        {
            kit.draw(1, 0, @params.WIDTH-1, 1, '═');
            kit.draw(1, @params.HEIGHT - 3, @params.WIDTH - 1, @params.HEIGHT - 2, '═');
            kit.draw(0, 1, 1, @params.HEIGHT - 3, '░');
            kit.draw(@params.WIDTH - 1, 1, @params.WIDTH, @params.HEIGHT - 3, '░');

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
            kit.draw(@params.WIDTH / 2, 1, @params.WIDTH / 2 + 1, @params.HEIGHT - 3, '│');
        }
    }
}
