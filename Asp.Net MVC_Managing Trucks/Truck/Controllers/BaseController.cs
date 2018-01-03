using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using Truck.Core;
using Truck.Data;
using Truck.Data.Repositories;
using Truck.Infrastructure;
using Truck.Infrastructure.Extensions;
using Truck.Infrastructure.Services;
using Truck.Models;
using Truck.Services;
using Truck.ViewModels;

namespace Truck.Controllers
{
    public class BaseController : Controller
    {
        public ApplicationUserManager UserManager => new ApplicationUserManager(new UserStore<ApplicationUser>(IdentityDb));
        public RoleManager<IdentityRole> RoleManager => new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(IdentityDb));
        [Dependency]
        public IMapper Mapper
        {
            get; set;
        }
        [Dependency]
        public ApplicationDbContext IdentityDb
        {
            get; set;
        }

        private TruckDbContext _dbContext;

        public TruckDbContext DbContext
        {
            get
            {
                if (_dbContext == null)
                    _dbContext = new TruckDbContext();
                return _dbContext;
            }
        }
        [Dependency]
        public IDbService<Attachment> AttachmentService { get; set; }

        [Dependency]
        public IUploadFileService UploadFileService { get; set; }
        public string UserId => User.Identity.GetUserId();
        public int CustomerId => User.IsInRole("admin")? 0 : IdentityDb.Users.Single(item => item.Id == UserId).CustomerId.Value;

       [HttpGet]
        public FileResult DownloadPdf(int id)
        {
            var file = AttachmentService.Find(id);
            //var vPath =GoogleDriveService.GetAsPdf(file.GoogleDriveId);
            return File(Server.MapPath(file.Path), "application/pdf", file.Name.Split('.')[0] + ".pdf");
        }
        
        //generate random password
        public string GeneratePassword(int totalLength)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            const string validSmallLetters = "abcdefghijklmnopqrstuvwxyz";
            const string validNumbers = "1234567890";
            const string validBigLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string validNonAlpha = "#*&^%$#@";
            var random = new Random();

            string password = string.Empty;
            var length = totalLength - 4;

            password += validBigLetters[random.Next(validBigLetters.Length)];
            password += validSmallLetters[random.Next(validSmallLetters.Length)];
            for (int i = 0; i < length; ++i)
                password += valid[random.Next(valid.Length)];
            password += validNumbers[random.Next(validNumbers.Length)];
            password += validNonAlpha[random.Next(validNonAlpha.Length)];

            return password;
        }

       
        //upload query of Excel template file
        protected string UploadFile(HttpPostedFileBase httpPostedFileBase,string fileName)
        {
            var dirPath = "~/Files/" + CustomerId + "/" + UserId;
            //create the directory if not exists
            var physPath = Server.MapPath(dirPath);
            if (!Directory.Exists(physPath))
                Directory.CreateDirectory(physPath);
            var filePath = dirPath + "/" + DateTime.Now.ToString("ddMMyyyyhhmmsstt") + fileName;
            httpPostedFileBase.SaveAs(Server.MapPath(filePath));
            return filePath;
        }
        //override JSon to return camel Case json
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonDotNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }
}