using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Security.Policy;

namespace Bollar_Ny
{
    public partial class Form1 : Form
    {
        public class ball_t
        {
            public ball_t(double r, double x, double y, double vx, double vy)
            {
                R = r;
                X = x;
                Y = y;
                VX = vx;
                VY = vy;
            }

            public double R { get; }
            public double X { get; set; }
            public double Y { get; set; }
            public double VX { get; }
            public double VY { get; }

            public void update(double dt)
            {
                X += VX * dt;
                Y += VY * dt;
            }
            void print()
            {
                //printf("(%f, %f)\n", x, y);
            }
        }

        public Form1()
        {
            InitializeComponent();
            rnd = new Random();
            bollar = slump_bollar(rnd.Next(1, 10));
            timer1.Start();
            physics_update.Start();
        }


        double randu(double low, double high)
        {
            return low + rnd.NextDouble() * (high - low);
        }

        bool val_in(double a, double low, double high)
        {
            if (a < low) return false;
            if (a > high) return false;
            return true;
        }

        double randv(double a, double b, double c, double d)
        {
            while (true)
            {
                double e = randu(a, d);
                if (val_in(e, a, b)) return e;
                if (val_in(e, c, d)) return e;
            }
        }

        ball_t slump_boll()
        {
            // bollar innom 0-1024
            int Rows = 512;
            int Cols = 512;
            double r = 100; randu(25, 75);
            double x = randu(r, Cols - r);
            double y = randu(r, Rows - r);
            double vx = 100;// randv(-100, -30, 30, 100);
            double vy = randv(-100, -30, 30, 100);
            ball_t b = new ball_t(r, x, y, vx, vy);
            return b;
        }

        List<ball_t> slump_bollar(int N)
        {
            List<ball_t> bs = new List<ball_t>();
            for (int i = 0; i < N; ++i)
            {
                bs.Add(slump_boll());
            }
            return bs;
        }


        Random rnd;
        List<ball_t> bollar;



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toss(object sender, PaintEventArgs e)
        {
        }

        int cnt = 0;

        private void draw_ball(ball_t b, PaintEventArgs e)
        {

            //create an ellipse with  
            // Black color  
            // start position = (10, 10)
            // (start position is always Top Left of the object)  
            // width = 75 px, height = 50 px  
            Color black = Color.FromArgb(255, 0, 0, 0);


            System.Drawing.SolidBrush myBrush;
            if (cnt == 0)
                myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            else
                myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            cnt++;
            float x = (float)(b.X - b.R);
            float y = (float)(b.Y - b.R);
            float r = (float)b.R;

            //  e.Graphics.Drawing2D::SmoothingMode::HighQuality
            e.Graphics.FillEllipse(myBrush, x, y, r, r);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Code goes here
            //  base.OnPaint(e);
            Color black = Color.FromArgb(255, 0, 0, 0);
            Pen blackPen = new Pen(black);
            blackPen.Width = 20;
            blackPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            blackPen.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            e.Graphics.DrawLine(blackPen, 300, 200, 800, 200);

            foreach (ball_t b in bollar)
            {
                draw_ball(b, e);
            }


        }


        void dosomething(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Update();
            Invalidate();
            Refresh();
        }

        private void physics_update_Tick(object sender, EventArgs e)
        {
            double dt = 0.01;
            foreach (ball_t b in bollar)
            {
                b.update(dt);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
