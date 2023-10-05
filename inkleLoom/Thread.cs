using System.Drawing;

namespace inkleLoom {

    enum Type {
        HEDDLED, UNHEDDLED
    }

    class Thread {

        public const int THREAD_WID = 12, THREAD_HEI = 20;

        public Color Color { get; set; }

        public Type Type { get; set; }

        public Rectangle Rect{ get; private set; }

        public void setPosition(int x, int y) {
            Rect = new Rectangle(x, y, THREAD_WID, THREAD_HEI);
        }

        internal void draw(Graphics gr) {
            gr.DrawRectangle(new Pen(Color.Black), Rect);
        }
    }
}
