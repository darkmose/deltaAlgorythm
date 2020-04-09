using System;
using System.Windows.Forms;

namespace deltaAlgorythm
{
    public partial class Form1 : Form
    {
        double[,] Wi = new double[2, 2];
        double[] Xi = new double[2];
        double[] Yi = new double[2];
        double[] YiLearn = new double[2];
        double[] ERRi = new double[2];

        double[,] DataSet = new double[3, 4] { { 0, 1, 0.2, 0.8 }, { 1, 0, 0.2, 0.2 }, { 1, 0, 0.8, 0.8 } };


        double learnRate = 0.8;

        public Form1()
        {
            InitializeComponent();
        }


        double Linear(double x, double k)
        {
            return x * k;
        }

        double HyperTang(double x, double k = 1)
        {
            double ch = Math.Exp((x / k)) + Math.Exp((x / k * -1));
            double zn = Math.Exp((x / k)) - Math.Exp((x / k * -1));
            if (zn != 0)
            {
                return ch / zn;
            }
            return 0;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Random rand = new Random(Convert.ToInt32(DateTime.Now.Millisecond));
            for (int i = 0; i < 2; i++)   //Случайные веса
            {
                for (int j = 0; j < 2; j++)
                {
                    Wi[i, j] = rand.NextDouble();
                }
            }
            WeightAnalyze();
        }

        void WeightAnalyze()
        {
            label10.Text = Convert.ToString(Wi[0, 0]);       
            label11.Text = Convert.ToString(Wi[0, 1]);       
      
            label13.Text = Convert.ToString(Wi[1, 0]);       
            label14.Text = Convert.ToString(Wi[1, 1]);            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Введите необходимые данные");
                return;
            }

            //Use

            Xi[0] = Convert.ToDouble(textBox1.Text);
            Xi[1] = Convert.ToDouble(textBox2.Text);

            Yi[0] = Xi[0] * Wi[0, 0] + Xi[1] * Wi[1, 0];
            Yi[1] = Xi[0] * Wi[0, 1] + Xi[1] * Wi[1, 1];

            Yi[0] = HyperTang(Yi[0]);
            Yi[1] = HyperTang(Yi[1]);

            label7.Text = Convert.ToString(Yi[0]);
            label8.Text = Convert.ToString(Yi[1]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Learn

            for (int s = 0; s < 100; s++)
            {             
                for (int a = 0; a < 3; a++)
                {
                    Xi[0] = DataSet[a,0];
                    Xi[1] = DataSet[a,1];
                    YiLearn[0] = DataSet[a,2];
                    YiLearn[1] = DataSet[a,3];

                    Yi[0] = Xi[0] * Wi[0, 0] + Xi[1] * Wi[1, 0] +1;
                    Yi[1] = Xi[0] * Wi[0, 1] + Xi[1] * Wi[1, 1] +1;

                    Yi[0] = HyperTang(Yi[0]);
                    Yi[1] = HyperTang(Yi[1]);

                    ERRi[0] = YiLearn[0] - Yi[0];
                    ERRi[1] = YiLearn[1] - Yi[1];


                    for (int k = 0; k < 2; k++)
                    {
                        for (int l = 0; l < 2; l++)
                        {
                            Wi[k, l] += learnRate * Xi[k] * ERRi[l];
                        }
                    }
                    WeightAnalyze();

                    double sqrErr = ERRi[0] * ERRi[0] + ERRi[1] * ERRi[1];
                    label20.Text = Convert.ToString(sqrErr);
                    chart1.Series[0].Points.Add(sqrErr);
                }                    
                
            }            
        }
    }
}
