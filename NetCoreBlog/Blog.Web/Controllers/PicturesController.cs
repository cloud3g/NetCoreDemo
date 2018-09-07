using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using System.IO;
using Blog.Common;
using Microsoft.AspNetCore.Routing;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Controllers
{
    [Route("api/[controller]")]
    public class PicturesController : Controller
    {
        private IHostingEnvironment hostingEnv;
        string[] pictureFormatArray = { "png", "jpg", "jpeg", "bmp", "gif", "ico", "PNG", "JPG", "JPEG", "BMP", "GIF", "ICO" };
        public PicturesController(IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="filename">图片名称</param>
        /// <param name="directoryName">保存文件夹</param>
        /// <param name="isThumbnail">按指定长宽保存</param>
        /// <param name="Percent">按比例缩小</param>
        /// <param name="width">长</param>
        /// <param name="height">宽</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(string filename, string directoryName, int isThumbnail, double Percent, int width = 140, int height = 140)
        {


            var files = Request.Form.Files;
            long size = files.Sum(f => f.Length);

            //size > 100MB refuse upload !
            if (size > 10485760)
            {
                return Json(new Response() { Code = ResponseCode.Success, Message = "上传文件不能超过10MB" });
            }
            var file = files[0];
            var fileName = System.Net.Http.Headers.ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

            string filePath = hostingEnv.WebRootPath + $@"/upload/Pictures/{directoryName}/";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string suffix = fileName.Split('.')[1];

            if (!pictureFormatArray.Contains(suffix))
            {
                return Json(new Response() { Code = ResponseCode.Success, Message = "只能上传图片格式文件" });
            }
            if (string.IsNullOrEmpty(filename))
            {
                fileName = Guid.NewGuid() + "." + suffix;
            }
            else
            {
                fileName = filename + "." + suffix;
            }

            string fileFullName = filePath + fileName;
            if (isThumbnail != 0)
            {

                Utils.SaveImageByWidthHeight(width, height, file.OpenReadStream(), fileFullName);
            }
            else if (Percent != 0)
            {
                if(size< 314572)
                {
                    using (FileStream fs = System.IO.File.Create(fileFullName))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                }
                else
                {
                    Utils.PercentImage(file.OpenReadStream(), Percent, fileFullName);
                }
                
            }
            else
            {
                using (FileStream fs = System.IO.File.Create(fileFullName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            //return Json(new Response() { Code = ResponseCode.Success, Message = "上传成功", Value = $"/upload/Pictures/{controller}/{fileName}" });
            return Json(new { code = 0, data = new { src = $"/upload/Pictures/{directoryName}/{fileName}", title = fileName } });
        }

    }
}
