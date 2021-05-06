using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Imitation
{
    public partial class Form1 : Form
    {
        Model mm = new Model(0.5,50);
        public Form1()
        {
            InitializeComponent();
        }
           
            //label3.Text = Convert.ToString(mm.randomEnvironment.TransitionDist[0][0]);
            //label4.Text = Convert.ToString(mm.randomEnvironment.TransitionDist[0][1]);
            //label5.Text = Convert.ToString(mm.randomEnvironment.TransitionDist[0][2]);
            //label6.Text = Convert.ToString(mm.randomEnvironment.TransitionDist[1][0]);
            //label7.Text = Convert.ToString(mm.randomEnvironment.TransitionDist[1][1]);
            //label8.Text = Convert.ToString(mm.randomEnvironment.TransitionDist[1][2]);
            //label9.Text = Convert.ToString(mm.randomEnvironment.TransitionDist[2][0]);
            //label10.Text = Convert.ToString(mm.randomEnvironment.TransitionDist[2][1]);
            //label11.Text = Convert.ToString(mm.randomEnvironment.TransitionDist[2][2]);
        private void button2_Click(object sender, EventArgs e)
        {
            mm.simulate(100000);
            label1.Text = Convert.ToString(mm.current_time);
            label2.Text = Convert.ToString(mm.current_state);
            label3.Text = Convert.ToString(mm.Statistic.Sum());
            label13.Text = Convert.ToString(mm.get_i());
            label14.Text = Convert.ToString(mm.get_v());
            label12.Text = Convert.ToString(mm.randomEnvironment.get_ts());
            label15.Text = Convert.ToString(mm.arrivalProcess.get_ta());
            label16.Text = Convert.ToString(mm.service.findNearest());
        }
    }
}
