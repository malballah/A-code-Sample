using System.Web;

namespace Truck.Infrastructure.Services
{
    public interface IUploadFileService
    {
        string UploadFile(HttpPostedFileBase httpPostedFileBase);
        byte[] Download(string fileId);
    }
}
