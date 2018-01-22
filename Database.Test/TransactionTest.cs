using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Database.Test
{
    public class TransactionTest
    {

        [Fact]
        public async Task ShouldThrowIfConnectionNotProvided()
        {
            var logger = new NullLogger<Transaction>();
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                var trans = new Transaction(string.Empty, logger);
            });
        }

        [Fact]
        public async Task ShouldThrowIfBeginNotCalled()
        {
            var logger = new NullLogger<Transaction>();

            var trans = new Transaction("connection", logger);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await trans.ExecuteAsync("");
            });
        }
    }
}
