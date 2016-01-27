namespace RedisCacheBuilder
{
    partial class SuggestGridSizeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbxScreenSize = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxDPI = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxScale = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxCount = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Screen Largest Size";
            // 
            // tbxScreenSize
            // 
            this.tbxScreenSize.Location = new System.Drawing.Point(217, 40);
            this.tbxScreenSize.Name = "tbxScreenSize";
            this.tbxScreenSize.Size = new System.Drawing.Size(100, 25);
            this.tbxScreenSize.TabIndex = 1;
            this.tbxScreenSize.Text = "1920";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(168, 236);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Screen DPI";
            // 
            // tbxDPI
            // 
            this.tbxDPI.Location = new System.Drawing.Point(217, 87);
            this.tbxDPI.Name = "tbxDPI";
            this.tbxDPI.Size = new System.Drawing.Size(100, 25);
            this.tbxDPI.TabIndex = 4;
            this.tbxDPI.Text = "96";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Map Scale(1:?)";
            // 
            // tbxScale
            // 
            this.tbxScale.Location = new System.Drawing.Point(217, 140);
            this.tbxScale.Name = "tbxScale";
            this.tbxScale.Size = new System.Drawing.Size(100, 25);
            this.tbxScale.TabIndex = 6;
            this.tbxScale.Text = "250000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Divided Count";
            // 
            // tbxCount
            // 
            this.tbxCount.Location = new System.Drawing.Point(217, 187);
            this.tbxCount.Name = "tbxCount";
            this.tbxCount.Size = new System.Drawing.Size(100, 25);
            this.tbxCount.TabIndex = 8;
            this.tbxCount.Text = "6";
            // 
            // SuggestGridSizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 281);
            this.Controls.Add(this.tbxCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbxScale);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxDPI);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbxScreenSize);
            this.Controls.Add(this.label1);
            this.Name = "SuggestGridSizeForm";
            this.Text = "SuggestGridSize";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxScreenSize;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxDPI;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxScale;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxCount;
    }
}