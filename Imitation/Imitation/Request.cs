using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imitation
{
    class Request
    {
        private double volume;
        private double time_when_left;

        public Request(double v, double t)
        {
            volume = v;
            time_when_left = t;
        }

        public double get_tl()
        {
            return time_when_left;
        }
        public double get_volume()
        {
            return volume;
        }

    }
}
