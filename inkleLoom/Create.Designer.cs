namespace inkleLoom {
    partial class Create {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Create));
            this.cntThreads = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trackR = new System.Windows.Forms.TrackBar();
            this.trackG = new System.Windows.Forms.TrackBar();
            this.trackB = new System.Windows.Forms.TrackBar();
            this.color = new System.Windows.Forms.Label();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.labelR = new System.Windows.Forms.Label();
            this.labelG = new System.Windows.Forms.Label();
            this.labelB = new System.Windows.Forms.Label();
            this.palette = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.openDlg = new System.Windows.Forms.OpenFileDialog();
            this.saveDlg = new System.Windows.Forms.SaveFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.picBox2 = new System.Windows.Forms.PictureBox();
            this.btnSymm = new System.Windows.Forms.CheckBox();
            this.btnExport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // cntThreads
            // 
            this.cntThreads.Location = new System.Drawing.Point(6, 28);
            this.cntThreads.Name = "cntThreads";
            this.cntThreads.Size = new System.Drawing.Size(62, 20);
            this.cntThreads.TabIndex = 0;
            this.cntThreads.TextChanged += new System.EventHandler(this.cntThreads_TextChanged);
            // 
            // btnCreate
            // 
            this.btnCreate.Image = ((System.Drawing.Image)(resources.GetObject("btnCreate.Image")));
            this.btnCreate.Location = new System.Drawing.Point(72, 26);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(62, 24);
            this.btnCreate.TabIndex = 1;
            this.btnCreate.Text = "Create";
            this.btnCreate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Threads:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Colors:";
            // 
            // trackR
            // 
            this.trackR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackR.AutoSize = false;
            this.trackR.Location = new System.Drawing.Point(6, 71);
            this.trackR.Maximum = 255;
            this.trackR.Name = "trackR";
            this.trackR.Size = new System.Drawing.Size(558, 30);
            this.trackR.TabIndex = 6;
            this.trackR.TickFrequency = 0;
            this.trackR.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackR.Value = 1;
            this.trackR.ValueChanged += new System.EventHandler(this.trackR_Scroll);
            // 
            // trackG
            // 
            this.trackG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackG.AutoSize = false;
            this.trackG.Location = new System.Drawing.Point(6, 107);
            this.trackG.Maximum = 255;
            this.trackG.Name = "trackG";
            this.trackG.Size = new System.Drawing.Size(558, 30);
            this.trackG.TabIndex = 7;
            this.trackG.TickFrequency = 0;
            this.trackG.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackG.Value = 1;
            this.trackG.ValueChanged += new System.EventHandler(this.trackR_Scroll);
            // 
            // trackB
            // 
            this.trackB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackB.AutoSize = false;
            this.trackB.Location = new System.Drawing.Point(6, 142);
            this.trackB.Maximum = 255;
            this.trackB.Name = "trackB";
            this.trackB.Size = new System.Drawing.Size(558, 30);
            this.trackB.TabIndex = 8;
            this.trackB.TickFrequency = 0;
            this.trackB.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackB.Value = 1;
            this.trackB.ValueChanged += new System.EventHandler(this.trackR_Scroll);
            // 
            // color
            // 
            this.color.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.color.Location = new System.Drawing.Point(619, 77);
            this.color.Name = "color";
            this.color.Size = new System.Drawing.Size(104, 94);
            this.color.TabIndex = 9;
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(1, 5);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(727, 347);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picBox.TabIndex = 10;
            this.picBox.TabStop = false;
            this.picBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseDown);
            // 
            // labelR
            // 
            this.labelR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelR.AutoSize = true;
            this.labelR.Location = new System.Drawing.Point(572, 80);
            this.labelR.Name = "labelR";
            this.labelR.Size = new System.Drawing.Size(15, 13);
            this.labelR.TabIndex = 11;
            this.labelR.Text = "R";
            // 
            // labelG
            // 
            this.labelG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelG.AutoSize = true;
            this.labelG.Location = new System.Drawing.Point(572, 116);
            this.labelG.Name = "labelG";
            this.labelG.Size = new System.Drawing.Size(15, 13);
            this.labelG.TabIndex = 12;
            this.labelG.Text = "G";
            // 
            // labelB
            // 
            this.labelB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelB.AutoSize = true;
            this.labelB.Location = new System.Drawing.Point(572, 151);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(14, 13);
            this.labelB.TabIndex = 13;
            this.labelB.Text = "B";
            // 
            // palette
            // 
            this.palette.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.palette.Location = new System.Drawing.Point(6, 185);
            this.palette.Name = "palette";
            this.palette.Size = new System.Drawing.Size(720, 20);
            this.palette.TabIndex = 14;
            this.palette.MouseDown += new System.Windows.Forms.MouseEventHandler(this.palette_MouseDown);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.picBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 219);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(744, 377);
            this.panel1.TabIndex = 15;
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(227, 26);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(62, 24);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnLoad.Image")));
            this.btnLoad.Location = new System.Drawing.Point(292, 26);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(62, 24);
            this.btnLoad.TabIndex = 17;
            this.btnLoad.Text = "Load";
            this.btnLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // openDlg
            // 
            this.openDlg.Filter = "Inkle loom|*.ikl";
            // 
            // saveDlg
            // 
            this.saveDlg.Filter = "Inkle loom|*.ikl";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.picBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 596);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(744, 119);
            this.panel2.TabIndex = 18;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.btnExport);
            this.panel3.Controls.Add(this.btnSymm);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.cntThreads);
            this.panel3.Controls.Add(this.btnLoad);
            this.panel3.Controls.Add(this.btnCreate);
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.palette);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.labelB);
            this.panel3.Controls.Add(this.trackR);
            this.panel3.Controls.Add(this.labelG);
            this.panel3.Controls.Add(this.trackG);
            this.panel3.Controls.Add(this.labelR);
            this.panel3.Controls.Add(this.trackB);
            this.panel3.Controls.Add(this.color);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(744, 219);
            this.panel3.TabIndex = 19;
            // 
            // picBox2
            // 
            this.picBox2.Location = new System.Drawing.Point(1, 0);
            this.picBox2.Name = "picBox2";
            this.picBox2.Size = new System.Drawing.Size(727, 88);
            this.picBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picBox2.TabIndex = 11;
            this.picBox2.TabStop = false;
            this.picBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBox2_MouseDown);
            // 
            // btnSymm
            // 
            this.btnSymm.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnSymm.Image = ((System.Drawing.Image)(resources.GetObject("btnSymm.Image")));
            this.btnSymm.Location = new System.Drawing.Point(137, 26);
            this.btnSymm.Name = "btnSymm";
            this.btnSymm.Size = new System.Drawing.Size(87, 24);
            this.btnSymm.TabIndex = 20;
            this.btnSymm.Text = "Symmetrical";
            this.btnSymm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSymm.UseVisualStyleBackColor = true;
            this.btnSymm.CheckedChanged += new System.EventHandler(this.btnSymm_CheckedChanged);
            // 
            // btnExport
            // 
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.Location = new System.Drawing.Point(357, 26);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(62, 24);
            this.btnExport.TabIndex = 20;
            this.btnExport.Text = "Export";
            this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(744, 715);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.MinimumSize = new System.Drawing.Size(746, 697);
            this.Name = "Form1";
            this.Text = "Inkle Loom ";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.trackR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox cntThreads;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackR;
        private System.Windows.Forms.TrackBar trackG;
        private System.Windows.Forms.TrackBar trackB;
        private System.Windows.Forms.Label color;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.Label labelR;
        private System.Windows.Forms.Label labelG;
        private System.Windows.Forms.Label labelB;
        private System.Windows.Forms.Label palette;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.OpenFileDialog openDlg;
        private System.Windows.Forms.SaveFileDialog saveDlg;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox picBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox btnSymm;
        private System.Windows.Forms.Button btnExport;
    }
}

