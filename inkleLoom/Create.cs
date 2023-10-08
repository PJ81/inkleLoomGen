using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace inkleLoom {

    public partial class Create : Form {

        private const int ROW_CNT = 8;

        private List<Thread> threads = new List<Thread>();
        private Dictionary<int, int> dic = new Dictionary<int, int>();
        private int xThreadsCounter, threadCount;
        private Bitmap bmp1 = null, bmp2 = null, pal;
        readonly List<Thread> pattern = new List<Thread>();
        readonly Font fnt = new Font("Consolas", 12);
        readonly SolidBrush sb = new SolidBrush(Color.Black);
        readonly Pen pen = new Pen(Color.Black);


        public Create() {
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
                                if (btnSymm.Checked) {
                                    updateTheads(t.Index, Color.Transparent);
                                }
                            }
                        }
                    }
                    else {
                        foreach (Thread tt in this.threads) {
                            if (tt.Rect.X == x) {
                                tt.Color = this.color.BackColor;
                                if(btnSymm.Checked) {
                                    updateTheads(t.Index, tt.Color);
                                }
                            }
                        }
                    }

                    this.counter();

                    this.updateBitmap();

                    return;
                }
            }
        }

        private void updateTheads(int index, Color color) {
            int i = threadCount - index - 1;
            foreach (Thread t in threads) {
                if (t.Index == i) {
                    if (color.A < 255) t.clearColor();
                    else  t.Color = color;
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e) {

            threadCount = Convert.ToInt32(this.cntThreads.Text);

            if (!createBitmap()) return;

            Graphics.FromImage(this.bmp1).Clear(Color.White);

            this.threads.Clear();
            this.dic.Clear();

            
            int y = 16, x;
            Thread t;

            for (int w = 0; w < ROW_CNT; w++) {

                x = 10;
                for (int i = 0; i < threadCount; i++) {
                    t = new Thread();

                    t.Index = i;

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

            this.createPattern();

            this.updateBitmap();
        }

        private void cntThreads_TextChanged(object sender, EventArgs e) {
            if (this.cntThreads.Text.Length < 1) this.cntThreads.Text = "0";
        }

        private void btnSave_Click(object sender, EventArgs e) {

            saveDlg.FileName = "";
            saveDlg.Filter = "Inkle loom|*.ikl";


            if (saveDlg.ShowDialog() == DialogResult.OK) {
                FileStream strm = new FileStream(saveDlg.FileName, FileMode.Create, FileAccess.Write);
                StreamWriter writer = new StreamWriter(strm);
                writer.BaseStream.Seek(0, SeekOrigin.End);

                writer.WriteLine(threads.Count);

                foreach (Thread t in threads) {
                    t.save(writer);
                }

                writer.Flush();
                writer.Close();
            }
        }

        private void btnExport_Click(object sender, EventArgs e) {
            saveDlg.FileName = "";
            saveDlg.Filter = "Image png|*.png";

            if (saveDlg.ShowDialog() == DialogResult.OK) {
                Bitmap b = new Bitmap(30 + Math.Max(bmp1.Width, bmp2.Width), bmp1.Height + 50 + bmp2.Height + 30);
                Graphics gr = Graphics.FromImage(b);
                gr.Clear(Color.White);
                gr.DrawImage(bmp1, 15, 15);
                gr.DrawImage(bmp2, 15, 15 + 50 + bmp1.Height);

                b.Save(saveDlg.FileName);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e) {
            if (openDlg.ShowDialog() == DialogResult.OK) {
                FileStream strm = new FileStream(openDlg.FileName, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(strm);

                threads.Clear();
                pattern.Clear();
                dic.Clear();

                int cnt = Convert.ToInt32(reader.ReadLine()), x, y;
                Color c;

                threadCount = cnt / ROW_CNT;
                cntThreads.Text = threadCount.ToString();

                for (int i = 0; i < cnt; i++) {
                    Thread t = new Thread();
                    c = Color.FromArgb(Convert.ToInt32(reader.ReadLine()));
                    if (c.A != 0) t.Color = c;

                    t.Type = (Type)Enum.Parse(typeof(Type), reader.ReadLine());

                    x = Convert.ToInt32(reader.ReadLine());
                    y = Convert.ToInt32(reader.ReadLine());
                    t.setPosition(x, y);

                    x = Convert.ToInt32(reader.ReadLine());
                    y = Convert.ToInt32(reader.ReadLine());
                    t.setPatternPosition(x, y);


                    threads.Add(t);
                }

                reader.Close();

                createBitmap();

                for (int i = 0; i < threadCount; i++) {
                    pattern.Add(threads[i]);
                }

                counter();

                updateBitmap();
            }
        }

        private bool createBitmap() {
           
            if (threadCount < 4) return false;

            int p1 = 100 + (threadCount / 2 + 1) * Thread.THREAD_WID + 4 * Thread.THREAD_HEI,
                p2 = 70 + threadCount * Thread.THREAD_HEI;
            xThreadsCounter = (threadCount / 2 + 1) * Thread.THREAD_WID + 80 + Thread.THREAD_HEI * 4;

            if (p2 < xThreadsCounter) p2 = xThreadsCounter;

            if (this.bmp1 != null) this.bmp1.Dispose();

            this.bmp1 = new Bitmap(p1, 350);
            this.bmp2 = new Bitmap(p2, 100);

            return true;
        }

        private void updateBitmap() {
            Graphics gr = Graphics.FromImage(this.bmp1);
            gr.Clear(Color.White);

            foreach (Thread t in this.threads) {
                t.draw(gr);
            }

            int y = 20, x = xThreadsCounter - 3 * Thread.THREAD_HEI;
            foreach (KeyValuePair<int, int> t in dic) {
                gr.FillRectangle(new SolidBrush(Color.FromArgb(t.Key)), x, y, Thread.THREAD_HEI, Thread.THREAD_HEI);
                gr.DrawRectangle(pen, x, y, Thread.THREAD_HEI, Thread.THREAD_HEI);

                gr.DrawString(t.Value.ToString(), fnt, sb, x + Thread.THREAD_HEI + 4, y);

                y += Thread.THREAD_HEI + 4;
            }

            gr = Graphics.FromImage(this.bmp2);
            gr.Clear(Color.White);

            foreach (Thread t in this.pattern) {
                t.draw(gr, true);
            }

            if (this.pattern.Count > 0) {
                gr.DrawString("H", this.fnt, this.sb, 10, 20);// this.bmp1.Height - 50)) ;
                gr.DrawString("U", this.fnt, this.sb, 10, 45);// this.bmp1.Height - 24);
            }

            this.picBox.Image = this.bmp1;
            this.picBox2.Image = this.bmp2;
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

        private void picBox2_MouseDown(object sender, MouseEventArgs e) {
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

        private void btnSymm_CheckedChanged(object sender, EventArgs e) {
            updateBitmap();
        }

        private void createPattern() {
            this.pattern.Clear();

            Thread t;
            int x = 30, y = 20;// this.bmp1.Height - 50;
            for (int i = 0; i < threadCount; i++) {
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
