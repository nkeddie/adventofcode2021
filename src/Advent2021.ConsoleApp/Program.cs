using Advent2021.ConsoleApp;
using BenchmarkDotNet.Running;

Console.WriteLine(await new Day4A().RunAsync_Simple());
Console.WriteLine(await new Day4B().RunAsync_Simple());
//BenchmarkRunner.Run<Day2A>();