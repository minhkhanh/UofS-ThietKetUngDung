using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UtilityLibrary;
using UtilityLibrary.WinControls;
using IObject;
using System.Drawing.Drawing2D;

namespace Affine
{
    public partial class frmMain : Form, IForm
    {
        public frmMain()
        {
            InitializeComponent();
            InitializeOutlookBar();
        }

        private void InitializeOutlookBar()
        {
            string ThuMuc_PlugIn = Application.StartupPath + @"\";
            AffineManager.LayDoiTuongVe(ThuMuc_PlugIn);
            bandHinhMau.Items.Clear();
            ImageList listImage = new ImageList();
            listImage.TransparentColor = Color.Transparent;
            for (int i = 0; i < AffineManager.DsDoiTuongVe.Count; ++i )
            {
                listImage.Images.Add(AffineManager.DsDoiTuongVe[i].LoaiDoiTuongVe.Hinh);
                AffineManager.DsDoiTuongVe[i].LoaiDoiTuongVe.Parrent = this;
                bandHinhMau.Items.Add(new OutlookBarItem(AffineManager.DsDoiTuongVe[i].LoaiDoiTuongVe.Ten, i));
            }
            bandHinhMau.LargeImageList = listImage;
            bandHinhMau.SmallImageList = listImage;
            bandHinhMau.IconView = IconView.Large;
            //item cua 10 hình cơ bản
            //bandHinhMau.Items.Add(new OutlookBarItem("01.Đường thẳng", 0));
            //bandHinhMau.Items.Add(new OutlookBarItem("02.Hình tròn", 1));
            //bandHinhMau.Items.Add(new OutlookBarItem("03.Hình chữ nhật", 2));
            //bandHinhMau.Items.Add(new OutlookBarItem("04.Hình ellipse", 3));
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Image img = new Bitmap(picDraw.Width, picDraw.Height);
            Graphics gh = Graphics.FromImage(img);
            gh.Clear(Color.Black);
            picDraw.Image = img;
        }
        void Win2View(Image win)
        {
            picDraw.Image = win;
        }
        Image View2Win()
        {
            return new Bitmap(picDraw.Width, picDraw.Height);
        }
        private void myOutlookBar_ItemClicked(OutlookBarBand band, OutlookBarItem item)
        {
            groupInput.Controls.Clear();
            doiTuongHienTai = AffineManager.DsDoiTuongVe[item.ImageIndex].LoaiDoiTuongVe;
            groupInput.Controls.Add(doiTuongHienTai.GiaoDien);
            matrixView.UpdateView(doiTuongHienTai.MatrixBienHinh);
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            
        }

        IDoiTuongVe doiTuongHienTai = null;
        public void VeLaiDoiTuong()
        {
            Image img = View2Win();
            Graphics gh = Graphics.FromImage(img);
            gh.Clear(Color.Black);
            doiTuongHienTai.VeDoiTuong(gh);
            Win2View(img);
        }

        private void buttTinhTien_Click(object sender, EventArgs e)
        {
            if (doiTuongHienTai==null)
            {
                return;
            }
            float _x, _y;
            if (!float.TryParse(txtTinhTienX.Text, out _x) || !float.TryParse(txtTinhTienY.Text, out _y))
            {
                MessageBox.Show("Nhập thông tin tịnh tiến chưa đúng!");
                return;
            }
            Vector2D vec = new Vector2D();
            vec.X = _x;
            vec.Y = _y;
            doiTuongHienTai.MatrixBienHinh.MultMatrix(Matrix2D.CreateTranslate(vec));
            matrixView.UpdateView(doiTuongHienTai.MatrixBienHinh);
        }
    }
}
