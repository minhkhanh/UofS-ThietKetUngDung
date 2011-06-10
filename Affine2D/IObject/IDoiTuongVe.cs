using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IObject
{
    public interface IDoiTuongVe
    {
        string Ten { get; }
        UserControl GiaoDien { get; }
        Bitmap Hinh { get; }
        IForm Parrent { get; set; }
        void VeDoiTuong(Graphics gh);
        Pen ButVe { get; }
        Matrix2D MatrixBienHinh { get; }
    }
}
