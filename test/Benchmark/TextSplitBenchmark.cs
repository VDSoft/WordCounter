using FluentAssertions;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Xunit;

namespace Benchmark
{
    public class TextSplitBenchmark
    {
        private readonly string input = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut dignissim enim commodo convallis mollis. Nullam tempor ultrices metus quis fringilla. Nunc tellus mauris, porta ac pellentesque ut, ornare in nulla. Donec sagittis euismod tincidunt. Praesent pharetra lacus felis. Aenean eleifend eget massa nec consectetur. Pellentesque habitant morbi tristique senectus et.";

        [Fact]
        public void StringSplit_SmallText_PerfomanceMeasuringInTime()
        {
            // Arrange
            var watch = new Stopwatch();

            // Act
            watch.Start();
            var result = input.Split(' ');
            watch.Stop();

            // Assert
            result.Should().NotBeNullOrEmpty();
            Trace.WriteLine($"Elapsed time {watch.Elapsed}.");
        }

        [Fact]
        public void RegexSplit_SmalText_PerformanceMeasuringInTime()
        {
            // Arrange
            var watch = new Stopwatch();

            // Act
            watch.Start();
            var result = Regex.Split(input, @"\s");
            watch.Stop();

            // Assert
            result.Should().NotBeNullOrEmpty();
            Trace.WriteLine($"Elapsed time {watch.Elapsed}.");
        }
    }
}