using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Storage
{
    public interface IStorage
    {
        Task<string> UploadFile(IFile file, CancellationToken cancellationToken);
        Task<IEnumerable<string>> UploadFiles(IEnumerable<IFile> files, CancellationToken cancellationToken);

        Task<string> UploadImage(IFile file, CancellationToken cancellationToken);
        Task<IEnumerable<string>> UploadImages(IEnumerable<IFile> files, CancellationToken cancellationToken);
    }
}
