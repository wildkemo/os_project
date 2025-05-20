using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OS_Project
{
    public partial class Form2: Form
    {
        Timer timer = new Timer();

        List<Philosipher> phs = new List<Philosipher>();
        List<Spoon> spoons = new List<Spoon>();
        List<Button> buttons = new List<Button>();
        List<TextBox> txts = new List<TextBox>();




        int num;
        bool startTick = false;
        public Form2(int num)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Paint += Form2_Paint;
            this.num = num;
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(startTick == true)
            {
                for(int i = 0; i < phs.Count; i++)
                {
                    if (phs[i].eating == true)
                    {
                        if (phs[i].time > 0)
                        {
                            phs[i].time--;
                        }
                        else
                        {
                            signal(i);
                        }

                        drawscene(this.CreateGraphics());
                    }
                }
                
            }

        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            drawscene(g);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedBtn = sender as Button;
            int index = (int)clickedBtn.Tag;

            //bool isNumber = int.TryParse(txts[index].Text, out int result);



            if (txts[index].Text != "" && Convert.ToInt16(txts[index].Text) > 0)
            {
                wait(spoons[index], spoons[(index + 1) % num], index, Convert.ToInt16(txts[index].Text));

                //eat

                //signal();

                //think
            }


        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer.Start();
            for(int i = 0; i < num; i++)
            {
                Philosipher ph = new Philosipher();
                ph.pos = new Point(100 + (i * 210), (this.Height / 2) - 250);
                //ph.img.Width = 100;

                Spoon spn = new Spoon();
                spn.pos = ph.pos;
                spn.pos.X = spn.pos.X - 40;
                spn.pos.Y = spn.pos.Y + 100;
                spoons.Add(spn);
                phs.Add(ph);


                TextBox txt = new TextBox();
                txt.Location = new Point(100 + (i * 210), (this.Height / 2) - 50);
                this.Controls.Add(txt);
                txts.Add(txt);

                // Create new button
                Button btn = new Button
                {
                    Text = "Eat",
                    Width = 100,
                    Height = 40,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold), // Bigger and bold
                    Location = new Point(100 + (i * 210), this.Height/2), // Spread horizontally
                    Tag = i // Store the index
                };

                // Add Click event
                btn.Click += Button_Click;

                // Add to form and list
                this.Controls.Add(btn);
                buttons.Add(btn);
            }

            drawscene(this.CreateGraphics());
        }

        private void wait(Spoon spoon1, Spoon spoon2, int index, int time)
        {
            if(spoon1.available == true && spoon2.available == true)
            {
                spoon1.available = false;
                spoon1.pos.X = phs[index].pos.X;

                spoon2.available = false;
                spoon2.pos.X = phs[index].pos.X+20;



                phs[index].eating = true;
                phs[index].img = new Bitmap(new Bitmap("eating.png"), new Size(100, 150));
                phs[index].time = time;
                startTick = true;



            }
            drawscene(this.CreateGraphics());
        }

        private void signal(int i)
        {
            for(int j = 0; j < spoons.Count; j++)
            {
                if (j == i)
                {
                    spoons[j].available = true;
                    spoons[j].pos.X = phs[j].pos.X - 40;


                    spoons[(j + 1) % num].available = true;
                    spoons[(j + 1) % num].pos.X = phs[(j + 1) % num].pos.X - 40;



                    phs[j].eating = false;
                    phs[j].img = new Bitmap(new Bitmap("thinking.png"), new Size(100, 150));

                }
            }

            drawscene(this.CreateGraphics());
        }

        private void drawscene(Graphics g)
        {
            g.Clear(Color.Black);

            for(int i = 0; i < phs.Count; i++)
            {
                g.DrawImage(phs[i].img, phs[i].pos);
                Point timepoint = phs[i].pos;
                timepoint.Y = timepoint.Y - 100;
                g.DrawString(phs[i].time.ToString(), new Font("Arial", 22), Brushes.White, timepoint);
                g.DrawImage(spoons[i].img, spoons[i].pos);

            }
        }

    }

    public partial class Philosipher
    {
        public Point pos;
        public Bitmap img = new Bitmap(new Bitmap("thinking.png"), new Size(100, 150));
        public bool eating = false;
        public int time = 0;

    }

    public partial class Spoon
    {
        public Point pos;
        public Bitmap img = new Bitmap(new Bitmap("spoon.png"), new Size(25, 50));
        public bool available = true;
    }
}
