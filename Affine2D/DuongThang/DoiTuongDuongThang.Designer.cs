namespace LibCacDoiTuongHinh
{
    partial class DoiTuongDuongThang
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAy = new System.Windows.Forms.TextBox();
            this.txtAx = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtBx = new System.Windows.Forms.TextBox();
            this.txtBy = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttVeLai = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y:";
            // 
            // txtAy
            // 
            this.txtAy.Location = new System.Drawing.Point(32, 48);
            this.txtAy.Name = "txtAy";
            this.txtAy.Size = new System.Drawing.Size(48, 20);
            this.txtAy.TabIndex = 2;
            // 
            // txtAx
            // 
            this.txtAx.Location = new System.Drawing.Point(32, 21);
            this.txtAx.Name = "txtAx";
            this.txtAx.Size = new System.Drawing.Size(48, 20);
            this.txtAx.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAx);
            this.groupBox1.Controls.Add(this.txtAy);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(7, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(86, 79);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tọa độ A";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtBx);
            this.groupBox2.Controls.Add(this.txtBy);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(99, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(86, 79);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tọa độ B";
            // 
            // txtBx
            // 
            this.txtBx.Location = new System.Drawing.Point(32, 21);
            this.txtBx.Name = "txtBx";
            this.txtBx.Size = new System.Drawing.Size(48, 20);
            this.txtBx.TabIndex = 3;
            // 
            // txtBy
            // 
            this.txtBy.Location = new System.Drawing.Point(32, 48);
            this.txtBy.Name = "txtBy";
            this.txtBy.Size = new System.Drawing.Size(48, 20);
            this.txtBy.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "X:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 14);
            this.label4.TabIndex = 1;
            this.label4.Text = "Y:";
            // 
            // buttVeLai
            // 
            this.buttVeLai.Location = new System.Drawing.Point(80, 93);
            this.buttVeLai.Name = "buttVeLai";
            this.buttVeLai.Size = new System.Drawing.Size(75, 23);
            this.buttVeLai.TabIndex = 6;
            this.buttVeLai.Text = "Vẽ lại";
            this.buttVeLai.UseVisualStyleBackColor = true;
            this.buttVeLai.Click += new System.EventHandler(this.buttVeLai_Click);
            // 
            // DoiTuongDuongThang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttVeLai);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "DoiTuongDuongThang";
            this.Size = new System.Drawing.Size(192, 125);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAy;
        private System.Windows.Forms.TextBox txtAx;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtBx;
        private System.Windows.Forms.TextBox txtBy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttVeLai;
    }
}
