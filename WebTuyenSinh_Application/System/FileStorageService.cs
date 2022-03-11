using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebTuyenSinh.Data.Entityes;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Drawing;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using WebTuyenSinh_Application.ViewApi;
using System.Data;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace WebTuyenSinh_Application.System
{
    public class UserDetails
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
    public class FileStorageService : IStorageService
    {
        private readonly string _userContentFolder;
        private IWebHostEnvironment _webHostEnvironment;
        private HeThongTuyenSinhDB _context;
        public FileStorageService(IWebHostEnvironment webHostEnvironment , HeThongTuyenSinhDB context)
        {
            _webHostEnvironment= webHostEnvironment;
            _context = context;
        }
        public  string CreatePdf(ProfileStudent profile)
        {
            //if (profile.url!=null)
            //{
            //    await DeleteFileAsync(profile.url.Trim());
            //}
          return CreateFileProfile(profile);
          
        }

        public async Task DeleteFileAsync(string fileName)
        {
            fileName = fileName.Replace(@"/", "\\");
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName.Substring(1));

            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }


        public string GetFileUrl(string fileName)
        {
            throw new NotImplementedException();
        }

 
     public string CreateFileProfile(ProfileStudent profile)
        {
            try {

            PdfDocument doc = new PdfDocument();

            // Create one page
            PdfPageBase page = doc.Pages.Add();


            //Draw the text
            int height = 0;

            PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Times New Roman", 13f), true);
          

            page.Canvas.DrawString("TRƯỜNG ĐẠI HỌC GTVT",
                           font1,
                              new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                              35, height);
            font1 = new PdfTrueTypeFont(new Font("Times New Roman", 13f, FontStyle.Bold), true);

            page.Canvas.DrawString("CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM",
                           font1,
                              new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                              280, height);
                    
            height += 12;
            page.Canvas.DrawString("PHÂN HIỆU TẠI TP. HỒ CHÍ MINH",
                           font1,
                              new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                              0, height);


            page.Canvas.DrawString("Độc lập - Tự do - Hạnh phúc",
                           font1,
                              new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                              320, height);
            PdfPen pen = new PdfPen(Color.FromArgb(25, 25, 112), 1f);


            height += 30;
            page.Canvas.DrawLine(pen, new PointF(400, height), new PointF(400, height + 20));
            page.Canvas.DrawLine(pen, new PointF(510, height), new PointF(510, height + 20));
            page.Canvas.DrawLine(pen, new PointF(400, height), new PointF(510, height));
            font1 = new PdfTrueTypeFont(new Font("Times New Roman", 12f, FontStyle.Underline), true);
            page.Canvas.DrawString("Số hồ sơ:  ",
                  font1,
                     new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                     405, height + 3);
            height += 20;
            page.Canvas.DrawLine(pen, new PointF(400, height), new PointF(510, height));
            page.Canvas.DrawLine(pen, new PointF(320, 28), new PointF(480, 28));


            height += 20;
            font1 = new PdfTrueTypeFont(new Font("Times New Roman", 15f, FontStyle.Bold), true);
            page.Canvas.DrawString("PHIẾU ĐĂNG KÝ XÉT TUYỂN ĐẠI HỌC CHÍNH QUY ",
                           font1,
                              new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                              110, height);


            font1 = new PdfTrueTypeFont(new Font("Times New Roman", 10f, FontStyle.Italic), true);
            height += 27;
            page.Canvas.DrawString("(Dành cho thí sinh xét tuyển bằng học bạ)",
                           font1,
                              new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                              220, height - 5);


            height = 60;
            page.Canvas.DrawLine(pen, new PointF(18, 28), new PointF(200, 28));
            pen = new PdfPen(Color.Black, 0.3f);
            page.Canvas.DrawRectangle(pen, new Rectangle(new Point(5, 45), new Size(80, 100)));


            page.Canvas.DrawString("Ảnh chụp",
                       font1,
                          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                          24, height);
            height += 11;
            page.Canvas.DrawString("kiểu CMND,",

                     font1,
                        new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                        18, height);
            height += 11;
            page.Canvas.DrawString("không quá 6",
                  font1,
                     new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                     18, height);
            height += 11;
            page.Canvas.DrawString("tháng .",
                  font1,
                     new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                     18, height);
            font1 = new PdfTrueTypeFont(new Font("Times New Roman", 10f, FontStyle.Bold), true);

            page.Canvas.DrawString("Đóng",
                 font1,
                    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                    43, height);
            height += 11;

            page.Canvas.DrawString("dấu giáp lai",
               font1,
                  new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                  18, height);
            height += 10;
            page.Canvas.DrawString("lên ảnh",
             font1,
                new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                18, height);

            page.Canvas.DrawString("\t\t\t\t(*)",
            font1,
               new PdfSolidBrush(Color.FromArgb(245, 44, 44)),
               14, height);



            font1 = new PdfTrueTypeFont(new Font("Times New Roman", 13f, FontStyle.Bold), true);
                PdfTrueTypeFont   font2 = new PdfTrueTypeFont(new Font("Times New Roman", 13f), true);


            height += 40;
            page.Canvas.DrawString("Kính gửi:",
  font1,
     new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
     0, height);
            page.Canvas.DrawString("Hội đồng tuyển sinh Đại học hệ chính quy năm "+profile.CreateDate.Value.Date.Year, font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), 60, height);

            PdfTrueTypeFont font3 = new PdfTrueTypeFont(new Font("Times New Roman", 11f, FontStyle.Italic), true);

            height += 25;

            page.Canvas.DrawString("THÔNG TIN THÍ SINH",
      font1,
         new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
         0, height);
            height += 25;
            page.Canvas.DrawString("1. Họ, chữ đệm và tên của thí sinh:",
         font1,
            new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
            0, height);

            page.Canvas.DrawString("\t (Viết đúng như trong giấy khai sinh bằng chữ in hoa có dấu)", font3, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), 190, height);

            height += 22;

            page.Canvas.DrawString(profile.Name.Trim().ToUpper(), font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), 65, height);

            page.Canvas.DrawString(" Nam/Nữ:",
font1,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
300, height);
            page.Canvas.DrawString("(Nữ ghi 1, Nam ghi 0)",
font3,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
360, height);
            page.Canvas.DrawString(profile.Sex.ToString(),
font2,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
488, height - 3);

            page.Canvas.DrawRectangle(pen, new Rectangle(new Point(480, height - 5), new Size(20, 20)));


            height += 22;
            page.Canvas.DrawString("2. Ngày sinh:",
       font1,
          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
          0, height);

            page.Canvas.DrawString(profile.BirthDay.Value.Date.ToShortDateString(),
     font2,
        new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
        75, height);

            page.Canvas.DrawString("3. Nơi sinh: ",
font1,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
310, height);
            page.Canvas.DrawString(profile.FromBirthDay.Trim(),
font2,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
380, height);

            height += 22;
            page.Canvas.DrawString("4. Dân tộc:  ",
       font1,
          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
          0, height);

            page.Canvas.DrawString(profile.Nation.Trim(),
     font2,
        new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
        70, height);

            page.Canvas.DrawString("5. Tôn giáo: ",
font1,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
310, height);
            page.Canvas.DrawString(profile.Religion.Trim(),
font2,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
380, height);


            height += 22;
            page.Canvas.DrawString("6. Số CMND/CCCD : ",
       font1,
          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
          0, height);

            pen = new PdfPen(Color.FromArgb(25, 25, 112), 0.5f);
            string cmnd = ""+ profile.CMND.Trim();
            int weight = 130;

            if (cmnd.Length < 12)
            {
                int size = 12 - cmnd.Length;
                for (int i = 0; i < size; i++)
                {
                    cmnd = "/" + cmnd;
                }
            }
            for (int i = 0; i < 13; i++)
            {

                page.Canvas.DrawLine(pen, new PointF(weight, height - 2), new PointF(weight, height + 18));
                if (i < 12 && cmnd[i]! != '/')
                {
                    page.Canvas.DrawString(" " + cmnd[i],
     font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
        weight + 5, height);

                }

                weight += 22;
            }
            pen = new PdfPen(Color.FromArgb(25, 25, 112), 0.5f);
            page.Canvas.DrawLine(pen, new PointF(130, height - 2), new PointF(weight - 21, height - 2));
            page.Canvas.DrawLine(pen, new PointF(130, height + 18), new PointF(weight - 21, height + 18));


            height += 25;

            page.Canvas.DrawString("Ngày cấp: "+profile.DateRange.Value.ToShortDateString().Trim()+$"	\t\t\t\t\t\t Nơi cấp: {profile.AdressRange}	",
   font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
      15, height);

            height += 22;
            page.Canvas.DrawString("7. Hộ khẩu thường trú:",
       font1,
          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
          0, height);


            page.Canvas.DrawString(""+profile.Adress.Trim(),
     font2,
        new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
       130, height);
       
            height += 22;
            page.Canvas.DrawString("8. Nơi học THPT hoặc tương đương: ",
       font1,
          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
          0, height);

            height += 22;
            page.Canvas.DrawString("\tGhi rõ tên trường và nơi trường đóng: huyện (quận), tỉnh (TP)                                              ",
     font3,
        new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
        0, height);

            page.Canvas.DrawString("  Mã tỉnh   \t   Mã trường    ",
font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
365, height);
            height += 22;
            var school =  _context.Schools.Find(profile.Shoo1);
            page.Canvas.DrawString("Năm lớp 10: "+school.NameShool.Trim(),
     font2,
        new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
        10, height);


            pen = new PdfPen(Color.FromArgb(25, 25, 112), 1f);
            page.Canvas.DrawLine(pen, new PointF(365, height), new PointF(415, height));

            page.Canvas.DrawLine(pen, new PointF(440, height), new PointF(515, height));
            height += 2;



            page.Canvas.DrawString(""+ school.idDistrict[0],
 font2,
    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
    370, height);
            page.Canvas.DrawString(""+ school.idDistrict[1],
 font2,
    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
    395, height);

            // shooll
            page.Canvas.DrawString(""+school.idShool[0],
font2,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
445, height);
            page.Canvas.DrawString(""+ school.idShool[1],
 font2,
    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
    470, height);

            page.Canvas.DrawString(""+ school.idShool[2],
font2,
  new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
  495, height);
            page.Canvas.DrawLine(pen, new PointF(365, height), new PointF(365, height + 20));

            page.Canvas.DrawLine(pen, new PointF(390, height), new PointF(390, height + 20));

            page.Canvas.DrawLine(pen, new PointF(415, height), new PointF(415, height + 20));

            page.Canvas.DrawLine(pen, new PointF(440, height), new PointF(440, height + 20));

            page.Canvas.DrawLine(pen, new PointF(465, height), new PointF(465, height + 20));

            page.Canvas.DrawLine(pen, new PointF(490, height), new PointF(490, height + 20));
            page.Canvas.DrawLine(pen, new PointF(515, height), new PointF(515, height + 20));

            height += 20;


            page.Canvas.DrawLine(pen, new PointF(365, height), new PointF(415, height));
            page.Canvas.DrawLine(pen, new PointF(440, height), new PointF(515, height));


            height += 5;
            school =  _context.Schools.Find(profile.Shoo2);
            page.Canvas.DrawString("Năm lớp 11: "+school.NameShool.Trim(),
font2,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
10, height);



            pen = new PdfPen(Color.FromArgb(25, 25, 112), 1f);
            page.Canvas.DrawLine(pen, new PointF(365, height), new PointF(415, height));

            page.Canvas.DrawLine(pen, new PointF(440, height), new PointF(515, height));
            height += 2;

            page.Canvas.DrawString("" + school.idDistrict[0],
 font2,
    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
    370, height);
            page.Canvas.DrawString("" + school.idDistrict[1],
 font2,
    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
    395, height);

            // shooll
            page.Canvas.DrawString("" + school.idShool[0],
font2,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
445, height);
            page.Canvas.DrawString("" + school.idShool[1],
 font2,
    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
    470, height);

            page.Canvas.DrawString("" + school.idShool[2],
font2,
  new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
  495, height);
            page.Canvas.DrawLine(pen, new PointF(365, height), new PointF(365, height + 20));

            page.Canvas.DrawLine(pen, new PointF(390, height), new PointF(390, height + 20));

            page.Canvas.DrawLine(pen, new PointF(415, height), new PointF(415, height + 20));

            page.Canvas.DrawLine(pen, new PointF(440, height), new PointF(440, height + 20));

            page.Canvas.DrawLine(pen, new PointF(465, height), new PointF(465, height + 20));

            page.Canvas.DrawLine(pen, new PointF(490, height), new PointF(490, height + 20));
            page.Canvas.DrawLine(pen, new PointF(515, height), new PointF(515, height + 20));

            height += 20;


            page.Canvas.DrawLine(pen, new PointF(365, height), new PointF(415, height));
            page.Canvas.DrawLine(pen, new PointF(440, height), new PointF(515, height));


            height += 5;
            school =  _context.Schools.Find(profile.Shoo3);
            page.Canvas.DrawString("Năm lớp 12: " + school.NameShool.Trim(),
font2,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
10, height);

            pen = new PdfPen(Color.FromArgb(25, 25, 112), 1f);
            page.Canvas.DrawLine(pen, new PointF(365, height), new PointF(415, height));

            page.Canvas.DrawLine(pen, new PointF(440, height), new PointF(515, height));
            height += 2;

            page.Canvas.DrawString("" + school.idDistrict[0],
 font2,
    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
    370, height);
            page.Canvas.DrawString("" + school.idDistrict[1],
 font2,
    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
    395, height);

            // shooll
            page.Canvas.DrawString("" + school.idShool[0],
font2,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
445, height);
            page.Canvas.DrawString("" + school.idShool[1],
 font2,
    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
    470, height);

            page.Canvas.DrawString("" + school.idShool[2],
font2,
  new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
  495, height);
            page.Canvas.DrawLine(pen, new PointF(365, height), new PointF(365, height + 20));

            page.Canvas.DrawLine(pen, new PointF(390, height), new PointF(390, height + 20));

            page.Canvas.DrawLine(pen, new PointF(415, height), new PointF(415, height + 20));

            page.Canvas.DrawLine(pen, new PointF(440, height), new PointF(440, height + 20));

            page.Canvas.DrawLine(pen, new PointF(465, height), new PointF(465, height + 20));

            page.Canvas.DrawLine(pen, new PointF(490, height), new PointF(490, height + 20));
            page.Canvas.DrawLine(pen, new PointF(515, height), new PointF(515, height + 20));

            height += 20;

            page.Canvas.DrawLine(pen, new PointF(365, height), new PointF(415, height));
            page.Canvas.DrawLine(pen, new PointF(440, height), new PointF(515, height));

            height += 10;
            page.Canvas.DrawString("9. Điện thoại di động:  ",
       font1,
          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
          0, height);

            page.Canvas.DrawString(""+profile.Teletephone.Trim(),
     font2,
        new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
        130, height);

            page.Canvas.DrawString("10. Email: ",
font1,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
280, height);
            page.Canvas.DrawString(""+profile.Email.ToLower().Trim(),
font2,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
340, height);


            height += 22;
            page.Canvas.DrawString("11. Địa chỉ liên hệ:",
       font1,
          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
          0, height);


            page.Canvas.DrawString(" "+profile.FromTelePhone.Trim(),
     font2,
        new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
       130, height);



            height += 22;

            page.Canvas.DrawString("12. Năm tốt nghiệp: ",
       font1,
          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
          0, height);

            page.Canvas.DrawString(" "+profile.Year,
     font2,
        new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
        120, height);

            page.Canvas.DrawString("Khu vực: ",
font1,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
170, height);
            page.Canvas.DrawString(""+ (profile.Areas != null ? profile.Areas.Trim() : "Không Có"), font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
228, height);



            page.Canvas.DrawString("Đối tượng ưu tiên (nếu có): ",
font1,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
295, height);
            page.Canvas.DrawString("" + (profile.Priority_object != null?profile.Priority_object.Trim():"Không Có"), font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
442, height);
                height += 22;
                page.Canvas.DrawString("THÔNG TIN ĐĂNG KÝ XÉT TUYỂN:", font1, new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
           0, height);

                font1 = new PdfTrueTypeFont(new Font("Times New Roman", 12f, FontStyle.Bold), true);

                string text = "\t Sau khi đã đọc và hiểu rõ các quy định về tiêu chí và điều kiện xét tuyển của Nhà trường, tôi \t đăng ký xét tuyển vào trình độ Đại học các Ngành/ Nhóm ngành/Chuyên ngành/Nhóm chuyên ngành theo thứ tự ưu tiên như bảng sau:";
                PdfStringFormat format1  = new PdfStringFormat(PdfTextAlignment.Left);

                font2 = new PdfTrueTypeFont(new Font("Times New Roman", 13f), true);
                height += 40;
                page.Canvas.DrawString(text, font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(0, height, 510, height + 30), format1);
                page = doc.Pages.Add();
            height = 10;
            int h1 = height;
            page.Canvas.DrawLine(pen, new PointF(0, height), new PointF(515, height - 2));

            height += 5;
            page.Canvas.DrawString("Điểm trung bình 3 năm THPT", font1, new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
     280, height);

            font2 = new PdfTrueTypeFont(new Font("Times New Roman", 12f, FontStyle.Bold), true);
            page.Canvas.DrawString("TT", font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(5, height + 40, 510, height + 30), format1);

            page.Canvas.DrawString("\n   Ngành \n Xét Tuyển", font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(50, height, 510, height + 30), format1);

            page.Canvas.DrawString("Mã \n xét \ntuyển", font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(140, height + 20, 510, height + 30), format1);
            page.Canvas.DrawString("  Tổ\n hợp\n  xét\n tuyển ", font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(180, height + 10, 510, height + 30), format1);
            height += 20;
            page.Canvas.DrawLine(pen, new PointF(210, height), new PointF(515, height - 2));

            height += 20;
            page.Canvas.DrawLine(pen, new PointF(210, height), new PointF(515, height - 2));
            height -= 29;
            page.Canvas.DrawString("Môn 1  \t \t \t\t Môn 1 \t\t\t\t\t\t Môn 1", font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(240, height + 10, 510, height + 30), format1);
            
            height += 30;
            weight = 215;
            page.Canvas.DrawString("Lớp\tLớp\n 10\t\t11", font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(weight, height + 10, 510, height + 30), format1);
            weight += 55;

            page.Canvas.DrawString("\tHK \n \t I \n\tLớp \n\t 12", font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(weight, height, 510, height + 30), format1);

            weight += 42;
            height += 10;
            page.Canvas.DrawString("Lớp \t Lớp  \n 10 \t \t11", font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(weight, height, 510, height + 30), format1);
            weight += 60;
            page.Canvas.DrawString("\tHK \n \t I \n\tLớp \n\t 12", font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(weight, height - 10, 510, height + 30), format1);
            weight += 50;

            page.Canvas.DrawString("Lớp\tLớp  \n 10 \t  11", font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(weight, height, 510, height + 30), format1);
            weight += 60;

            page.Canvas.DrawString("\tHK \n \t I \n\tLớp \n\t 12", font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(weight - 3, height - 10, 510, height + 30), format1);
            height += 68;
            page.Canvas.DrawLine(pen, new PointF(0, height), new PointF(515, height - 2));
          var  height1 = 2;
            int cout =1 ;
            foreach (var item in profile.InforMationProflies)

            {
                if (height > 730)
                {
                    weight = 0;
                    weight += 25;
                    page.Canvas.DrawLine(pen, new PointF(weight, h1), new PointF(weight, height));

                    weight += 105;
                    page.Canvas.DrawLine(pen, new PointF(weight, h1), new PointF(weight, height));
                    weight += 450;
                    page.Canvas.DrawLine(pen, new PointF(weight, h1), new PointF(weight, height));

                    weight += 33;
                    page.Canvas.DrawLine(pen, new PointF(weight, h1), new PointF(weight, height));
                    weight += 35;

                    page.Canvas.DrawLine(pen, new PointF(weight, h1 + 46), new PointF(weight, height));

                    weight += 30;
                    page.Canvas.DrawLine(pen, new PointF(weight, h1 + 46), new PointF(weight, height));


                    weight += 30;
                    page.Canvas.DrawLine(pen, new PointF(weight, h1 + 25), new PointF(weight, height));


                    weight += 35;
                    page.Canvas.DrawLine(pen, new PointF(weight, h1 + 46), new PointF(weight, height));


                    weight += 37;
                    page.Canvas.DrawLine(pen, new PointF(weight, h1 + 46), new PointF(weight, height));

                    weight += 37;
                    page.Canvas.DrawLine(pen, new PointF(weight, h1 + 25), new PointF(weight, height));


                    weight += 37;
                    page.Canvas.DrawLine(pen, new PointF(weight, h1 + 46), new PointF(weight, height));


                    weight += 33;
                    page.Canvas.DrawLine(pen, new PointF(weight, h1 + 46), new PointF(weight, height));
                    page.Canvas.DrawLine(pen, new PointF(0, h1), new PointF(0, height));
                    page.Canvas.DrawLine(pen, new PointF(515, h1), new PointF(515, height));
                    height = 0;
                    h1 = 0;
                    PdfPageBase page1 = doc.Pages.Add();
                    page = page1;
                }
                font2 = new PdfTrueTypeFont(new Font("Times New Roman", 10f), true);
                page.Canvas.DrawString((cout) + "", font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(3, height, 510, height + 30), format1);
                ;
                cout++;
                weight = 30;
                var major =  _context.Majors.Find(item.idMajor);
                page.Canvas.DrawString(major.Name.Trim(), font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(weight, height+2, 90, height+2), format1);

                weight += 105;
                page.Canvas.DrawString(major.id.Trim(), font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(weight, height + height1, 510, height + height1), format1);

                weight += 50;
                page.Canvas.DrawString(""+item.idBlock.Trim(), font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(weight, height + height1, 510, height + height1), format1);

                weight += 36;
                page.Canvas.DrawString(Math.Round(item.subject1.Value, 2).ToString(), font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(weight, height + height1, 510, height + height1), format1);
                ;
                weight += 31;
                page.Canvas.DrawString(Math.Round(item.subject2.Value, 2).ToString(), font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(weight, height + height1, 510, height + height1), format1);
                weight += 29;
                page.Canvas.DrawString(Math.Round(item.subject3.Value, 2).ToString(), font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(weight, height + height1, 510, height + height1), format1);
                weight += 31;
                page.Canvas.DrawString(Math.Round(item.subject4.Value, 2).ToString(), font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(weight, height + height1, 510, height + height1), format1);
                ;
                weight += 35;
                page.Canvas.DrawString(Math.Round(item.subject5.Value, 2).ToString(), font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(weight, height + height1, 510, height + height1), format1);
                ;
                weight += 35;
                page.Canvas.DrawString(Math.Round(item.subject6.Value, 2).ToString(), font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(weight, height + height1, 510, height + height1), format1);
                ;
                weight += 38;
                page.Canvas.DrawString(Math.Round(item.subject7.Value, 2).ToString(), font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(weight, height + height1, 510, height + height1), format1);
                ;
                weight += 35;
                page.Canvas.DrawString(Math.Round(item.subject8.Value, 2).ToString(), font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(weight, height + height1, 510, height + height1), format1);
                weight += 32;
                page.Canvas.DrawString(Math.Round(item.subject9.Value,2).ToString(), font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(weight, height + height1, 510, height + height1), format1);

                if (major.Name.Length > 18)
                {
                    height += 30;
                    height1 = 4;
                }
                else
                {
                    height1 = 2;
                    height += 18;

                }

                page.Canvas.DrawLine(pen, new PointF(0, height), new PointF(515, height - 2));
            }
            weight = 0;
            weight += 25;
            page.Canvas.DrawLine(pen, new PointF(weight, h1), new PointF(weight, height));

            weight += 107;
            page.Canvas.DrawLine(pen, new PointF(weight, h1), new PointF(weight, height));
            weight += 45;
            page.Canvas.DrawLine(pen, new PointF(weight, h1), new PointF(weight, height));

            weight += 33;
            page.Canvas.DrawLine(pen, new PointF(weight, h1), new PointF(weight, height));
            weight += 35;
            if (h1 == 0)
            {
                page.Canvas.DrawLine(pen, new PointF(0, h1), new PointF(515, h1));
                page.Canvas.DrawLine(pen, new PointF(weight, h1), new PointF(weight, height));

                weight += 30;
                page.Canvas.DrawLine(pen, new PointF(weight, h1), new PointF(weight, height));


                weight += 30;
                page.Canvas.DrawLine(pen, new PointF(weight, h1), new PointF(weight, height));


                weight += 35;
                page.Canvas.DrawLine(pen, new PointF(weight, h1), new PointF(weight, height));


                weight += 37;
                page.Canvas.DrawLine(pen, new PointF(weight, h1), new PointF(weight, height));

                weight += 37;
                page.Canvas.DrawLine(pen, new PointF(weight, h1), new PointF(weight, height));


                weight += 33;
                page.Canvas.DrawLine(pen, new PointF(weight, h1), new PointF(weight, height));


                weight += 37;
                page.Canvas.DrawLine(pen, new PointF(weight, h1), new PointF(weight, height));
            }
            else
            {
                page.Canvas.DrawLine(pen, new PointF(weight, h1 + 46), new PointF(weight, height));

                weight += 30;
                page.Canvas.DrawLine(pen, new PointF(weight, h1 + 46), new PointF(weight, height));


                weight += 30;
                page.Canvas.DrawLine(pen, new PointF(weight, h1 + 25), new PointF(weight, height));


                weight += 35;
                page.Canvas.DrawLine(pen, new PointF(weight, h1 + 46), new PointF(weight, height));


                weight += 37;
                page.Canvas.DrawLine(pen, new PointF(weight, h1 + 46), new PointF(weight, height));

                weight += 37;
                page.Canvas.DrawLine(pen, new PointF(weight, h1 + 25), new PointF(weight, height));


                weight += 37;
                page.Canvas.DrawLine(pen, new PointF(weight, h1 + 46), new PointF(weight, height));


                weight += 33;
                page.Canvas.DrawLine(pen, new PointF(weight, h1 + 46), new PointF(weight, height));
            }


            page.Canvas.DrawLine(pen, new PointF(0, h1), new PointF(0, height));
            page.Canvas.DrawLine(pen, new PointF(515, h1), new PointF(515, height));

            text = "\t Tôi xin cam đoan những lời khai trên là đúng sự thật. Nếu có gì sai trái, tôi xin chịu trách nhiệm và bị xử lý theo Quy chế tuyển sinh hiện hành của Bộ Giáo dục và Đào tạo";
            format1 = new PdfStringFormat(PdfTextAlignment.Left);


            height += 10;
            page.Canvas.DrawString(text, font3, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(0, height, 500, height), format1);

            format1 = new PdfStringFormat(PdfTextAlignment.Right);
            height += 25;
            if (height > 730)
            {
                height = 0;
                PdfPageBase page1 = doc.Pages.Add();
                page = page1;
            }
            font1 = new PdfTrueTypeFont(new Font("Times New Roman", 12f), true);
            height += 10;
            page.Canvas.DrawString("…………….., ngày "+profile.CreateDate.Value.Date.Day+" tháng "+ profile.CreateDate.Value.Date.Month+ " năm "+ profile.CreateDate.Value.Date.Year, font1, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(0, height, 500, height), format1);

            height += 30;
            if (height > 700)
            {
                height = 0;
                PdfPageBase page1 = doc.Pages.Add();
                page = page1;
            }
            font1 = new PdfTrueTypeFont(new Font("Times New Roman", 13f, FontStyle.Bold), true);
            font2 = new PdfTrueTypeFont(new Font("Times New Roman", 9, FontStyle.Italic), true);

            page.Canvas.DrawString("NGƯỜI ĐĂNG KÝ",
                           font1,
                              new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                              350, height);
            height += 15;
            page.Canvas.DrawString("(Ký tên, ghi rõ họ tên)",
                       font2,
                          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                          375, height);
            height += 70;

            font1 = new PdfTrueTypeFont(new Font("Times New Roman", 12f), true);
            page.Canvas.DrawString(""+profile.Name.Trim().ToUpper(),
                       font1,
                          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                          355, height);
                string fileName =  "Admisstion/" + profile.idAdmisstion + "/" + profile.CMND.Trim() + "/" + profile.CMND.Trim()+"_"  +profile.Name.Trim() + "_" + profile.idAdmisstion + ".pdf";
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
            doc.SaveToFile(filePath);
            return "/" + fileName;
            }
            catch (Exception e) { return e.Message; }
        }

        public async Task<ApiResult> ExportExcell(long? id)
        {
            try
            {
                string fileName = "Profile/dulieu.xlsx";
                Admisstion admisstion = _context.Admisstions.Where(x => x.id == id).FirstOrDefault();
                var Profile = await _context.ProfileStudents.Where(x => x.idAdmisstion == id).ToListAsync();
                var ProfileInfor = await _context.InforMationProflies.ToListAsync();
                var School = await _context.Schools.ToListAsync();
                var Major = await _context.Majors.ToListAsync();
                var Block = await _context.Blocks.ToListAsync();
                int i = 0;
                if (admisstion.Type == 2) {
                    var result = (from p in Profile
                                  join I in ProfileInfor on p.id equals I.idProfile
                                  select new
                                  {
                                      STT = (i = i + 1),
                                      So_BD = "GSA" + I.id,
                                      SO_CMND = p.CMND,
                                      HO_TEN = p.Name.ToLower(),
                                      NGAY_SINH = p.BirthDay.Value.ToShortDateString(),
                                      KHU_VUC = p.Areas,
                                      DOI_TUONG = (p.Priority_object != null ? p.Priority_object : ""),
                                      THUTU_NV = I.STT,
                                      MA_NGANH = I.idMajor,
                                      TEN_NGANH = Major.FirstOrDefault(x => x.id.Trim().Equals(I.idMajor)).Name,
                                      MA_TOHOPMON = I.idBlock,
                                      TEN_TOHOPMON = Block.FirstOrDefault(x => x.id.Trim().Equals(I.idBlock.Trim())).Desscription,
                                      L10_Mon1 = I.subject1,
                                      L10_Mon2 = I.subject2,
                                      L10_Mon3 = I.subject3,

                                      L11_Mon1 = I.subject4,
                                      L11_Mon2 = I.subject5,
                                      L11_Mon3 = I.subject6,
                                      L12_Mon1 = I.subject7,
                                      L12_Mon2 = I.subject8,
                                      L12_Mon3 = I.subject9,
                                  }).ToList();
                    i = 0;

                    var result1 = (from p in Profile
                                   join I in ProfileInfor on p.id equals I.idProfile
                                   select new
                                   {
                                       STT = (i = i + 1),
                                       So_BD = "GSA" + I.id,
                                       SO_CMND = p.CMND,
                                       HO_TEN = p.Name.ToLower(),
                                       NGAY_SINH = p.BirthDay.Value.ToShortDateString(),
                                       KHU_VUC = p.Areas,
                                       DOI_TUONG = "",

                                       THUTU_NV = "",
                                       MA_NGANH = "",
                                       TEN_NGANH = "",
                                       MA_TOHOPMON = "",
                                       TEN_TOHOPMON = "",
                                       L10_Mon1 = "",
                                       L10_Mon2 = "",
                                       L10_Mon3 = "",

                                       L11_Mon1 = "",
                                       L11_Mon2 = "",
                                       L11_Mon3 = "",
                                       L12_Mon1 = "",
                                       L12_Mon2 = "",
                                       L12_Mon3 = "",
                                   }).ToList();
                 

                    try
                    {
                        await DeleteFileAsync($"/" + fileName);
                    }
                    catch { }

                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);

                    DataTable table = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(result), (typeof(DataTable)));
                    DataTable table1 = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(result1), (typeof(DataTable)));
                    FileInfo filePath1 = new FileInfo(filePath);

                    using (var excelPack = new ExcelPackage(filePath1))
                    {
                        var ws = excelPack.Workbook.Worksheets.Add("DanhSachNguyenVong");
                        ws.DefaultRowHeight = 12;
                        ws.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Row(1).Style.Font.Bold = true;
                        for (int j = 2; j < 6; j++)
                        {
                            ws.Column(j).Width = 20;
                        }
                        ws.Cells.LoadFromDataTable(table1, true, OfficeOpenXml.Table.TableStyles.None);

                        ws = excelPack.Workbook.Worksheets.Add("DanhSachThiSinh");
                        ws.DefaultRowHeight = 12;
                        ws.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Row(1).Style.Font.Bold = true;
                        for (int j = 2; j < 6; j++)
                        {
                            ws.Column(j).Width = 20;
                        }

                        ws.Cells[1, 1].LoadFromDataTable(table, true, OfficeOpenXml.Table.TableStyles.None);

                        excelPack.Save();
                    }
                    return new ApiResult() { Success = true, Message = "Không tìm thấy", Data = "/" + fileName };
                }
                else
                {
                    var result = (from p in Profile
                                  join I in ProfileInfor on p.id equals I.idProfile
                                  
                                  select new
                                  {
                                      SO_CMND = p.CMND,
                                      HO_TEN = p.Name.ToLower(),
                                      NGAY_SINH = p.BirthDay.Value.ToShortDateString(),
                                      KHU_VUC = p.Areas,
                                      DOI_TUONG = (p.Priority_object != null ? p.Priority_object : ""),
                                      THUTU_NV = I.STT,
                                      MA_NGANH = I.idMajor,
                                      TEN_NGANH = Major.FirstOrDefault(x => x.id.Trim().Equals(I.idMajor)).Name,
                                      MA_TOHOPMON = p.IdBlock,
                                      TEN_TOHOPMON = Block.FirstOrDefault(x=>x.id.Trim().Equals(p.IdBlock.Trim())).Desscription,
                                      DIEM_THI = p.Mark
                                  }). ToList();
                    try
                    {
                        await DeleteFileAsync($"/" + fileName);
                    }
                    catch { }
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
                    DataTable table = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(result), (typeof(DataTable)));
                    FileInfo filePath1 = new FileInfo(filePath);
                    using (var excelPack = new ExcelPackage(filePath1))
                    {
                        var ws = excelPack.Workbook. Worksheets.Add("DanhSachNguyenVong");
                        ws.DefaultRowHeight = 12;
                        ws.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Row(1).Style.Font.Bold = true;
                        for (int j = 1; j < 10; j++)
                        {
                            ws.Column(j).Width = 18;
                        }
                        ws.Cells.LoadFromDataTable(table, true, OfficeOpenXml.Table.TableStyles.None);

                        excelPack.Save();
                    }
                    return new ApiResult() { Success = true, Message = "Không tìm thấy", Data = "/" + fileName };
                }
                

            }
            catch (Exception e)
            {
                return new ApiResult() { Success = false, Message = "Lỗi server " + e.Message, Data = null };
            }
        }

        public Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            throw new NotImplementedException();
        }

        public string CreateTypeTwoPdf(ProfileStudent profile)
        {
            PdfDocument doc = new PdfDocument();

            // Create one page
            PdfPageBase page = doc.Pages.Add();


            //Draw the text
            int height = 0;

            PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Times New Roman", 13f), true);
            PdfTrueTypeFont font2 = new PdfTrueTypeFont(new Font("Times New Roman", 13f), true);
            page.Canvas.DrawString("TRƯỜNG ĐẠI HỌC GTVT",
                           font1,
                              new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                              35, height);


            font1 = new PdfTrueTypeFont(new Font("Times New Roman", 13f, FontStyle.Bold), true);

            page.Canvas.DrawString("CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM",
                           font1,
                              new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                              280, height);

            height += 12;
            page.Canvas.DrawString("PHÂN HIỆU TẠI TP. HỒ CHÍ MINH",
                           font1,
                              new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                              0, height);


            page.Canvas.DrawString("Độc lập - Tự do - Hạnh phúc",
                           font1,
                              new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                              320, height);
            PdfPen pen = new PdfPen(Color.FromArgb(25, 25, 112), 1f);
            page.Canvas.DrawLine(pen, new PointF(320, 28), new PointF(480, 28));
            height += 53;
            font1 = new PdfTrueTypeFont(new Font("Times New Roman", 15f, FontStyle.Bold), true);
            page.Canvas.DrawString("ĐƠN XÉT TUYỂN ĐẠI HỌC CHÍNH QUY",
                           font1,
                              new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                              155, height);
            font1 = new PdfTrueTypeFont(new Font("Times New Roman", 9f, FontStyle.Italic), true);
            height += 27;
            page.Canvas.DrawString("(Dành cho xét tuyển bằng kết quả thi đánh giá năng lực của Đại học Quốc gia Tp. Hồ Chí Minh)",
                           font1,
                              new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                              115, height - 5);


            height = 60;
            page.Canvas.DrawLine(pen, new PointF(18, 28), new PointF(200, 28));
            pen = new PdfPen(Color.Black, 0.3f);
            page.Canvas.DrawRectangle(pen, new Rectangle(new Point(5, 45), new Size(80, 100)));

            font1 = new PdfTrueTypeFont(new Font("Times New Roman", 10f, FontStyle.Italic), true);
            page.Canvas.DrawString("Ảnh chụp",
                       font1,
                          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                          24, height);
            height += 11;
            page.Canvas.DrawString("kiểu CMND,",

                     font1,
                        new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                        18, height);
            height += 11;
            page.Canvas.DrawString("không quá 6",
                  font1,
                     new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                     18, height);
            height += 11;
            page.Canvas.DrawString("tháng .",
                  font1,
                     new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                     18, height);
            font1 = new PdfTrueTypeFont(new Font("Times New Roman", 10f, FontStyle.Bold), true);

            page.Canvas.DrawString("Đóng",
                 font1,
                    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                    43, height);
            height += 11;

            page.Canvas.DrawString("dấu giáp lai",
               font1,
                  new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                  18, height);
            height += 10;
            page.Canvas.DrawString("lên ảnh",
             font1,
                new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                18, height);

            page.Canvas.DrawString("\t\t\t\t(*)",
            font1,
               new PdfSolidBrush(Color.FromArgb(245, 44, 44)),
               14, height);

            height += 45;


            font1 = new PdfTrueTypeFont(new Font("Times New Roman", 13f, FontStyle.Bold), true);
      


            page.Canvas.DrawString("Kính gửi:",
      font1,
         new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
         0, height);
            page.Canvas.DrawString($"Hội đồng tuyển sinh Đại học hệ chính quy năm {profile.CreateDate.Value.Year}", font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), 60, height);
            height += 25;
            page.Canvas.DrawString("THÔNG TIN THÍ SINH",
      font1,
         new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
         0, height);

            height += 25;
            page.Canvas.DrawString("1. Họ tên:",
         font1,
            new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
            0, height);
            page.Canvas.DrawString(""+profile.Name, font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), 65, height);

            page.Canvas.DrawString("2. Nam/Nữ:",
font1,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
300, height);
            page.Canvas.DrawString((profile?.Sex==0?"Nam":"Nữ"),
font2,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
370, height);

            height += 22;
            page.Canvas.DrawString("3. Ngày sinh:",
       font1,
          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
          0, height);

            page.Canvas.DrawString($"{profile?.BirthDay.Value.Day+"/"+ profile?.BirthDay.Value.Month +"/"+ profile?.BirthDay.Value.Year}",
     font2,
        new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
        75, height);

            page.Canvas.DrawString("4. Nơi sinh: ",
font1,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
300, height);
            page.Canvas.DrawString($"{profile.FromBirthDay.ToLower()}",
font2,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
370, height);

            height += 22;
            page.Canvas.DrawString("5. Số CMND/CCCD : ",
       font1,
          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
          0, height);
            string cmnd = "" + profile.CMND.Trim();

            if (cmnd.Length < 12)
            {
                int size = 12 - cmnd.Length;
                for (int i = 0; i < size; i++)
                {
                    cmnd = "/" + cmnd;
                }
            }
            var weight = 130;
            for (int i = 0; i < 13; i++)
            {

                page.Canvas.DrawLine(pen, new PointF(weight, height - 2), new PointF(weight, height + 18));
                if (i < 12 && cmnd[i]! != '/')
                {
                    page.Canvas.DrawString(" " + cmnd[i],
     font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
        weight + 5, height);

                }
                weight += 22;
            }
            pen = new PdfPen(Color.FromArgb(25, 25, 112), 0.5f);
            page.Canvas.DrawLine(pen, new PointF(130, height - 2), new PointF(weight - 21, height - 2));
            page.Canvas.DrawLine(pen, new PointF(130, height + 18), new PointF(weight - 21, height + 18));


            height += 25;
            
            page.Canvas.DrawString($"Ngày cấp:  {profile?.DateRange.Value.Day+"/"+ profile?.DateRange.Value.Month+"/"+ profile?.DateRange.Value.Year}	\t\t\t\t\t\t Nơi cấp: {profile.AdressRange.ToLower()}	",
   font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
      15, height);

            height += 22;
            page.Canvas.DrawString("6. Địa chỉ liên hệ: ",
       font1,
          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
          0, height);

            page.Canvas.DrawString($" {profile.FromTelePhone} ",
     font2,
        new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
       100, height);


            height += 22;
            page.Canvas.DrawString("7. Điện thoại di động:  ",
       font1,
          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
          0, height);

            page.Canvas.DrawString($" {profile.Teletephone} ",
     font2,
        new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
        130, height);

            page.Canvas.DrawString("8. Email: ",
font1,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
300, height);
            page.Canvas.DrawString($" {profile.Email.ToLower()} ",
font2,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
360, height);



            height += 22;
            page.Canvas.DrawString("9. Nơi học THPT hoặc tương đương: ",
       font1,
          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
          0, height);

            height += 22;
            page.Canvas.DrawString("\tGhi rõ tên trường và nơi trường đóng: huyện (quận), tỉnh (TP)                                              ",
     font2,
        new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
        0, height);

            page.Canvas.DrawString("  Mã tỉnh   \t   Mã trường    ",
font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
365, height);
            height += 22;
            var school = _context.Schools.Find(profile.Shoo1);
            page.Canvas.DrawString("Năm lớp 10: " + school.NameShool.Trim(),
     font2,
        new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
        10, height);


            pen = new PdfPen(Color.FromArgb(25, 25, 112), 1f);
            page.Canvas.DrawLine(pen, new PointF(365, height), new PointF(415, height));

            page.Canvas.DrawLine(pen, new PointF(440, height), new PointF(515, height));
            height += 2;
            page.Canvas.DrawString("" + school.idDistrict[0],
 font2,
    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
    370, height);
            page.Canvas.DrawString("" + school.idDistrict[1],
 font2,
    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
    395, height);

            // shooll
            page.Canvas.DrawString("" + school.idShool[0],
font2,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
445, height);
            page.Canvas.DrawString("" + school.idShool[1],
 font2,
    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
    470, height);

            page.Canvas.DrawString("" + school.idShool[2],
font2,
  new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
  495, height);
            page.Canvas.DrawLine(pen, new PointF(365, height), new PointF(365, height + 20));

            page.Canvas.DrawLine(pen, new PointF(390, height), new PointF(390, height + 20));

            page.Canvas.DrawLine(pen, new PointF(415, height), new PointF(415, height + 20));

            page.Canvas.DrawLine(pen, new PointF(440, height), new PointF(440, height + 20));

            page.Canvas.DrawLine(pen, new PointF(465, height), new PointF(465, height + 20));

            page.Canvas.DrawLine(pen, new PointF(490, height), new PointF(490, height + 20));
            page.Canvas.DrawLine(pen, new PointF(515, height), new PointF(515, height + 20));

            height += 20;


            page.Canvas.DrawLine(pen, new PointF(365, height), new PointF(415, height));
            page.Canvas.DrawLine(pen, new PointF(440, height), new PointF(515, height));


            height += 5;
            school = _context.Schools.Find(profile.Shoo2);
            page.Canvas.DrawString("Năm lớp 11: " + school.NameShool.Trim(),
font2,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
10, height);



            pen = new PdfPen(Color.FromArgb(25, 25, 112), 1f);
            page.Canvas.DrawLine(pen, new PointF(365, height), new PointF(415, height));

            page.Canvas.DrawLine(pen, new PointF(440, height), new PointF(515, height));
            height += 2;

            page.Canvas.DrawString("" + school.idDistrict[0],
 font2,
    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
    370, height);
            page.Canvas.DrawString("" + school.idDistrict[1],
 font2,
    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
    395, height);

            // shooll
            page.Canvas.DrawString("" + school.idShool[0],
font2,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
445, height);
            page.Canvas.DrawString("" + school.idShool[1],
 font2,
    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
    470, height);

            page.Canvas.DrawString("" + school.idShool[2],
font2,
  new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
  495, height);
            page.Canvas.DrawLine(pen, new PointF(365, height), new PointF(365, height + 20));

            page.Canvas.DrawLine(pen, new PointF(390, height), new PointF(390, height + 20));

            page.Canvas.DrawLine(pen, new PointF(415, height), new PointF(415, height + 20));

            page.Canvas.DrawLine(pen, new PointF(440, height), new PointF(440, height + 20));

            page.Canvas.DrawLine(pen, new PointF(465, height), new PointF(465, height + 20));

            page.Canvas.DrawLine(pen, new PointF(490, height), new PointF(490, height + 20));
            page.Canvas.DrawLine(pen, new PointF(515, height), new PointF(515, height + 20));

            height += 20;


            page.Canvas.DrawLine(pen, new PointF(365, height), new PointF(415, height));
            page.Canvas.DrawLine(pen, new PointF(440, height), new PointF(515, height));


            height += 5;
            school = _context.Schools.Find(profile.Shoo3);
            page.Canvas.DrawString("Năm lớp 12: " + school.NameShool.Trim(),
font2,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
10, height);

            pen = new PdfPen(Color.FromArgb(25, 25, 112), 1f);
            page.Canvas.DrawLine(pen, new PointF(365, height), new PointF(415, height));

            page.Canvas.DrawLine(pen, new PointF(440, height), new PointF(515, height));
            height += 2;

            page.Canvas.DrawString("" + school.idDistrict[0],
 font2,
    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
    370, height);
            page.Canvas.DrawString("" + school.idDistrict[1],
 font2,
    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
    395, height);

            // shooll
            page.Canvas.DrawString("" + school.idShool[0],
font2,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
445, height);
            page.Canvas.DrawString("" + school.idShool[1],
 font2,
    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
    470, height);

            page.Canvas.DrawString("" + school.idShool[2],
font2,
  new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
  495, height);
            page.Canvas.DrawLine(pen, new PointF(365, height), new PointF(365, height + 20));

            page.Canvas.DrawLine(pen, new PointF(390, height), new PointF(390, height + 20));

            page.Canvas.DrawLine(pen, new PointF(415, height), new PointF(415, height + 20));

            page.Canvas.DrawLine(pen, new PointF(440, height), new PointF(440, height + 20));

            page.Canvas.DrawLine(pen, new PointF(465, height), new PointF(465, height + 20));

            page.Canvas.DrawLine(pen, new PointF(490, height), new PointF(490, height + 20));
            page.Canvas.DrawLine(pen, new PointF(515, height), new PointF(515, height + 20));

            height += 20;

            page.Canvas.DrawLine(pen, new PointF(365, height), new PointF(415, height));
            page.Canvas.DrawLine(pen, new PointF(440, height), new PointF(515, height));

            height += 10;

            page.Canvas.DrawString("11. Năm tốt nghiệp: ",
       font1,
          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
          0, height);

            page.Canvas.DrawString($"{profile.Year} ",
     font2,
        new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
        120, height);

            page.Canvas.DrawString("Khu vực: ",
font1,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
170, height);
            page.Canvas.DrawString($"{ profile.Areas}", font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
228, height);

            page.Canvas.DrawString("Đối tượng ưu tiên (nếu có): ",
font1,
new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
310, height);
           
            page.Canvas.DrawString(""+ (profile.Priority_object != null ? profile.Priority_object.Trim() : "Không Có"), font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
470, height);
            height += 22;
            page.Canvas.DrawString("THÔNG TIN ĐĂNG KÝ XÉT TUYỂN:",
       font1,
          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
          0, height);
            height += 22;
            font1 = new PdfTrueTypeFont(new Font("Times New Roman", 12f, FontStyle.Italic), true);
            string text = $"\t Số báo danh thi đánh giá năng lực:{profile.Identification} 	Điểm thi:\t {Math.Round(profile.Mark.Value, 2)} \t Tổ hợp môn:\t {profile.IdBlock}";
            PdfStringFormat format1 = new PdfStringFormat(PdfTextAlignment.Left);
            page.Canvas.DrawString(text, font1, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(0, height, 500, height), format1);
            height += 22;
            text = " Sau khi tìm hiểu các quy định về tiêu chí và điều kiện xét tuyển của Nhà trường, tôi đăng ký xét tuyển vào trình độ Đại học: ";
            format1 = new PdfStringFormat(PdfTextAlignment.Left);
            page.Canvas.DrawString(text, font1, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(0, height, 500, height), format1);
            height += 22;
            int stt = 0;
            foreach (var item in profile.InforMationProflies)
            {
                var major = _context.Majors.Find(item.idMajor);
                height += 22;
                if (height > 730)
                {
                    height = 0;
                    PdfPageBase page1 = doc.Pages.Add();
                    page = page1;
                }
                page.Canvas.DrawString((stt + 1) + ".\tNgành: ",
           font1,
              new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
              10, height);

                page.Canvas.DrawString($"{major.Name}",
         font2,
            new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
            80, height);

                page.Canvas.DrawString("Mã Ngành: ",
    font1,
    new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
    320, height);
                page.Canvas.DrawString($"{major.id}", font2, new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
    390, height);
            }
            font1 = new PdfTrueTypeFont(new Font("Times New Roman", 11f, FontStyle.Italic), true);
            height += 28;

            if (height > 730)
            {
                height = 0;
                PdfPageBase page1 = doc.Pages.Add();
                page = page1;
            }

            text = "\t Tôi xin cam đoan những lời khai trên là đúng sự thật. Nếu sai, tôi xin chịu xử lý theo quy định.";
            format1 = new PdfStringFormat(PdfTextAlignment.Left);

            page.Canvas.DrawString(text, font1, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(0, height, 500, height), format1);

            format1 = new PdfStringFormat(PdfTextAlignment.Right);
            height += 30;
            if (height > 730)
            {
                height = 0;
                PdfPageBase page1 = doc.Pages.Add();
                page = page1;
            }
            font1 = new PdfTrueTypeFont(new Font("Times New Roman", 12f), true);
            page.Canvas.DrawString("…………….., ngày " + profile.CreateDate.Value.Date.Day + " tháng " + profile.CreateDate.Value.Date.Month + " năm " + profile.CreateDate.Value.Date.Year, font1, new PdfSolidBrush(Color.FromArgb(25, 25, 112)), new RectangleF(0, height, 500, height), format1);

            height += 25;
            if (height > 680)
            {
                height = 0;
                PdfPageBase page1 = doc.Pages.Add();
                page = page1;
            }
            font1 = new PdfTrueTypeFont(new Font("Times New Roman", 12f, FontStyle.Bold), true);


            font2 = new PdfTrueTypeFont(new Font("Times New Roman", 9, FontStyle.Italic), true);

            page.Canvas.DrawString("NGƯỜI ĐĂNG KÝ HỌC",
                           font1,
                              new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                              350, height);
            height += 15;
            page.Canvas.DrawString("(Ký tên, ghi rõ họ tên)",
                       font2,
                          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                          375, height);
            height += 55;

            font1 = new PdfTrueTypeFont(new Font("Times New Roman", 12f), true);
            page.Canvas.DrawString($"{profile.Name.ToUpper()}",
                       font1,
                          new PdfSolidBrush(Color.FromArgb(25, 25, 112)),
                          360, height);

         string fileName = "Admisstion/" + profile.idAdmisstion + "/" + profile.CMND.Trim() + "/" + profile.CMND.Trim()+"_" +profile.Name.Trim() + "_" + profile.idAdmisstion + ".pdf";
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
            doc.SaveToFile(filePath);
            doc.Close();
            return "/" + fileName;
        }

        public async Task<ApiResult> ExportExcellOne(long? id)
        {
            try
            {
                string fileName = "Profile/DS_Tuyen_Sinh.xlsx";
                Admisstion admisstion = _context.Admisstions.Where(x => x.id == id).FirstOrDefault();
                var Profile = await _context.ProfileStudents.Where(x => x.idAdmisstion == id).ToListAsync();
                var ProfileInfor = await _context.InforMationProflies.ToListAsync();
                var School = await _context.Schools.ToListAsync();
                var Major = await _context.Majors.ToListAsync();
                var Block = await _context.Blocks.ToListAsync();
                int i = 0;
                if (admisstion.Type == 2)
                {
                    var result = (from p in Profile
                                  join I in ProfileInfor on p.id equals I.idProfile
                                  select new
                                  {
                                      STT = (i = i + 1),
                                      So_BD = "GSA" + I.id,
                                      HO_TEN = p.Name.ToLower(),
                                      NGAY_SINH = p.BirthDay.Value.ToShortDateString(),
                                      Gioi_Tinh=(p.Sex==1?"Name":"Nữ"),
                                      SO_CMND = p.CMND,
                                      NGAY_CAP=p.DateRange,
                                      DAN_TOC=p.Nation,
                                      SĐT =p.Teletephone,
                                      EMAIL=p.Email,
                                      DIA_CHI =p.Adress,
                                      NOI_Sinh=p.FromBirthDay,
                                      KHU_VUC = p.Areas,
                                      DOI_TUONG = (p.Priority_object != null ? p.Priority_object : ""), 
                                      THUTU_NV = I.STT,
                                      MA_NGANH = I.idMajor,
                                      TEN_NGANH = Major.FirstOrDefault(x => x.id.Trim().Equals(I.idMajor)).Name,
                                      MA_TOHOPMON = I.idBlock,
                                      TEN_TOHOPMON = Block.FirstOrDefault(x => x.id.Trim().Equals(I.idBlock.Trim())).Desscription,
                                      L10_Mon1 = I.subject1,
                                      L10_Mon2 = I.subject2,
                                      L10_Mon3 = I.subject3,
                                      L11_Mon1 = I.subject4,
                                      L11_Mon2 = I.subject5,
                                      L11_Mon3 = I.subject6,
                                      L12_Mon1 = I.subject7,
                                      L12_Mon2 = I.subject8,
                                      L12_Mon3 = I.subject9,
                                  }).ToList();
                    i = 0;
                    try
                    {
                        await DeleteFileAsync($"/" + fileName);
                    }
                    catch { }

                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);

                    DataTable table = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(result), (typeof(DataTable)));
               
                    FileInfo filePath1 = new FileInfo(filePath);

                    using (var excelPack = new ExcelPackage(filePath1))
                    {
                        var ws = excelPack.Workbook.Worksheets.Add("DanhSachThiSinh");      
                        ws.DefaultRowHeight = 12;
                        ws.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Row(1).Style.Font.Bold = true;
                        for (int j = 2; j < 15; j++)
                        {
                            ws.Column(j).Width = 16;
                        }

                        ws.Cells[1, 1].LoadFromDataTable(table, true, OfficeOpenXml.Table.TableStyles.None);

                        excelPack.Save();
                    }
                    return new ApiResult() { Success = true, Message = "Không tìm thấy", Data = "/" + fileName };
                }
                else
                {
                    var result = (from p in Profile
                                  join I in ProfileInfor on p.id equals I.idProfile

                                  select new
                                  {
                                      STT = (i = i + 1),
                                      So_BD = p.Identification,
                                      HO_TEN = p.Name.ToLower(),
                                      NGAY_SINH = p.BirthDay.Value.ToShortDateString(),
                                      Gioi_Tinh = (p.Sex == 1 ? "Name" : "Nữ"),
                                      SO_CMND = p.CMND,
                                      NGAY_CAP = p.DateRange,
                                      DAN_TOC = p.Nation,
                                      SĐT = p.Teletephone,
                                      EMAIL = p.Email,
                                      DIA_CHI = p.Adress,
                                      NOI_Sinh = p.FromBirthDay,
                                      KHU_VUC = p.Areas,
                                      DOI_TUONG = (p.Priority_object != null ? p.Priority_object : ""),
                                      THUTU_NV = I.STT,
                                      MA_NGANH = I.idMajor,
                                      TEN_NGANH = Major.FirstOrDefault(x => x.id.Trim().Equals(I.idMajor)).Name,
                                      MA_TOHOPMON = I.idBlock,
                                      TEN_TOHOPMON = Block.FirstOrDefault(x => x.id.Trim().Equals(I.idBlock.Trim())).Desscription,
                                     TONG_DIEM=p.Mark
                                  }).ToList();
                    try
                    {
                        await DeleteFileAsync($"/" + fileName);
                    }
                    catch { }
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
                    DataTable table = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(result), (typeof(DataTable)));
                    FileInfo filePath1 = new FileInfo(filePath);
                    using (var excelPack = new ExcelPackage(filePath1))
                    {
                        var ws = excelPack.Workbook.Worksheets.Add("DanhSachNguyenVong");
                        ws.DefaultRowHeight = 12;
                        ws.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Row(1).Style.Font.Bold = true;
                        for (int j = 1; j < 10; j++)
                        {
                            ws.Column(j).Width = 18;
                        }
                        ws.Cells.LoadFromDataTable(table, true, OfficeOpenXml.Table.TableStyles.None);

                        excelPack.Save();
                    }
                    return new ApiResult() { Success = true, Message = "Không tìm thấy", Data = "/" + fileName };
                }


            }
            catch (Exception e)
            {
                return new ApiResult() { Success = false, Message = "Lỗi server " + e.Message, Data = null };
            }
        }
    }
}
