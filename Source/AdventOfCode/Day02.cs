using System;
using System.Buffers;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    internal class Day02
    {
        private readonly string _puzzleFilename;

        int rLim = 12; int gLim = 13; int bLim = 14;

        public Day02(String puzzleFilename)
        {
            _puzzleFilename = puzzleFilename;
        }

        public void SolvePossibleGames()
        {
            var sum = 0;
            using (var fileInput = new StreamReader(_puzzleFilename))
            {
                while (!fileInput.EndOfStream)
                {
                    var line = fileInput.ReadLine();
                    if (line == null) break;
                    line = line.Trim();
                    if (line != "")
                    {
                        sum += GamePossible(line);
                    }
                }
            }
            Console.WriteLine($"Possible game IDs sum={sum}");
        }



        public void SolveCubePowers()
        {
            var sum = 0;
            using (var fileInput = new StreamReader(_puzzleFilename))
            {
                while (!fileInput.EndOfStream)
                {
                    var line = fileInput.ReadLine();
                    if (line == null) break;
                    line = line.Trim();
                    if (line != "")
                    {
                        sum += GamePower(line);
                    }
                }
            }
            Console.WriteLine($"Total game Powers sum={sum}");
        }


        private int GamePower(string line)
        {
            line = line.Trim();

            //Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
            var gamePattern = @"Game\s?(\d*)\:(.*)";
            var gameIdStr = Regex.Match(line, gamePattern).Groups[1].Value;
            var gameID = Convert.ToInt32(gameIdStr);
            var draws = Regex.Match(line, gamePattern).Groups[2].Value.Split(';');

            int rMin = 0; int gMin = 0; int bMin = 0;
            for (int i = 0; i < draws.Length; i++)
            {
                var colors = draws[i].Split(',');
                for (int j = 0; j < colors.Length; j++)
                {
                    var color = colors[j].Trim().Split(' ');
                    var count = Convert.ToInt32(color[0]);
                    switch (color[1])
                    {
                        case "red":
                            if (count > rMin) rMin = count;
                            break;
                        case "green":
                            if (count > gMin) gMin = count;
                            break;
                        case "blue":
                            if (count > bMin) bMin = count;
                            break;
                    }
                }

            }

            return rMin * gMin * bMin;

        }


        private int GamePossible(string line)
        {
            line = line.Trim();

            //Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
            var gamePattern = @"Game\s?(\d*)\:(.*)";
            var gameIdStr = Regex.Match(line, gamePattern).Groups[1].Value;
            var gameID = Convert.ToInt32(gameIdStr);
            var draws = Regex.Match(line, gamePattern).Groups[2].Value.Split(';');

            for (int i=0; i < draws.Length; i++)
            {
                var colors = draws[i].Split(',');
                for (int  j=0; j < colors.Length; j++)
                {
                    var color = colors[j].Trim().Split(' ');
                    var count = Convert.ToInt32(color[0]);
                    int max = 0;
                    switch (color[1])
                    {
                        case "red":
                            max = rLim;
                            break;
                        case "green":
                            max = gLim;
                            break;
                        case "blue":
                            max = bLim;
                            break;
                    }
                    if (count > max)
                    {
                        gameID = 0;
                        break;
                    }
                }

            }

            return gameID;


        }



        public static void Run(String puzzleFilename)
        {
            var day = new Day02(puzzleFilename);
            day.SolvePossibleGames();
            day.SolveCubePowers();

        }
    }
}
