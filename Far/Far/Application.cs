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

        public Application(int _width = 100, int _height = 50)
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
            @body body = new @body();
            @footer footer = new @footer();
        }
    }
}
