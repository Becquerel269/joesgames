using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreSerilogExample.Web.Services.Processing
{
    public interface IFileProcessService
    {
        bool EnsureFileExists(string filepath);
    }
}
