using System.Drawing;
using System.IO;

namespace inkleLoom {

    enum Type {
        HEDDLED, UNHEDDLED
    }

    class Thread {

        internal const int THREAD_WID = 10, THREAD_HEI = 20;

        private Pen pen = new Pen(Color.Black);
        private SolidBrush clr = null;

        internal Color Color {
            get { return this.clr != null ? this.clr.Color : Color.Transparent; }
            set { this.clr = new SolidBrush(value); }
        }

        internal Type Type { get; set; }

        internal Rectangle Rect { get; private set; }

        internal Rectangle RectP { get; private set; }

        internal void setPosition(int x, int y) {
            this.Rect = new Rectangle(x, y, THREAD_WID, THREAD_HEI);
        }

        internal void setPatternPosition(int x, int y) {
            this.RectP = new Rectangle(x, y, THREAD_HEI, THREAD_HEI);
        }

        internal void draw(Graphics gr, bool pat = false) {
            Rectangle rc = pat ? this.RectP : this.Rect;

            if (this.clr != null) {
                gr.FillRectangle(this.clr, rc);
            }
            gr.DrawRectangle(this.pen, rc);
        }

        internal void clearColor() {
            if (clr != null) {
                clr.Dispose();
                clr = null;
            }
        }

        internal void save(StreamWriter writer) {
            writer.WriteLine(Color.ToArgb());
            writer.WriteLine(Type);
            writer.WriteLine(Rect.X);
            writer.WriteLine(Rect.Y);
            writer.WriteLine(RectP.X);
            writer.WriteLine(RectP.Y);
        }
    }
}