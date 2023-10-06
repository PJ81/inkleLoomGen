using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace inkleLoom {

    public partial class Form1 : Form {

        private List<Thread> threads = new List<Thread>();
        private Dictionary<int, int> dic = new Dictionary<int, int>();
        private int xThreadsCounter;
        private Bitmap bmp = null, pal;
        readonly List<Thread> pattern = new List<Thread>();
        readonly Font fnt = new Font("Consolas", 12);
        readonly SolidBrush sb = new SolidBrush(Color.Black);
        readonly Pen pen = new Pen(Color.Black);

        public Form1() {
            this.InitializeComponent();
            this.cntThreads.Text = "0";
            this.createPalette();
        }

        private void Form1_Shown(object sender, EventArgs e) {
            this.trackR.Value = this.trackG.Value = this.trackB.Value = 0;
        }

        private void palette_MouseDown(object sender, MouseEventArgs e) {
            Color c = this.pal.GetPixel(e.X, e.Y);
            this.color.BackColor = c;
            this.trackR.Value = c.R;
            this.trackG.Value = c.G;
            this.trackB.Value = c.B;
        }

        private void trackR_Scroll(object sender, EventArgs e) {
            int r = this.trackR.Value,
                g = this.trackG.Value,
                b = this.trackB.Value;
            this.color.BackColor = Color.FromArgb(255, r, g, b);

            this.labelR.Text = r.ToString();
            this.labelG.Text = g.ToString();
            this.labelB.Text = b.ToString();
        }

        private void picBox_MouseDown(object sender, MouseEventArgs e) {
            int x;
            foreach (Thread t in this.threads) {
                if (t.Rect.Contains(e.Location)) {
                    x = t.Rect.X;
                    if (e.Button == MouseButtons.Right) {
                        foreach (Thread tt in this.threads) {
                            if (tt.Rect.X == x) {
                                tt.clearColor();
                            }
                        }
                    }
                    else {
                        foreach (Thread tt in this.threads) {
                            if (tt.Rect.X == x) {
                                tt.Color = this.color.BackColor;
                            }
                        }
                    }

                    this.counter();

                    this.updateBitmap();

                    return;
                }
            }

            foreach (Thread t in pattern) {
                if (t.RectP.Contains(e.Location)) {
                    Color c = t.Color;
                    this.color.BackColor = c;
                    this.trackR.Value = c.R;
                    this.trackG.Value = c.G;
                    this.trackB.Value = c.B;

                    return;
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e) {
            int cnt = Convert.ToInt32(this.cntThreads.Text);
            if (cnt < 4) return;

            int p = 70 + cnt * Thread.THREAD_HEI;
            xThreadsCounter = (cnt / 2 + 1) * Thread.THREAD_WID + 100 + Thread.THREAD_HEI * 4;

            if (p < xThreadsCounter) p = xThreadsCounter;

            if (this.bmp != null) this.bmp.Dispose();

            this.bmp = new Bitmap(p, this.picBox.Height);
            Graphics.FromImage(this.bmp).Clear(Color.White);

            this.threads.Clear();
            this.dic.Clear();

            int y = 16, x;
            Thread t;

            for (int w = 0; w < 5; w++) {

                x = 10;
                for (int i = 0; i < cnt; i++) {
                    t = new Thread();

                    if (i % 2 == 0) {
                        t.Type = Type.HEDDLED;
                        t.setPosition(x, y);

                    }
                    else {
                        t.Type = Type.UNHEDDLED;
                        t.setPosition(x + Thread.THREAD_WID / 2, y + Thread.THREAD_HEI);
                        x += Thread.THREAD_WID;
                    }

                    this.threads.Add(t);

                }

                y += 2 * Thread.THREAD_HEI;
            }

            this.createPattern(cnt);

            this.updateBitmap();
        }

        private void cntThreads_TextChanged(object sender, EventArgs e) {
            if (this.cntThreads.Text.Length < 1) this.cntThreads.Text = "0";
        }

        private void updateBitmap() {
            Graphics gr = Graphics.FromImage(this.bmp);
            gr.Clear(Color.White);

            foreach (Thread t in this.threads) {
                t.draw(gr);
            }

            foreach (Thread t in this.pattern) {
                t.draw(gr, true);
            }

            if (this.pattern.Count > 0) {
                gr.DrawString("H", this.fnt, this.sb, 10, this.bmp.Height - 50);
                gr.DrawString("U", this.fnt, this.sb, 10, this.bmp.Height - 24);
            }

            int y = 20, x = xThreadsCounter - 3 * Thread.THREAD_HEI;
            foreach (KeyValuePair<int, int> t in dic) {
                gr.FillRectangle(new SolidBrush(Color.FromArgb(t.Key)), x, y, Thread.THREAD_HEI, Thread.THREAD_HEI);
                gr.DrawRectangle(pen, x, y, Thread.THREAD_HEI, Thread.THREAD_HEI);

                gr.DrawString(t.Value.ToString(), fnt, sb, x + Thread.THREAD_HEI + 4, y);

                y += Thread.THREAD_HEI + 4;
            }

            this.picBox.Image = this.bmp;
        }

        private void counter() {

            this.dic.Clear();

            int clr;

            foreach (Thread t in pattern) {
                if (t.Color == Color.Transparent) continue;

                clr = t.Color.ToArgb();

                if (dic.ContainsKey(clr)) {
                    dic[clr]++;
                }
                else {
                    dic[clr] = 1;
                }
            }
        }

        private void createPattern(int cnt) {
            this.pattern.Clear();

            Thread t;
            int x = 30, y = this.bmp.Height - 50;
            for (int i = 0; i < cnt; i++) {
                t = this.threads[i];
                t.setPatternPosition(x, y + (t.Type == Type.UNHEDDLED ? Thread.THREAD_HEI + 5 : 0));
                this.pattern.Add(t);

                x += Thread.THREAD_HEI;
            }
        }

        private void createPalette() {

            Color HSLtoRGB(double h, double s, double b) {
                Func<double, double> k = (n) => (n + h / 60) % 6;
                Func<double, double> f = (n) => b * (1 - s * Math.Max(0, Math.Min(Math.Min(k(n), 4 - k(n)), 1)));
                return Color.FromArgb(255, (int)(255 * f(5)), (int)(255 * f(3)), (int)(255 * f(1)));
            }

            this.pal = new Bitmap(720, 20);
            Graphics gr = Graphics.FromImage(this.pal);
            int t = 0;

            for (int z = 0; z < 360; z++) {
                Pen p = new Pen(HSLtoRGB(z, 1, 1));

                gr.DrawLine(p, t, 0, t, 20);
                t++;
                gr.DrawLine(p, t, 0, t, 20);
                t++;
            }

            this.palette.Image = this.pal;
        }
    }
}
