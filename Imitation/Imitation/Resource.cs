using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imitation
{

    class Resource
    {
        private Func<double,double>[] G = { fun1, fun2, fun3};

        static private double[] alpha = { 0.5, 1.5, 2 };
        static private double[] beta = { 0.5, 1.5, 2 };
        static public double gamma_d(double d)
        {
            double r = 2.71 / (2.71 + d);
            double n1;
            double n2;
            Random rnd = new Random();
            do {
                double a1 = rnd.NextDouble();
                double a2 = rnd.NextDouble();
                if (a1 <= r)
                {
                    n1 = Math.Pow((a1 / r), (1 / d));
                    n2 = a2 * Math.Pow(n1, (d - 1));
                }
                else
                {
                    n1 = 1 - Math.Log((a1 - r) / (1 - r));
                    n2 = a2 * Math.Pow(2.71, -n1);
                }
            } while (n2>Math.Pow(n1,(d-1))*Math.Pow(2.71,-n1));
            return n1;                   
        }
        static private double fun1(double a)
        {
            int i = (int)alpha[0];
            double d = alpha[0] - i;
            double e = 0;
            if (d > 0) {
                e = gamma_d(d);
            }

            double ans = 0;
            Random rnd = new Random();
            double b = 0;
            for (int j = 0; j < i; j++)
            {
                b = rnd.NextDouble();
                ans -= Math.Log(b);
            }
            return ((ans+e)/beta[0]);
        }
        static private double fun2(double a)
        {
            int i = (int)alpha[1];
            double d = alpha[1] - i;
            double e = 0;
            if (d > 0)
            {
                e = gamma_d(d);
            }

            double ans = 0;
            Random rnd = new Random();
            double b = 0;
            for (int j = 0; j < i; j++)
            {
                b = rnd.NextDouble();
                ans -= Math.Log(b);
            }
            return ((ans + e) / beta[1]);
        }
        static private double fun3(double a)
        {
            int i = (int)alpha[2];
            double d = alpha[2] - i;
            double e = 0;
            if (d > 0)
            {
                e = gamma_d(d);
            }

            double ans = 0;
            Random rnd = new Random();
            double b = 0;
            for (int j = 0; j < i; j++)
            {
                b = rnd.NextDouble();
                ans -= Math.Log(b);
            }
            return ((ans + e) / beta[2]); 
        }

        public double calculateVolume(int state)
        {
            Random rnd = new Random();
            double a = rnd.NextDouble();
            return G[state].Invoke(a);
        }
    }
}
