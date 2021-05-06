using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imitation
{
    class MarkovianChain
    {
        protected double[][] Q;
        //protected double[] R;
        public double[][] TransitionDist;
        protected double ts;
        protected int state;
        protected int next_state;
        protected int S = 3;
        public MarkovianChain()
        {
            Q = new double[][]
            {
                new double[] {-3, 1, 2},
                new double[] {1, -2, 1},
                new double[] {2, 2, -4}
            };

            InitState();
            calculateDist();
        }
        public double get_ts()
        {
            return ts;
        }

        public int get_state()
        {
            return state;
        }
        protected void InitState()
        {
            state = 0;
            next_state = 0;
        }

        protected void calculateDist()
        {
            TransitionDist = new double[S][];
            for(int i = 0; i < S; i++)
            {
                TransitionDist[i] = new double[S];
                for(int j =0; j< S; j++)
                {
                    TransitionDist[i][j] = 0;
                    if (i!=j) TransitionDist[i][j] = -Q[i][j] / Q[i][i];
                } 
            }
        }


        public void nextState(double t)
        {

            state = next_state;
            Random rnd = new Random();
            double a = rnd.NextDouble();
            ts = t + Math.Log(a)/Q[state][state];

            next_state = 0;
            double A = rnd.NextDouble() - TransitionDist[state][next_state];
            while (A>0)
            {
                next_state++;
                A -= TransitionDist[state][next_state];
            }
        }
    }
}
