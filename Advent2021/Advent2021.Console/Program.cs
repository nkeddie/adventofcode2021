using Advent2021.ConsoleApp;

Console.WriteLine(await Performance.TimeAsync(async () => await Day1A.RunAsync(), 1000));
Console.WriteLine(await Performance.TimeAsync(async () => await Day1B.RunAsync(), 1000));
