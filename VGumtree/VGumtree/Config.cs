using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace VGumtree
{
    public class Config : IConfig
    {
        public static string AdminRole = "admin";
        public static string UserRole = "user";

        public string GetImageFolderName()
        {            
            return GetAppSetting("ImageFolder");
        }

        public string GetImageUploadFolder()
        {
            string folderPath = HttpContext.Current.Server.MapPath(GetAppSetting("ImageFolder"));
            Directory.CreateDirectory(folderPath);
            return folderPath;
        }

        public string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key] != null ? ConfigurationManager.AppSettings[key].ToString() : null;
        }
        
        public string GetTempUploadFolder()
        {
            string folder = HttpContext.Current.Server.MapPath(GetAppSetting("TempUploadFolder"));
            Directory.CreateDirectory(folder);
            return folder;
        }      
    }
}