using System;
using System.Windows.Forms;

namespace Perceptron
{
    public partial class Form1 : Form
    {
        private double bias;
        private int epochs;
        private double learningRate;
        private double[] weights;
        private double error;

        public Form1()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            decimal value = Convert.ToDecimal(trackBar1.Value) / 10000m;
            label7.Text = value.ToString();
        }

        public void GenerateWeights(int numInputs)
        {
            weights = new double[numInputs];

            var r = new Random();
            for (int i = 0; i < numInputs; i++)
            {
                weights[i] = r.NextDouble() * 2 - 1; //random numbers from -1 to 1
            }
        }

        public double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        public double Activation(double[] inputs)
        {
            GenerateWeights(inputs.Length);
            double sum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                sum += weights[i] * inputs[i];
            }
                
            sum += bias;

            return Sigmoid(sum) > 0 ? 1 : 0;
        }

        public void Train(double[][] inputs, int[] targets)
        {
            for (int epoch = 0; epoch < epochs; epoch++)
            {
                textBox3.AppendText(Environment.NewLine + Environment.NewLine + "Epoch: " + epoch + Environment.NewLine);
                for (int i = 0; i < inputs.Length; i++)
                {
                    double prediction = Activation(inputs[i]);
                    error = targets[i] - prediction;

                    for (int j = 0; j < weights.Length; j++)
                    {
                        weights[j] += learningRate * error * inputs[i][j];  
                    }

                    bias += learningRate * error;
                }
                textBox3.AppendText("\tw1: " + weights[0] + Environment.NewLine);
                textBox3.AppendText("\tw2: " + weights[1] + Environment.NewLine);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            learningRate = Convert.ToDouble(label7.Text);
            epochs = Convert.ToInt32(textBox4.Text);

            double[][] inputs = new double[4][];
            inputs[0] = new double[] { 0, 0 };
            inputs[1] = new double[] { 0, 1 };
            inputs[2] = new double[] { 1, 0 };
            inputs[3] = new double[] { 1, 1 };

            int[] targets = new int[] { 0, 0, 0, 1 };

            Train(inputs, targets);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int input1 = Convert.ToInt32(textBox1.Text);
            int input2 = Convert.ToInt32(textBox2.Text);

            label4.Text = Activation(new double[] {input1, input2}).ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
