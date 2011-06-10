namespace Affine
{
    partial class frmMain
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
            this.myOutlookBar = new UtilityLibrary.WinControls.OutlookBar();
            this.bandHinhMau = new UtilityLibrary.WinControls.OutlookBarBand();
            this.groupInput = new System.Windows.Forms.GroupBox();
            this.picDraw = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttTinhTien = new System.Windows.Forms.Button();
            this.txtTinhTienY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTinhTienX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.matrixView = new Affine.MatrixView();
            ((System.ComponentModel.ISupportInitialize)(this.picDraw)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // myOutlookBar
            // 
            this.myOutlookBar.AnimationSpeed = 20;
            this.myOutlookBar.BackgroundBitmap = null;
            this.myOutlookBar.Bands.Add(this.bandHinhMau);
            this.myOutlookBar.BorderType = UtilityLibrary.WinControls.BorderType.None;
            this.myOutlookBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.myOutlookBar.FlatArrowButtons = false;
            this.myOutlookBar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.myOutlookBar.LeftTopColor = System.Drawing.Color.Empty;
            this.myOutlookBar.Location = new System.Drawing.Point(0, 0);
            this.myOutlookBar.Name = "myOutlookBar";
            this.myOutlookBar.RightBottomColor = System.Drawing.Color.Empty;
            this.myOutlookBar.Size = new System.Drawing.Size(160, 416);
            this.myOutlookBar.TabIndex = 0;
            this.myOutlookBar.Text = "outlookBar1";
            this.myOutlookBar.ItemClicked += new UtilityLibrary.WinControls.OutlookBarItemClickedHandler(this.myOutlookBar_ItemClicked);
            // 
            // bandHinhMau
            // 
            this.bandHinhMau.Background = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.bandHinhMau.IconView = UtilityLibrary.WinControls.IconView.Large;
            this.bandHinhMau.LargeImageList = null;
            this.bandHinhMau.Location = new System.Drawing.Point(0, 0);
            this.bandHinhMau.Name = "bandHinhMau";
            this.bandHinhMau.Size = new System.Drawing.Size(0, 0);
            this.bandHinhMau.SmallImageList = null;
            this.bandHinhMau.TabIndex = 0;
            this.bandHinhMau.Text = "Hình mẫu";
            this.bandHinhMau.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // groupInput
            // 
            this.groupInput.Location = new System.Drawing.Point(832, 263);
            this.groupInput.Name = "groupInput";
            this.groupInput.Size = new System.Drawing.Size(206, 142);
            this.groupInput.TabIndex = 1;
            this.groupInput.TabStop = false;
            this.groupInput.Text = "Input";
            // 
            // picDraw
            // 
            this.picDraw.Location = new System.Drawing.Point(166, 11);
            this.picDraw.Name = "picDraw";
            this.picDraw.Size = new System.Drawing.Size(660, 394);
            this.picDraw.TabIndex = 2;
            this.picDraw.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(833, 121);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(205, 136);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttTinhTien);
            this.tabPage1.Controls.Add(this.txtTinhTienY);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtTinhTienX);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(197, 110);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tịnh tiến";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttTinhTien
            // 
            this.buttTinhTien.Location = new System.Drawing.Point(81, 76);
            this.buttTinhTien.Name = "buttTinhTien";
            this.buttTinhTien.Size = new System.Drawing.Size(75, 21);
            this.buttTinhTien.TabIndex = 5;
            this.buttTinhTien.Text = "Thêm";
            this.buttTinhTien.UseVisualStyleBackColor = true;
            this.buttTinhTien.Click += new System.EventHandler(this.buttTinhTien_Click);
            // 
            // txtTinhTienY
            // 
            this.txtTinhTienY.Location = new System.Drawing.Point(65, 43);
            this.txtTinhTienY.Name = "txtTinhTienY";
            this.txtTinhTienY.Size = new System.Drawing.Size(45, 20);
            this.txtTinhTienY.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Y:";
            // 
            // txtTinhTienX
            // 
            this.txtTinhTienX.Location = new System.Drawing.Point(65, 19);
            this.txtTinhTienX.Name = "txtTinhTienX";
            this.txtTinhTienX.Size = new System.Drawing.Size(45, 20);
            this.txtTinhTienX.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "X:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vector tịnh tiến:";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(197, 110);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // matrixView
            // 
            this.matrixView.Location = new System.Drawing.Point(832, 11);
            this.matrixView.Name = "matrixView";
            this.matrixView.Size = new System.Drawing.Size(192, 103);
            this.matrixView.TabIndex = 3;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 416);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.matrixView);
            this.Controls.Add(this.picDraw);
            this.Controls.Add(this.groupInput);
            this.Controls.Add(this.myOutlookBar);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Affine";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMain_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.picDraw)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UtilityLibrary.WinControls.OutlookBar myOutlookBar;
        private UtilityLibrary.WinControls.OutlookBarBand bandHinhMau;
        private System.Windows.Forms.GroupBox groupInput;
        private System.Windows.Forms.PictureBox picDraw;
        private MatrixView matrixView;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttTinhTien;
        private System.Windows.Forms.TextBox txtTinhTienY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTinhTienX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;

    }
}