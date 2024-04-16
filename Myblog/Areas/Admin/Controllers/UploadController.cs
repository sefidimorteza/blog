using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using weblog.CoreLayer.Services.IFileManager;
using weblog.CoreLayer.Utilities;

namespace Myblog.Areas.Admin.Controllers
{
    public class UploadController : Controller
    {
        private readonly IFileManager _fileManager;

        public UploadController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [Route("/Upload/Article")]
        [Authorize]
        public IActionResult Article(IFormFile upload)
        {
            if (upload == null)
            {
                BadRequest();
            }

            var uploadImage = _fileManager.saveFile(upload, Directories.PostContentImage);
            return new JsonResult(new { Uploaded = true, url = Directories.GetPostContentImage(uploadImage) });
        }
    }
}
