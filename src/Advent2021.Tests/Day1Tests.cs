using Advent2021.ConsoleApp;
using System.Threading.Tasks;
using Xunit;

namespace Advent2021.Tests
{
    public class Day1Tests
    {
        [Fact]
        public async Task RunAsync_Streaming_LowAlloc_ReturnsCorrectValue()
        {
            var result = await new Day1A().RunAsync_Streaming_LowAlloc();
            Assert.Equal(1655, result);
        }

        [Fact]
        public async Task RunAsync_Simple_ReturnsCorrectValue()
        {
            var result = await new Day1A().RunAsync_Simple();
            Assert.Equal(1655, result);
        }

        [Fact]
        public async Task RunAsync_Streaming_ReturnsCorrectValue()
        {
            var result = await new Day1A().RunAsync_Streaming();
            Assert.Equal(1655, result);
        }
    }
}
