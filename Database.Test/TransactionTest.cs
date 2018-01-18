using System;
using Xunit;

namespace Database.Test
{
    public class TransactionTest
    {

        [Fact]
        public void ShouldThrowIfConnectionNotProvided()
        {
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                var trans = new Transaction(string.Empty, null);
            });
        }

        [Fact]
        public void ShouldThrowIfBeginNotCalled()
        {
            var trans = new Transaction("connection", null);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await trans.Execute("", null);
            });
        }
    }
}
