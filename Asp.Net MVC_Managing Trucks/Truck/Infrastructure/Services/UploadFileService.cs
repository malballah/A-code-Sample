using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Rectangle = iTextSharp.text.Rectangle;

namespace Truck.Infrastructure.Services
{
    public class UploadFileService : IUploadFileService
    {
        
        public string UploadFile(HttpPostedFileBase httpPostedFileBase)
        {
            //return request.ResponseBody.Id;
            var tempFileName = Guid.NewGuid() + DateTime.Now.ToString("ddMMyyyyhhmmss");
            var ext = httpPostedFileBase.FileName.Substring(httpPostedFileBase.FileName.LastIndexOf(".", StringComparison.Ordinal)).ToLower();
            var orgFilePath = HttpContext.Current.Server.MapPath("~/OrgFiles/" + tempFileName + ext);
            var pdfFilePath = HttpContext.Current.Server.MapPath("~/PdfFiles/" + tempFileName + ".pdf");
            //save file locally
            httpPostedFileBase.SaveAs(orgFilePath);
            var contentType = MimeMapping.GetMimeMapping(httpPostedFileBase.FileName);
            string api="https://do.convertapi.com/Text2Pdf";
            if (contentType.Contains("image"))
            {
                //api = "https://do.convertapi.com/Image2Pdf";
                Rectangle pageSize = null;
                using (var srcImage = new Bitmap(orgFilePath))
                {
                    pageSize = new Rectangle(0, 0, srcImage.Width, srcImage.Height);
                }
                using (var ms = new MemoryStream())
                {
                    var document = new Document(pageSize, 0, 0, 0, 0);
                    PdfWriter.GetInstance(document, ms).SetFullCompression();
                    document.Open();
                    var image = iTextSharp.text.Image.GetInstance(orgFilePath);
                    image.SetAbsolutePosition(0, 0);
                    image.ScaleAbsoluteHeight(document.PageSize.Height);
                    image.ScaleAbsoluteWidth(document.PageSize.Width);
                    document.Add(image);
                    document.Close();
                    File.WriteAllBytes(pdfFilePath, ms.ToArray());
                }
                
                return tempFileName;
            }
                
            else if (ext==".xls" || ext==".xlsx")
                api = "https://do.convertapi.com/Excel2Pdf";
            else if(ext==".doc" || ext==".docx")
                api = "https://do.convertapi.com/Word2Pdf";
            else if (ext == ".pdf")
            {
                httpPostedFileBase.SaveAs(pdfFilePath);//just save again in PdfFolder
                return tempFileName;
            }
                
            using (var client = new WebClient())
            {
                var data = new NameValueCollection
                {
                    {"OutputFormat", "pdf"},
                    {"OutputFileName", pdfFilePath},
                    {"ApiKey", ConfigurationManager.AppSettings["ApiKey"]}
                };
                
                try
                {
                    client.QueryString.Add(data);
                    var response = client.UploadFile(api, orgFilePath);
                    System.IO.File.WriteAllBytes(pdfFilePath, response);
                }
                catch (WebException e)
                {
                   
                }
                return tempFileName;

            }
        }
        
        private string FileStamp()
        {
            return DateTime.Now.ToString("ddmmyyhhmmss_");
        }
        
        public byte[] Download(string fileId)
        {
            throw new NotImplementedException();
        }
        
    }
    
}