using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imitation
{
    class Service
    {
        private double[] Mu;
        private List<Request> requests;
        private Resource resource_system;
        public Service()
        {
            Mu = new double[] { 1.5, 1.0, 0.8 };
            resource_system = new Resource();
            requests = new List<Request>();
        }
        public void addRequest(int state,double t)
        {
            Random rnd = new Random();
            double a = rnd.NextDouble();

            double t1 = t - Math.Log(a) / Mu[state];
            double volume = resource_system.calculateVolume(state);
            Request r = new Request(volume, t1);
            requests.Add(r);
        }
        
        public double findNearest()
        {
            double tmin= Double.MaxValue;
            foreach (Request request in requests)
            {
                if (tmin > request.get_tl()) tmin = request.get_tl();
            }
            return tmin;
        }

        public double calculateVolume()
        {
            double vol = 0;
            foreach (Request request in requests)
            {
                vol += request.get_volume();
            }
            return vol;
        }
        private void deleteRequest(int id)
        {
            requests.RemoveAt(id);
        }
        public void serveRequest()
        {
            List<int> toDelete = new List<int>();
            double t = findNearest();
            for (int i = requests.Count-1; i >= 0; i--)
            {
                if (requests[i].get_tl() == t)
                {
                    toDelete.Add(i);
                }
            }
            foreach (int id in toDelete)
            {
                deleteRequest(id);
            }
        }
        public void on_state_shift(int state, double t)
        {
            int n = requests.Count;
            requests.Clear();
            for (int i = 0; i < n; i++)
            {
                addRequest(state, t);
            }
        }
        public int calculate_requests()
        {
            int n = requests.Count;
            return n;
        }
    }
}
