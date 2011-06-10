using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using IObject;

namespace LibCacDoiTuongHinh
{
    public partial class DoiTuongDuongThang : UserControl, IDoiTuongVe
    {
        public DoiTuongDuongThang()
        {
            InitializeComponent();
            Location = new Point(20, 20);
        }
        public string Ten
        {
            get { return "Đường thẳng"; }
        }

        public UserControl GiaoDien
        {
            get { return this; }
        }

        public Bitmap Hinh
        {
            get { return LibCacDoiTuongHinh.Properties.Resources.duongthang; }
        }

        IForm frmParrent;
        public IForm Parrent
        {
            get
            {
                return frmParrent;
            }
            set
            {
                frmParrent = value;
            }
        }

        private void buttVeLai_Click(object sender, EventArgs e)
        {
            frmParrent.VeLaiDoiTuong();
        }


        public void VeDoiTuong(Graphics gh)
        {
            float _x, _y;

            if (!float.TryParse(txtAx.Text, out _x) || !float.TryParse(txtAy.Text, out _y)) 
                return;

            Vector2D p1= new Vector2D(_x, _y);

            if (!float.TryParse(txtBx.Text, out _x) || !float.TryParse(txtBy.Text, out _y)) 
                return;

            Vector2D p2 = new Vector2D(_x, _y);

            p1.Transform(matrix);
            p2.Transform(matrix);
            gh.DrawLine(ButVe, p1.ToPoint(), p2.ToPoint());
        }


        public Pen ButVe
        {
            get { return new Pen(Color.Red); }
        }

        Matrix2D matrix = Matrix2D.CreateIndentity();
        public Matrix2D MatrixBienHinh
        {
            get { return matrix; }
        }
    }
}
