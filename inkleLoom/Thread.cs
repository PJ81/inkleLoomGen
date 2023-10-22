using System.Drawing;

namespace inkleLoom {

    enum Type {
        HEDDLED, UNHEDDLED
    }

    class Thread {

        internal const int 
            THREAD_WID = 12, 
            THREAD_HEI = 24, 
            PATTERN_SZ = 16,
            HTW = THREAD_WID / 2, 
            HTH = THREAD_HEI / 2;

        private Point[] pts = new Point[6];
        private readonly Point[] tmp = new Point[6];

        private readonly Pen pen = new Pen(Color.Black);
        private SolidBrush clr = null, threads = new SolidBrush(Color.Black);

        public bool Selected { get; set; }

        public Thread() {
            pts[0] = new Point(-HTW, -(HTH / 2));
            pts[1] = new Point(0, -HTH);
            pts[2] = new Point(HTW, -(HTH / 2));
            pts[3] = new Point(HTW, HTH / 2);
            pts[4] = new Point(0, HTH);
            pts[5] = new Point(-HTW, HTH / 2);
        }

        internal int Index { get; set; }

        internal Color Color {
            get { return this.clr != null ? this.clr.Color : Color.Transparent; }
            set { this.clr = new SolidBrush(value); }
        }

        internal Type Type { get; set; }

        internal Rectangle Rect { get; private set; }

        internal Rectangle RectP { get; private set; }

        internal void setPosition(int x, int y) => this.Rect = new Rectangle(x, y, THREAD_WID, THREAD_HEI);

        internal void setPatternPosition(int x, int y) => this.RectP = new Rectangle(x, y, PATTERN_SZ, PATTERN_SZ);

        internal void draw(Graphics gr, bool pat = false) {

            if (!pat) {
                pts.CopyTo(tmp, 0);

                for (int z = 0; z < 6; z++) {
                    tmp[z].X += this.Rect.X + HTW;
                    tmp[z].Y += this.Rect.Y + HTH;
                }

                if (this.clr != null) gr.FillPolygon(this.clr, tmp);
                gr.DrawPolygon(this.pen, tmp);

            }
            else {
                if (this.clr != null) gr.FillRectangle(this.clr, this.RectP);
                gr.DrawRectangle(this.pen, this.RectP);
                if(this.Selected) {
                    gr.FillEllipse(this.threads, this.RectP.X + PATTERN_SZ / 2 - 4, this.RectP.Y - 12, 8, 8);
                }

            }
        }

        internal void clearColor() {
            if (this.clr != null) {
                this.clr.Dispose();
                this.clr = null;
            }
        }
    }
}