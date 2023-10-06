using System.Drawing;

namespace inkleLoom {

    enum Type {
        HEDDLED, UNHEDDLED
    }

    class Thread {

        private Pen pen = new Pen(Color.Black);

        internal const int THREAD_WID = 10, THREAD_HEI = 20;

        private SolidBrush clr = null;

        internal Color Color { 
            get { return clr.Color; }
            set { clr = new SolidBrush(value); } 
        }

        internal Type Type { get; set; }

        internal Rectangle Rect{ get; private set; }

        internal Rectangle RectP { get; private set; }

        internal void setPosition(int x, int y) {
            Rect = new Rectangle(x, y, THREAD_WID, THREAD_HEI);
            RectP = new Rectangle(x, 200 + y + (Type == Type.UNHEDDLED ? THREAD_WID : 0), THREAD_WID, THREAD_WID);
        }

        internal void draw(Graphics gr, bool pat = false) {
            Rectangle rc = pat ? RectP : Rect;

            if(clr != null) {
                gr.FillRectangle(clr, rc);
            }
            gr.DrawRectangle(pen, rc);
        }
    }
}
