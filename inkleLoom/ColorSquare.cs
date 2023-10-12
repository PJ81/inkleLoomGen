using System.Drawing;

namespace inkleLoom {
    class ColorSquare {

        private SolidBrush br;
        readonly SolidBrush sb = new SolidBrush(Color.Black);
        readonly Pen pen = new Pen(Color.Black);
        readonly Font fnt = new Font("Consolas", 12);

        internal void setPosition(int x, int y) => this.Rect = new Rectangle(x, y, Thread.PATTERN_SZ, Thread.PATTERN_SZ);

        internal void draw(Graphics gr) {
            gr.FillRectangle(this.br, this.Rect);
            gr.DrawRectangle(this.pen, this.Rect);
            gr.DrawString(this.Count.ToString(), this.fnt, this.sb, this.Rect.X + Thread.PATTERN_SZ + 4, this.Rect.Y);
        }

        internal int Count { get; set; }

        internal Rectangle Rect { get; private set; }

        internal Color Color {
            get { return this.br.Color; }
            set { this.br = new SolidBrush(value); }
        }
    }
}