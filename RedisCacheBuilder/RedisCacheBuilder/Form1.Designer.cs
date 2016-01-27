namespace RedisCacheBuilder
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tbxDataSource = new System.Windows.Forms.TextBox();
            this.tbxCacheName = new System.Windows.Forms.TextBox();
            this.tbxSize = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.tbxServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.cbxMetaOnly = new System.Windows.Forms.CheckBox();
            this.lsCaches = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(215, 218);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(208, 52);
            this.button1.TabIndex = 0;
            this.button1.Text = "Build Cache";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 407);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(736, 29);
            this.progressBar1.TabIndex = 1;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // tbxDataSource
            // 
            this.tbxDataSource.Location = new System.Drawing.Point(133, 37);
            this.tbxDataSource.Margin = new System.Windows.Forms.Padding(4);
            this.tbxDataSource.Name = "tbxDataSource";
            this.tbxDataSource.Size = new System.Drawing.Size(407, 25);
            this.tbxDataSource.TabIndex = 2;
            // 
            // tbxCacheName
            // 
            this.tbxCacheName.Location = new System.Drawing.Point(133, 87);
            this.tbxCacheName.Margin = new System.Windows.Forms.Padding(4);
            this.tbxCacheName.Name = "tbxCacheName";
            this.tbxCacheName.Size = new System.Drawing.Size(407, 25);
            this.tbxCacheName.TabIndex = 3;
            // 
            // tbxSize
            // 
            this.tbxSize.Location = new System.Drawing.Point(133, 136);
            this.tbxSize.Margin = new System.Windows.Forms.Padding(4);
            this.tbxSize.Name = "tbxSize";
            this.tbxSize.Size = new System.Drawing.Size(268, 25);
            this.tbxSize.TabIndex = 4;
            this.tbxSize.Text = "50000";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(228, 128);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(208, 65);
            this.button3.TabIndex = 6;
            this.button3.Text = "Flush Cache";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tbxServer
            // 
            this.tbxServer.Location = new System.Drawing.Point(138, 12);
            this.tbxServer.Name = "tbxServer";
            this.tbxServer.Size = new System.Drawing.Size(461, 25);
            this.tbxServer.TabIndex = 7;
            this.tbxServer.Text = "192.168.1.78:6379";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Redis Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Cache Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Features";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "GridSize";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(423, 131);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(117, 31);
            this.button4.TabIndex = 13;
            this.button4.Text = "Suggest";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(219, 198);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(208, 65);
            this.button5.TabIndex = 7;
            this.button5.Text = "Delete Cache";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(548, 37);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(83, 25);
            this.button6.TabIndex = 14;
            this.button6.Text = "Browse";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(607, 13);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(104, 24);
            this.button7.TabIndex = 15;
            this.button7.Text = "Connect";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Enabled = false;
            this.tabControl1.Location = new System.Drawing.Point(26, 58);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(660, 326);
            this.tabControl1.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(652, 297);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Flush Cache";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cbxMetaOnly);
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.button6);
            this.tabPage3.Controls.Add(this.tbxSize);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.button4);
            this.tabPage3.Controls.Add(this.tbxCacheName);
            this.tabPage3.Controls.Add(this.tbxDataSource);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(652, 297);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Build Cache";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lsCaches);
            this.tabPage4.Controls.Add(this.button5);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(652, 297);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Delete Cache";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // cbxMetaOnly
            // 
            this.cbxMetaOnly.AutoSize = true;
            this.cbxMetaOnly.Location = new System.Drawing.Point(38, 178);
            this.cbxMetaOnly.Name = "cbxMetaOnly";
            this.cbxMetaOnly.Size = new System.Drawing.Size(133, 19);
            this.cbxMetaOnly.TabIndex = 14;
            this.cbxMetaOnly.Text = "MetaInfo Only";
            this.cbxMetaOnly.UseVisualStyleBackColor = true;
            // 
            // lsCaches
            // 
            this.lsCaches.FormattingEnabled = true;
            this.lsCaches.ItemHeight = 15;
            this.lsCaches.Location = new System.Drawing.Point(80, 32);
            this.lsCaches.Name = "lsCaches";
            this.lsCaches.Size = new System.Drawing.Size(483, 139);
            this.lsCaches.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 436);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxServer);
            this.Controls.Add(this.progressBar1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "ArcGIS POI Cache Builder";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox tbxDataSource;
        private System.Windows.Forms.TextBox tbxCacheName;
        private System.Windows.Forms.TextBox tbxSize;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tbxServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox cbxMetaOnly;
        private System.Windows.Forms.ListBox lsCaches;
    }
}

