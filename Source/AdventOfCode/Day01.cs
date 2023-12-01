using System;
using System.Buffers;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Day01
    {
        private readonly string _puzzleFilename;

        const string Digits = "0123456789";
        static readonly SearchValues<char> SearchDigits = SearchValues.Create(Digits);
        static readonly string[] NumericWords = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };


        public Day01(String puzzleFilename) 
        {
            _puzzleFilename = puzzleFilename;
        }

        public void SolveSumCalibrationValues()
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
                        sum += CalValue(line);
                    }
                }
            }
            Console.WriteLine($"Calibration value sum={sum}");
        }

        private int CalValue(string line)
        {
            // Assumes that each line contains at least 1 digit
            var firstDigit = line[line.AsSpan().IndexOfAny(SearchDigits)] - '0';
            var lastDigit = line[line.AsSpan().LastIndexOfAny(SearchDigits)] -'0';

            return (firstDigit * 10 + lastDigit);
        }


        public void SolveSumWordCalibrationValues()
        {
            var sum = 0;
            var debugFilename = Path.Combine(Path.GetDirectoryName(_puzzleFilename), "Day1DebugOut.txt");
            using (var debugFile = new StreamWriter(debugFilename))
            using (var fileInput = new StreamReader(_puzzleFilename))
            {
                while (!fileInput.EndOfStream)
                {
                    var line = fileInput.ReadLine();
                    if (line == null) break;
                    line = line.Trim();
                    if (line != "")
                    {
                        var calWord = CalWordValue(line);
                        debugFile.WriteLine($"{line} -- {calWord}");
                        sum += calWord;
                    }
                }
            }
            Console.WriteLine($"Calibration word value sum={sum}");
        }


        private int CalWordValue(string line)
        {
            char? firstDigit = null;
            char? lastDigit = null;
            string lastWord = "";
            for (int i = 0; i < line.Length; i++)
            {
                var thisChar = line[i];
                if (char.IsDigit(thisChar))
                {
                    lastDigit = thisChar;
                    if (firstDigit == null)
                    {
                        if (lastWord != "")
                        {
                            firstDigit = FirstDigitWord(lastWord);
                        }

                        if (firstDigit == null)
                        {
                            firstDigit = thisChar;
                        }
                    }
                    lastWord = "";
                } else
                {
                    lastWord += thisChar;
                }
            }

            var strLine = "";
            if (lastWord != "")
            {

                if (firstDigit == null)
                {
                    // no digits on this line
                    firstDigit = FirstDigitWord(lastWord);
                }

                var foundDigit = LastDigitWord(lastWord);
                if (foundDigit != null)
                {
                    lastDigit = foundDigit;
                }
            }
            strLine = strLine + firstDigit;
            strLine = strLine + lastDigit;
            return Convert.ToInt32(strLine);
        }

        private char? LastDigitWord(string lastWord)
        {
            char? foundDigit = null;
            int lastFoundIndex = -1;

            for (int i=1; i < NumericWords.Length; i++)
            {
                var word = NumericWords[i];
                var foundIndex = lastWord.LastIndexOf(word);
                if (foundIndex > lastFoundIndex)
                {
                    lastFoundIndex = foundIndex;
                    foundDigit = (char)('0' + i);
                }

            }
            return foundDigit;
        }

        private char? FirstDigitWord(string lastWord)
        {
            char? foundDigit = null;
            int lastFoundIndex = 9999;

            for (int i = 1; i < NumericWords.Length; i++)
            {
                var word = NumericWords[i];
                var foundIndex = lastWord.IndexOf(word);
                if (foundIndex >= 0)
                {
                    if (foundIndex < lastFoundIndex)
                    {
                        lastFoundIndex = foundIndex;
                        foundDigit = (char)('0' + i);
                    }
                }
            }
            return foundDigit;
        }


        public static void Run(String puzzleFilename)
        {
            var day = new Day01(puzzleFilename);
            day.SolveSumCalibrationValues();
            day.SolveSumWordCalibrationValues();

        }
    }
}
