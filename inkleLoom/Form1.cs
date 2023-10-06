using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace inkleLoom {

    public partial class Form1 : Form {

        List<Thread> threads = new List<Thread>();
        List<Thread> pattern = new List<Thread>();

        Bitmap bmp, pal;

        public Form1() {
            InitializeComponent();
            cntThreads.Text = "0";
            bmp = new Bitmap(picBox.Width, picBox.Height);
            Graphics.FromImage(bmp).Clear(Color.White);

            updateBitmap();

            createPalette();
        }

        private void createPalette() {
            pal = new Bitmap(720, 20);
            Graphics gr = Graphics.FromImage(pal);
            int t = 0;

            for (int z = 0; z < 360; z++) {
                Pen p = new Pen(HSLtoRGB(z, 1, 1));

                gr.DrawLine(p, t, 0, t, 20);
                t++;
                gr.DrawLine(p, t, 0, t, 20);
                t++;
            }

            palette.Image = pal;
        }

        private void trackR_Scroll(object sender, EventArgs e) {
            int r = trackR.Value,
                g = trackG.Value,
                b = trackB.Value;
            color.BackColor = Color.FromArgb(255, r, g, b);

            labelR.Text = r.ToString();
            labelG.Text = g.ToString();
            labelB.Text = b.ToString();
        }

        private Color HSLtoRGB(double h, double s, double b) {
            Func<double, double> k = (n) => (n + h / 60) % 6;
            Func<double, double> f = (n) => b * (1 - s * Math.Max(0, Math.Min(Math.Min(k(n), 4 - k(n)), 1)));

            return Color.FromArgb(255, (int)(255 * f(5)), (int)(255 * f(3)), (int)(255 * f(1)));
        }

        private void Form1_Shown(object sender, EventArgs e) {
            trackR.Value = trackG.Value = trackB.Value = 0;
        }

        private void btnCreate_Click(object sender, EventArgs e) {
            int cnt = Convert.ToInt32(cntThreads.Text);
            if (cnt < 4) return;

            threads.Clear();
            
            int c = cnt / 2, y = 16;

            for (int w = 0; w < 5; w++) {
                int x = 10;
                for (int i = 0; i < c + (cnt % 2 == 0 ? 0 : 1); i++) {
                    Thread t = new Thread();
                    t.Type = Type.HEDDLED;
                    t.setPosition(x, y);
                    x += Thread.THREAD_WID;
                    threads.Add(t);
                }

                x = 10 + Thread.THREAD_WID / 2;
                for (int i = 0; i < c; i++) {
                    Thread t = new Thread();
                    t.Type = Type.UNHEDDLED;
                    t.setPosition(x, y + Thread.THREAD_HEI);
                    x += Thread.THREAD_WID;
                    threads.Add(t);
                }

                y += 2 * Thread.THREAD_HEI;
            }

            createPattern(cnt);

            updateBitmap();
        }

        private void createPattern(int cnt) {

            pattern.Clear();

            int c = cnt / 2, d = c + (cnt % 2 == 0 ? 1 : 0);

            for (int i = 0; i < c; i++) {
                pattern.Add(threads[i]);
                pattern.Add(threads[i + d]);
            }
        }

        private void cntThreads_TextChanged(object sender, EventArgs e) {
            if (cntThreads.Text.Length < 1) cntThreads.Text = "0";
        }

        private void updateBitmap() {
            Graphics gr = Graphics.FromImage(bmp);
            gr.Clear(Color.White);
            
            foreach (Thread t in threads) {
                t.draw(gr);
            }

            foreach (Thread t in pattern) {
                t.draw(gr, true);
            }

            picBox.Image = bmp;
        }

        private void picBox_MouseDown(object sender, MouseEventArgs e) {
            foreach(Thread t in threads) {
                if(t.Rect.Contains(e.Location)) {
                    int x = t.Rect.X;
                    foreach(Thread tt in threads) {
                        if(tt.Rect.X == x) {
                            tt.Color = color.BackColor;
                        }
                    }

                    updateBitmap();
                    return;
                }
            }
        }

        private void palette_MouseDown(object sender, MouseEventArgs e) {
            Color c = pal.GetPixel(e.X, e.Y);
            color.BackColor = c;
            trackR.Value = c.R;
            trackG.Value = c.G;
            trackB.Value = c.B;
        }
    }
}
