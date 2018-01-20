using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Storage.Azure
{
    public class File : IFile
    {
        public string FileName { get; set; }
        public Stream Stream { get; set; }
    }
}
