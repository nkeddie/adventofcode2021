using Advent2021.ConsoleApp;
using BenchmarkDotNet.Running;

Console.WriteLine(await new Day2A().RunAsync_Simple());
Console.WriteLine(await new Day2B().RunAsync_Simple());
//BenchmarkRunner.Run<Day2A>();