using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storage
{
    public interface IStorage
    {
        Task<string> UploadFile(IFile file);
        Task<IEnumerable<string>> UploadFiles(IEnumerable<IFile> files);

        Task<string> UploadImage(IFile file);
        Task<IEnumerable<string>> UploadImages(IEnumerable<IFile> files);
    }
}
