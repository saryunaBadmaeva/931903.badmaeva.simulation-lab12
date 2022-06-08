using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAb12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double[,] Q = { 
            { -0.4, 0.3, 0.1 }, 
            { 0.4, -0.8, 0.4 }, 
            { 0.1, 0.4, -0.5 } 
        };
        double[] probs = new double[3];
        string[] states = { "Clear", "Cloudy", "Overcast" };

        int T, N, state = 0;
        double dt, t = 0;

        double[] freq = new double[3];

        int k = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (k < N)
            {
                if (t < T)
                {
                    dt = Math.Log(rnd.NextDouble()) / Q[state, state];
                    t += dt;
                    chart1.Series[0].Points.AddXY(t, state + 1);

                    for (int i = 0; i < states.Length; i++)
                        if (i != state)
                            probs[i] = -Q[state, i] / Q[state, state];
                        else
                            probs[i] = 0;

                    probability = rnd.NextDouble();
                    for (int i = 0; i < 3; i++)
                    {
                        probability -= probs[i];
                        if (probability <= 0)
                        {
                            state = i;
                            break;
                        }
                    }

                    chart1.Series[0].Points.AddXY(t, state + 1);
                }
                else
                {
                    freq[state]++;
                    state = 0;
                    k++;
                    t = 0;
                    chart1.Series[0].Points.Clear();
                    chart1.Series[0].Points.AddXY(t, state + 1);
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    freq[i] = freq[i] / N;
                    chart2.Series[0].Points.AddXY(i + 1, freq[i]);
                }
                timer1.Stop();
            }
        }

        Random rnd = new Random();
        double probability;
        private void btStart_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(t, state + 1);
            chart2.Series[0].Points.Clear();
            T = (int)edT.Value;
            N = (int)edN.Value;

          
            timer1.Start();
        }

    }
}
