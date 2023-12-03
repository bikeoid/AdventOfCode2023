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
    internal class Day03
    {
        private readonly string _puzzleFilename;

        List<string> lines = new List<string>();

        public Day03(String puzzleFilename)
        {
            _puzzleFilename = puzzleFilename;
        }

        public void ReadLines()
        {
            using (var fileInput = new StreamReader(_puzzleFilename))
            {
                while (!fileInput.EndOfStream)
                {
                    var line = fileInput.ReadLine();
                    if (line == null) break;
                    line = line.Trim();
                    if (line != "")
                    {
                        lines.Add(line);
                    }
                }
            }
        }

        string starLoc = "";
        Dictionary<string, List<int>> possiblGears = new ();

        private bool IsSymbol(int row, int col)
        {
            if (col < 0 || row <= 0 || row >= lines.Count || col >= lines[row].Length) return false;
            var c = lines[row][col];
            if (c == '*')
            {
                starLoc = row.ToString() + "," + col.ToString();
                // ?? What if multiple stars?
            }
            return c != '.' && !char.IsDigit(c);
        }

        public void SolvePartNumbers()
        {
            var sum = 0;
            for (int row = 0; row < lines.Count; row++)
            {
                int col = 0;
                while (col < lines[row].Length)
                {
                    if (char.IsDigit(lines[row][col]))
                    {
                        string strNum = "";
                        bool hasSymbol = false;
                        starLoc = "";
                        while (col < lines[row].Length && char.IsDigit(lines[row][col]))
                        {
                            strNum += lines[row][col];
                            if (IsSymbol(row - 1, col - 1) || IsSymbol(row, col - 1) || IsSymbol(row + 1, col - 1)) hasSymbol = true;
                            if (IsSymbol(row - 1, col) || IsSymbol(row + 1, col)) hasSymbol = true;
                            if (IsSymbol(row - 1, col + 1) || IsSymbol(row, col + 1) || IsSymbol(row + 1, col + 1)) hasSymbol = true;
                            col++;
                        };
                        if (hasSymbol)
                        {
                            sum += Convert.ToInt32(strNum);
                        }

                    }
                    col++;
                }

            }

            Console.WriteLine($"Total part number sum={sum}");

        }


        public void SolveGearRatios()
        {
            var sum = 0;
            for (int row = 0; row < lines.Count; row++)
            {
                int col = 0;
                while (col < lines[row].Length)
                {
                    if (char.IsDigit(lines[row][col]))
                    {
                        string strNum = "";
                        bool hasSymbol = false;
                        starLoc = "";
                        while (col < lines[row].Length && char.IsDigit(lines[row][col]))
                        {
                            strNum += lines[row][col];
                            if (IsSymbol(row - 1, col - 1) || IsSymbol(row, col - 1) || IsSymbol(row + 1, col - 1)) hasSymbol = true;
                            if (IsSymbol(row - 1, col) || IsSymbol(row + 1, col)) hasSymbol = true;
                            if (IsSymbol(row - 1, col + 1) || IsSymbol(row, col + 1) || IsSymbol(row + 1, col + 1)) hasSymbol = true;
                            col++;
                        };
                        if (hasSymbol && !string.IsNullOrEmpty(starLoc))
                        {

                            if (!possiblGears.ContainsKey(starLoc))
                            {
                                var gearList = new List<int>();
                                possiblGears.Add(starLoc, gearList);
                            }
                            var gear = Convert.ToInt32(strNum);
                            possiblGears[starLoc].Add(gear);
                        }
                    }
                    col++;
                }

            }

            // Find gears and total
            foreach (var coord in possiblGears.Keys)
            {
                var possibleGear = possiblGears[coord];

                if (possibleGear.Count == 2)
                {
                    sum += possibleGear[0] * possibleGear[1];
                }
            }


            Console.WriteLine($"Total gear ratios sum={sum}");
        }







        public static void Run(String puzzleFilename)
        {
            var day = new Day03(puzzleFilename);
            day.ReadLines();
            day.SolvePartNumbers();
            day.SolveGearRatios();

        }
    }
}
