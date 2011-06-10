using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using IObject;

namespace Affine
{
    public partial class MatrixView : UserControl
    {
        public MatrixView()
        {
            InitializeComponent();
        }
        public void UpdateView(Matrix2D m)
        {
            txt1.Text = m.matrix[0, 0].ToString();
            txt2.Text = m.matrix[0, 1].ToString();
            txt3.Text = m.matrix[0, 2].ToString();
            txt4.Text = m.matrix[1, 0].ToString();
            txt5.Text = m.matrix[1, 1].ToString();
            txt6.Text = m.matrix[1, 2].ToString();
            txt7.Text = m.matrix[2, 0].ToString();
            txt8.Text = m.matrix[2, 1].ToString();
            txt9.Text = m.matrix[2, 2].ToString();
        }
    }
}
