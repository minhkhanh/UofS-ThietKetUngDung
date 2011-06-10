using System;
using System.Collections.Generic;
using System.Text;
using IObject;
using System.IO;
using System.Reflection;

namespace Affine
{
    class AffineManager
    {
        public static List<DoiTuongVe> DsDoiTuongVe = new List<DoiTuongVe>();
        public static void LayDoiTuongVe(string Duong_dan)
        {
            DsDoiTuongVe.Clear();
            foreach (string Tap_tin in Directory.GetFiles(Duong_dan)) 
            { 
                FileInfo file = new FileInfo(Tap_tin);
                if (file.Extension.Equals(".dll"))
                {
                    Assembly asm = Assembly.LoadFile(Tap_tin);
                    foreach (Type Loai in asm.GetTypes())
                    {
                        //Chọn kiểu mà có chứa giao diện Interface mà chúng ta đã cài đặt trước
                        //Điều này rất quan trọng nếu bạn ko kiểm tra sẽ xảy ra lỗi
                        Type Loai_Interface = Loai.GetInterface("IObject.IDoiTuongVe", true);
                        if (Loai_Interface != null)//Có Interface phù hợp
                        {
                            //Phần quan trọng nhất là ở đây
                            //asm.GetType(Loai_nhan_vat.ToString()) : Lấy kiểu từ một chuỗi
                            //Activator.CreateInstance : Tạo thể hiện từ một kiểu chi định
                            //Vì đã kiểm tra Loai_Interface nên ta có thể ép kiểu ở đây
                            DoiTuongVe nv = new DoiTuongVe();
                            nv.LoaiDoiTuongVe = (IDoiTuongVe)Activator.CreateInstance(asm.GetType(Loai.ToString()));
                            DsDoiTuongVe.Add(nv);
                        }
                    }
                    //if (TheHienDoiTuongVe(Tap_tin)!=null)
                    //{
                    //    DsDoiTuongVe.Add(TheHienDoiTuongVe(Tap_tin)); 
                    //}                    
                } 
            } 
        }
        public static DoiTuongVe TheHienDoiTuongVe(string Tap_tin)
        {
            DoiTuongVe nv = new DoiTuongVe();
            //Nạp thông tin của một file Dll vào
            //Do đã kiểm tra phần mở rộng là Dll
            //Muốn tìm hiểu về Assembly->MSDN
            Assembly asm = Assembly.LoadFile(Tap_tin);
            //Một asm sẽ chứa nhiều kiểu(Type) khác nhau
            //Duyệt qua từng kiểu(Type) trong tập hợp kiểu(asm.GetTypes())
            foreach (Type Loai in asm.GetTypes())
            {
                //Chọn kiểu mà có chứa giao diện Interface mà chúng ta đã cài đặt trước
                //Điều này rất quan trọng nếu bạn ko kiểm tra sẽ xảy ra lỗi
                Type Loai_Interface = Loai.GetInterface("IObject.IDoiTuongVe", true);
                if (Loai_Interface != null)//Có Interface phù hợp
                {
                    //Phần quan trọng nhất là ở đây
                    //asm.GetType(Loai_nhan_vat.ToString()) : Lấy kiểu từ một chuỗi
                    //Activator.CreateInstance : Tạo thể hiện từ một kiểu chi định
                    //Vì đã kiểm tra Loai_Interface nên ta có thể ép kiểu ở đây
                    nv.LoaiDoiTuongVe = (IDoiTuongVe)Activator.CreateInstance(asm.GetType(Loai.ToString()));
                    return nv;
                }
            }
            return null;
        }
    }
}
