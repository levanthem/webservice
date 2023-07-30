using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;


namespace MyWebService
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public int tongHaiSo(int a, int b)
        {
            return a+b;
        }
        [WebMethod]
        public string PTB2(double a, double b, double c)
        {
            if (a==0)
            {
                if(b == 0 && c == 0){
                    return "PT vô Số nghiệm";
                }
                else if(b == 0 && c != 0){
                    return "PT Vô nghiệm";
                }
                return "x=" + (-c)/b;

            }
            else
            {
                double delta = Math.Pow(b, 2) -4*a*c;
                if (delta <0) return "Vô Nghiệm";
                if (delta ==0) return "x1 =x2=" + (-b/(2*a));
                else
                {
                    double x1 = -b - Math.Sqrt(delta)/(2*a);
                    double x2 = -b + Math.Sqrt(delta)/(2*a);
                    return "x1=" + x1 +";x2=" + x2;
                }
            }

        }
        [WebMethod]
        public int tongSoSinhVien()
        {
            CSDL_QLVSDataContext context = new CSDL_QLVSDataContext();
            return context.SInhViens.Count();
        }
        [WebMethod]
        public int tongSinhVienTheoLop(int maLop)
        {
            CSDL_QLVSDataContext context = new CSDL_QLVSDataContext();
            return context.SInhViens.Where(x => x.MaLop==maLop).Count();
        }
        [WebMethod]
        public SInhVien chitietSinhVien(int ma)
        {
            CSDL_QLVSDataContext context = new CSDL_QLVSDataContext();
            SInhVien sv = context.SInhViens.FirstOrDefault(x => x.Ma==ma);
            if (sv !=null)
            {
                sv.MaLop =null;
            }
            return sv;
        }
        [WebMethod]
        public LopHoc chiTietMaLop(int ma)
        {
            CSDL_QLVSDataContext context = new CSDL_QLVSDataContext();
            LopHoc lophoc = context.LopHocs.FirstOrDefault(x => x.MaLop==ma);
            if (lophoc!=null)
            {
                lophoc.SInhViens.Clear();
               
            }
            return lophoc;
        }

        [WebMethod]
        public List<SInhVien> danhsachSinhVientheoMa(int ma)
        {
            CSDL_QLVSDataContext context = new CSDL_QLVSDataContext();
            List<SInhVien> dssv = context.SInhViens.Where(x=> x.MaLop==ma).ToList();
            foreach (SInhVien sv in dssv)
                    sv.MaLop =null; 
            return dssv;

        }

        [WebMethod]
        public List<LopHoc> danhsachLophoc()
        {
            CSDL_QLVSDataContext context = new CSDL_QLVSDataContext();
            List<LopHoc> dslop = context.LopHocs.ToList();
            foreach (LopHoc lop in dslop){
                lop.SInhViens.Clear();
            }  
            return dslop;
        }
        [WebMethod]
        public bool ThemSinhVien(int maSV, String hotenSV, DateTime namsinhSV, int maLopSV)
        {
            try
            {
                SInhVien sv = new SInhVien();
                sv.Ma =maSV;
                sv.Ten = hotenSV;
                sv.NamSinh = namsinhSV; 
                sv.MaLop = maLopSV;
                CSDL_QLVSDataContext context = new CSDL_QLVSDataContext();
                context.SInhViens.InsertOnSubmit(sv);
                context.SubmitChanges();    
                return true;    
            }
            catch{ }
            return false;
        }
        [WebMethod]
        public bool ThemLopHoc(int malop, string tenlop)
        {
            try
            {
                LopHoc lop = new LopHoc();
                lop.MaLop = malop;
                lop.TenLop = tenlop;
                CSDL_QLVSDataContext context = new CSDL_QLVSDataContext();
                context.LopHocs.InsertOnSubmit(lop);
                context.SubmitChanges();
                return true;

            }
            catch { }
            return false;
        }
        [WebMethod]
        public List<SInhVien>danhsachSinhVien()
        {
            CSDL_QLVSDataContext context =new CSDL_QLVSDataContext();
            List<SInhVien> dssv = context.SInhViens.ToList();
            foreach(SInhVien sv in dssv)
            {
                sv.MaLop =null;
            }
            return dssv;
        }
    }
}
