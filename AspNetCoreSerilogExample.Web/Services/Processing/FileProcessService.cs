using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreSerilogExample.Web.Services.Processing
{
    public class FileProcessService : IFileProcessService
    {
        public bool EnsureFileExists(string filepath)
        {
            if (File.Exists(filepath))
            {
                return true;
            }
            try
            {
                File.Create(filepath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"unable to create file with filepath {filepath}");
                
            }

            return false;
        }
    }
}
