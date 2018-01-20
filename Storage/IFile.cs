using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Storage
{
    public interface IFile
    {
        string FileName { get; set; }
        Stream Stream { get; set; }
    }
}
