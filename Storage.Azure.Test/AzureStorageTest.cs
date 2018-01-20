using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Storage.Azure.Test
{
    public class AzureStorageTest
    {
        [Fact]
        public void ShouldThrowIfNoConnectionString()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var storage = new AzureStorage("");
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                var storage = new AzureStorage(" ");
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                var storage = new AzureStorage(null);
            });
        }

        [Fact]
        public async Task ShouldThrowIfFileIsNull()
        {
            var storage = new AzureStorage("connection");
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await storage.UploadFile(null, CancellationToken.None);
            });

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await storage.UploadImage(null, CancellationToken.None);
            });
        }

        [Fact]
        public async Task ShouldThrowIfFilesIsNull()
        {
            var storage = new AzureStorage("connection");

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await storage.UploadFiles(null, CancellationToken.None);
            });

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await storage.UploadImages(null, CancellationToken.None);
            });
        }
    }
}
