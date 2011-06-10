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
    public partial class DoiTuongDuongTron : UserControl, IDoiTuongVe
    {
        public DoiTuongDuongTron()
        {
            InitializeComponent();
            Location = new Point(20, 20);
        }

        public string Ten
        {
            get { return "Hình tròn"; }
        }

        public UserControl GiaoDien
        {
            get { return this; }
        }

        public Bitmap Hinh
        {
            get { return LibCacDoiTuongHinh.Properties.Resources.duongtron; }
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

        public void VeDoiTuong(Graphics gh)
        {
            throw new NotImplementedException();
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
