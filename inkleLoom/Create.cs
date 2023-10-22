using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace inkleLoom {

    public partial class Create : Form {

        private const int ROW_CNT = 10;

        private readonly List<Thread> threads = new List<Thread>();
        private readonly List<ColorSquare> colors = new List<ColorSquare>();
        private int xThreadsCounter, threadCount;
        private Bitmap bmp1 = null, bmp2 = null, pal;
        readonly List<Thread> pattern = new List<Thread>();
        readonly Font fnt = new Font("Consolas", 12);
        readonly SolidBrush sb = new SolidBrush(Color.Black);
        private int threadingPosition;

        public Create() {
            this.InitializeComponent();
            this.cntThreads.Text = "0";
            this.createPalette();
        }

        private void Form1_Shown(object sender, EventArgs e) => this.trackR.Value = this.trackG.Value = this.trackB.Value = 0;

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
                                if (this.btnSymm.Checked) {
                                    this.updateTheads(t.Index, Color.Transparent);
                                }
                            }
                        }
                    }
                    else {
                        foreach (Thread tt in this.threads) {
                            if (tt.Rect.X == x) {
                                tt.Color = this.color.BackColor;
                                if (this.btnSymm.Checked) {
                                    this.updateTheads(t.Index, tt.Color);
                                }
                            }
                        }
                    }

                    this.counter();

                    this.updateBitmap();

                    return;
                }
            }

            foreach (ColorSquare q in this.colors) {
                if (q.Rect.Contains(e.Location)) {
                    this.clrDlg.Color = q.Color;
                    if (this.clrDlg.ShowDialog() == DialogResult.OK) {
                        this.updateColors(q.Color, this.clrDlg.Color);
                        break;
                    }
                }
            }

            this.counter();
            this.updateBitmap();
        }

        private void updateColors(Color c1, Color c2) {
            foreach (Thread t in this.threads) {
                if (t.Color.ToArgb() == c1.ToArgb()) {
                    t.Color = c2;
                }
            }
        }

        private void updateTheads(int index, Color color) {
            bool o = this.threadCount % 2 == 1, u = threads[index].Type == Type.UNHEDDLED;
            int i = this.threadCount - index - (o ? 1 : u ? 0 : 2);

            foreach (Thread t in this.threads) {
                if (t.Index == i) {
                    if (color.A < 255) t.clearColor();
                    else t.Color = color;
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e) {
            this.threadCount = Convert.ToInt32(this.cntThreads.Text);
            this.threadingPosition = -1;
            this.createThreads();
        }

        private void cntThreads_TextChanged(object sender, EventArgs e) {
            if (this.cntThreads.Text.Length < 1) this.cntThreads.Text = "0";
        }

        private void btnSave_Click(object sender, EventArgs e) {

            this.saveDlg.FileName = "";
            this.saveDlg.Filter = "Inkle loom|*.ikl";


            if (this.saveDlg.ShowDialog() == DialogResult.OK) {
                using (FileStream strm = new FileStream(this.saveDlg.FileName, FileMode.Create, FileAccess.Write)) {
                    StreamWriter writer = new StreamWriter(strm);
                    writer.BaseStream.Seek(0, SeekOrigin.End);

                    writer.WriteLine(this.threads.Count);

                    foreach (Thread t in this.threads) {
                        writer.WriteLine(t.Color.ToArgb());
                    }

                    writer.Flush();
                    writer.Close();
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e) {
            this.saveDlg.FileName = "";
            this.saveDlg.Filter = "Image png|*.png";

            if (this.saveDlg.ShowDialog() == DialogResult.OK) {
                Bitmap b = new Bitmap(30 + Math.Max(this.bmp1.Width, this.bmp2.Width), this.bmp1.Height + 50 + this.bmp2.Height + 30);
                Graphics gr = Graphics.FromImage(b);
                gr.Clear(Color.White);
                gr.DrawImage(this.bmp1, 15, 15);
                gr.DrawImage(this.bmp2, 15, 15 + 50 + this.bmp1.Height);

                b.Save(this.saveDlg.FileName);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e) {
            if (this.openDlg.ShowDialog() == DialogResult.OK) {
                this.threadingPosition = -1;

                using (FileStream strm = new FileStream(this.openDlg.FileName, FileMode.Open, FileAccess.Read)) {
                    StreamReader reader = new StreamReader(strm);

                    this.threads.Clear();
                    this.pattern.Clear();

                    int cnt = Convert.ToInt32(reader.ReadLine());
                    Color c;

                    this.threadCount = cnt / ROW_CNT;

                    this.createThreads();
                    this.cntThreads.Text = this.threadCount.ToString();

                    foreach (Thread t in threads) {
                        c = Color.FromArgb(Convert.ToInt32(reader.ReadLine()));
                        if (c.A != 0) t.Color = c;
                    }

                    reader.Close();
                }

                this.createBitmap();

                this.counter();

                this.updateBitmap();
            }
        }

        private void addMid_Click(object sender, EventArgs e) {
            // not implemented
        }

        private void remMid_Click(object sender, EventArgs e) {
            // not implemented
        }

        private void addBoard_Click(object sender, EventArgs e) {
            // not implemented
        }

        private void remBoard_Click(object sender, EventArgs e) {
            // not implemented
        }

        private void createThreads() {

            if (!this.createBitmap()) return;

            this.threads.Clear();
            this.colors.Clear();

            int y = 30, x;
            Thread t;

            for (int w = 0; w < ROW_CNT; w++) {

                x = 12;
                for (int i = 0; i < this.threadCount; i++) {
                    t = new Thread();

                    t.Index = i;

                    if (i % 2 == 0) {
                        t.Type = Type.HEDDLED;
                        t.setPosition(x, y);

                    }
                    else {
                        t.Type = Type.UNHEDDLED;
                        t.setPosition(x + Thread.THREAD_WID / 2, y + Thread.THREAD_HEI - Thread.THREAD_WID / 2);
                        x += Thread.THREAD_WID;
                    }

                    this.threads.Add(t);

                }

                y += (int)(1.5 * Thread.THREAD_HEI);
            }

            this.createPattern();

            this.updateBitmap();
        }

        private bool createBitmap() {

            if (this.threadCount < 4) return false;

            int p1 = 100 + (this.threadCount / 2 + 1) * Thread.THREAD_WID + 4 * Thread.THREAD_HEI,
                p2 = 70 + this.threadCount * Thread.PATTERN_SZ;
            this.xThreadsCounter = (this.threadCount / 2 + 1) * Thread.THREAD_WID + 80 + Thread.THREAD_HEI * 4;

            if (p2 < this.xThreadsCounter) p2 = this.xThreadsCounter;

            if (this.bmp1 != null) this.bmp1.Dispose();
            if (this.bmp2 != null) this.bmp2.Dispose();

            this.bmp1 = new Bitmap(p1, 450);
            this.bmp2 = new Bitmap(p2, 100);

            return true;
        }

        private void updateBitmap() {
            Graphics gr = Graphics.FromImage(this.bmp1);
            gr.Clear(Color.White);

            foreach (Thread t in this.threads) {
                t.draw(gr);
            }

            foreach (ColorSquare q in this.colors) {
                q.draw(gr);
            }

            int h = 0;
            foreach (Thread t in this.pattern) {
                if (t.Type == Type.HEDDLED) h++;
            }
            int u = pattern.Count - h;

            gr.DrawString(String.Format("Threads:{0} - H:{1} - U:{2}", (h + u), h, u), this.fnt, this.sb, 8, 0);

            gr = Graphics.FromImage(this.bmp2);
            gr.Clear(Color.White);

            foreach (Thread t in this.pattern) {
                t.draw(gr, true);
            }

            if (this.pattern.Count > 0) {
                gr.DrawString("H", this.fnt, this.sb, this.pattern[0].RectP.X - Thread.PATTERN_SZ, this.pattern[0].RectP.Y);
                gr.DrawString("U", this.fnt, this.sb, this.pattern[0].RectP.X, this.pattern[1].RectP.Y);
            }

            this.picBox.Image = this.bmp1;
            this.picBox2.Image = this.bmp2;
        }

        private void counter() {

            Dictionary<int, int> dic = new Dictionary<int, int>();
            this.colors.Clear();

            foreach (Thread t in this.pattern) {
                if (t.Color == Color.Transparent) continue;

                int clr = t.Color.ToArgb();

                if (dic.ContainsKey(clr)) {
                    dic[clr]++;
                }
                else {
                    dic[clr] = 1;
                }
            }


            int y = 40, x = this.xThreadsCounter - 3 * Thread.THREAD_HEI;
            ColorSquare q;
            foreach (KeyValuePair<int, int> t in dic) {
                q = new ColorSquare();
                q.setPosition(x, y);
                q.Color = Color.FromArgb(t.Key);
                q.Count = t.Value;

                this.colors.Add(q);

                y += Thread.THREAD_HEI + 4;
            }
        }

        private void picBox2_MouseDown(object sender, MouseEventArgs e) {
            if( e.Button == MouseButtons.Right && this.pattern.Count > 0) {

                if (this.threadingPosition > -1) {
                    this.pattern[this.threadingPosition].Selected = false;
                    if(++this.threadingPosition >= this.pattern.Count) {
                        this.threadingPosition = -1;
                        this.updateBitmap();
                        return;
                    }

                } else {
                    this.threadingPosition = 0;
                }

                this.pattern[this.threadingPosition].Selected = true;
                this.updateBitmap();

                return;
            }

            foreach (Thread t in this.pattern) {
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

        private void createPattern() {
            this.pattern.Clear();

            Thread t;
            int x = 30, y = 20;
            for (int i = 0; i < this.threadCount; i++) {
                t = this.threads[i];
                t.setPatternPosition(x, y + (t.Type == Type.UNHEDDLED ? Thread.PATTERN_SZ + 3 : 0));
                this.pattern.Add(t);

                x += Thread.PATTERN_SZ;
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