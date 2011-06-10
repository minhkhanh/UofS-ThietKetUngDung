using System;
using System.Collections.Generic;
using System.Text;
using IObject;

namespace Affine
{
    class DoiTuongVe
    {
        IDoiTuongVe doiTuong;
        public IDoiTuongVe LoaiDoiTuongVe
        {
            get { return doiTuong; }
            set { doiTuong = value; }
        }
    }
}
