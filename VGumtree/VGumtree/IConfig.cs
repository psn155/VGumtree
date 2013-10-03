using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGumtree
{
    public interface IConfig
    { 
        string GetAppSetting(string key);
        string GetImageFolderName();
        string GetImageUploadFolder();
        string GetTempUploadFolder();
    }
}
