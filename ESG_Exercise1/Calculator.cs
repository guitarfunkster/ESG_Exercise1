using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace ESG_Exercise1
{
    public static class Calculator
    {

        public static int Add(string numbers)
        {
            const string ReplacementDelimiter = "@";

            if (numbers.Length == 0) return 0;

            //Extract the delimiter(s) portion
            var delimiter = ExtractDelimiter(numbers);

            //Create list of delimiters
            var delimiters = ExtractDelimiters(delimiter); 

            //Handle delimiter ID line (i.e. remove)
            numbers = ProcessDelimiterLine(numbers);

            //Check if delimiter list has any values
            if (delimiters.Count > 0)
            {
                delimiter = ReplacementDelimiter;

                //Replace all delimiters
                numbers = EncodeMultipleDelimiters(numbers, delimiters, delimiter);
            }

            //Validation
            if (!ValidateInput(numbers, delimiter))
            {
                return -1;
            }

            //Create list of values from inputs
            var numberValues = ProcessInputForDelimiter(numbers, delimiter);

            var processed = ProcessNewLines(numberValues);
        
            //Convert to list of integers
            var intNums = Convert(processed);

            //Calculate
            return Calculate(intNums);
        }

        private static List<string> ProcessInputForDelimiter(string numbers, string delimiter)
        {

             return numbers.Split(delimiter).ToList();
        }

        private static string ProcessDelimiterLine(string input)
        {
            const string DelimIdentifier = "//";
            const string NewLine = "\n";

            if (input.StartsWith(DelimIdentifier))
            {
                var newLinePos = input.IndexOf(NewLine);
                return input.Substring(newLinePos + NewLine.Length).Trim();
            }
            else
            {
                return input;
            }
        }

        public static List<string> ExtractDelimiters(string delimiters)
        {
            
            const char DelimIdStart= '[';
            const char DelimIdEnd = ']';

            var listDelims = new List<string>();
            var delims = delimiters.ToCharArray();

            for (int i = 0; i < delims.Length; i++)
            {
                //Check for the start character
                if (delims[i] == DelimIdStart)
                {
                    //Find the next end from start
                    var endDelimPos = delimiters.IndexOf(DelimIdEnd, i);

                    if (endDelimPos != -1)
                    {
                        var startDelim = i + 1;
                        var lengthDelim = endDelimPos - startDelim;

                        //Extract the current delimiter
                        var delim = delimiters.Substring(startDelim, lengthDelim);

                        listDelims.Add(delim);
                    }
                }
            }

            return listDelims;
        }

        public static string ExtractDelimiter(string numbers)
        {
            var delim = ",";
            const string DelimIdentifier= "//";

            var delimStart = numbers.IndexOf(DelimIdentifier);

            if (delimStart == 0)
            {
                var newLinePos = numbers.IndexOf("\n");
                var endDelimiterPos = delimStart + DelimIdentifier.Length;

                delim = numbers.Substring(endDelimiterPos, newLinePos - (endDelimiterPos));
            }

            return delim;
        }

        private static List<string> ProcessNewLines(List<string>inputs)
        {
            var ret = new List<string>();
            foreach (var item in inputs)
            {
                if (item.IndexOf('\n') != 0)
                {
                    var nums= item.Split('\n');

                    foreach (var num in nums)
                    {
                        ret.Add(num);
                    }      
                }
                else
                {
                    ret.Add(item);
                }
            }

            return ret;

        }

        private static List<int> Convert(List<string>values)
        {
   
            var ret = new List<int>();

            foreach (var item in values)
            {
                if (int.TryParse(item, out int candidate) && (candidate <= 1000))
                {
                   ret.Add(candidate); 
                }
            }

            return ret;
        }

        private static int Calculate(List<int> numbers)
        {
   
            int ret = 0;
            foreach (var input in numbers) 
            {
                ret += input;
            }

            return ret;
        }

        private static string EncodeMultipleDelimiters(string input, List<string> delimiters, string replacementDelimiter)
        {

            foreach (var delim in delimiters)
            {
                input = input.Replace(delim, replacementDelimiter);
            }

            return input;
        }

        private static bool ValidateInput(string input, string delim)
        {
            if (input == null) return false;

            var inputs = input.Split(delim);
            var listNegatives = new List<int>();

            //Checked for numbers
            if (inputs.Length > 0)
            {

                //Check if integers
                foreach (var item in inputs)
                {

                    if (item.IndexOf('\n') != 0)
                    {
                        var nums = item.Split('\n');

                        foreach (var num in nums)
                        {
                            int candidate;
                            if (!int.TryParse(num, out candidate))
                            {
                                return false;
                            }
                            else
                            {
                                //Add to negatives list
                                if (candidate < 0)
                                {
                                    listNegatives.Add(candidate);
                                }
                            }
                        }
                    }
                    else if (!int.TryParse(item, out int _))
                    {
                        return false;
                    }
                }

                //Raise exception if any negatives
                if (listNegatives.Count > 0)
                {
                    throw new NegativesNotAllowedException(listNegatives);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
