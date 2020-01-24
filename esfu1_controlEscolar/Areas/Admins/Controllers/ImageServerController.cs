using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace esfu1_controlEscolar.Areas.Admins.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ImageServerController : Controller
    {
        public string AddImage(HttpPostedFileBase archivo, string ruta_para_guardar, HttpServerUtilityBase Server)
        {
            string pathReturn = "none";
            Random rnd = new Random();

            if (archivo.ContentLength > 0)
            {
                string path = archivo.FileName;
                int number = rnd.Next(10000);
                string filename = Path.GetFileNameWithoutExtension(path);
                string extension = Path.GetExtension(path);
                filename = filename + DateTime.Now.ToString("yymmssff") + number + extension;
                pathReturn = ruta_para_guardar + filename;
                filename = Path.Combine(Server.MapPath(ruta_para_guardar), filename);
                archivo.SaveAs(filename);
            }
            return pathReturn;
        }

        public void RemoveImage(string ruta_para_quitar, HttpServerUtilityBase Server)
        {
            string fullPath = Path.Combine(Server.MapPath(ruta_para_quitar));
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }

        public void RemoveAll(string ruta_para_quitar, HttpServerUtilityBase Server)
        {
            if (Directory.Exists(Server.MapPath(ruta_para_quitar)))
            {
                var folderPath = Server.MapPath(ruta_para_quitar);
                System.IO.DirectoryInfo folderInfo = new DirectoryInfo(folderPath);
                foreach (FileInfo file in folderInfo.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in folderInfo.GetDirectories())
                {
                    dir.Delete(true);
                }
            }

        }

        public bool ImgValid(HttpPostedFileBase file)
        {
            if (file.Equals(null))
            {
                return false;
            }

            if (file.ContentType.Contains("image"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}