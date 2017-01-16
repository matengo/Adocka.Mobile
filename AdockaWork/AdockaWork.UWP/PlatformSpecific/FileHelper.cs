using AdockaWork.Interfaces;
using AdockaWork.UWP.PlatformSpecific;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace AdockaWork.UWP.PlatformSpecific
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}
