using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OS_Project
{
    
    public partial class Form1: Form
    {
        int w;
        int h;
        int num;
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            w = this.Width;
            h = this.Height;

            label1.Location = new Point((w / 2) - (label1.Width/2) , (h / 4));
            textBox1.Location = new Point((w / 2) - (textBox1.Width / 2), (h / 2));
            button1.Location = new Point((w / 2) - (button1.Width / 2), (h - (h/5)*2));


            bool isNumber = int.TryParse(textBox1.Text, out int result);

            if (textBox1.Text != "" && isNumber == true)
            {
                if (Convert.ToInt16(textBox1.Text) > 0)
                {
                    button1.Enabled = true;
                }
                else
                {
                    button1.Enabled = false;
                }
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bool isNumber = int.TryParse(textBox1.Text, out int result);

            if (textBox1.Text != "" && isNumber == true)
            {
                if (Convert.ToInt16(textBox1.Text) > 0 && Convert.ToInt16(textBox1.Text) <= 7)
                {
                    button1.Enabled = true;
                }
                else
                {
                    button1.Enabled = false;
                }
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            bool isNumber = int.TryParse(textBox1.Text, out int result);

            if (isNumber == true)
            {
                num = Convert.ToInt16(textBox1.Text);
                Form2 f2 = new Form2(num);
                f2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Input");
            }
        }
    }
}
