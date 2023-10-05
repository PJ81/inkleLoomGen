﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace inkleLoom {

    public partial class Form1 : Form {

        List<Thread> threads = new List<Thread>();
        Bitmap bmp;

        public Form1() {
            InitializeComponent();
            cntThreads.Text = "0";
            bmp = new Bitmap(picBox.Width, picBox.Height);
            Graphics.FromImage(bmp).Clear(Color.White);

            updateBitmap();
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

        private void Form1_Shown(object sender, EventArgs e) {
            trackR.Value = trackG.Value = trackB.Value = 0;
        }

        private void btnCreate_Click(object sender, EventArgs e) {
            int cnt = Convert.ToInt32(cntThreads.Text);
            if (cnt % 2 == 0) cnt++;

            int x = 10, y = 16;
            for(int i = 0; i < (cnt / 2) + 1; i++) {
                Thread t = new Thread();
                t.Type = Type.HEDDLED;
                t.setPosition(x, y);
                x += Thread.THREAD_WID;
                threads.Add(t);
            }

            x = 10 + Thread.THREAD_WID / 2;
            for (int i = 0; i < (cnt / 2); i++) {
                Thread t = new Thread();
                t.Type = Type.UNHEDDLED;
                t.setPosition(x, y + Thread.THREAD_HEI);
                x += Thread.THREAD_WID;
                threads.Add(t);
            }

            updateBitmap();
        }

        private void cntThreads_TextChanged(object sender, EventArgs e) {
            if (cntThreads.Text.Length < 1) cntThreads.Text = "0";
        }

        private void updateBitmap() {
            Graphics gr = Graphics.FromImage(bmp);
            foreach (Thread t in threads) {
                t.draw(gr);
            }

            picBox.Image = bmp;
        }
    }
}
