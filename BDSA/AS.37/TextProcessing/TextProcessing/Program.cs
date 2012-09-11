using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TextProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            // Our keyword
            String query = "th*";

            // Handle + and * symbols (create query pattern)
            query = query.Replace(" + ", @"\s");
            query = query.Replace("+", @"\s");
            if (query.Contains("*"))
            {
                // Do different based on first/last *
                if (query.StartsWith("*"))
                {
                    query = query.Replace("*", @"\b\S*") + @"\b";
                }
                else
                {
                    query = @"\b" + query.Replace("*", @"\S*\b");
                }
            }

            // Create url pattern
            String url = @"\b\S+://\S+\b";

            // Create date pattern
            String weekdays = @"(mon|tue|wed|thu|fri|sat|sun)";
            String months = @"(jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec)";
            String date = @"\b" + weekdays + @", [0-9]+ " + months + @" [0-9]{4} [0-9]{2}:[0-9]{2}:[0-9]{2} (-?)[0-9]{4}";
            // Search for query or url or date
            query = @"(?<query>" + query + @")|(?<url>" + url + @")|(?<date>" + date + @")";

            string content = TextFileReader.ReadFile("../../testFile.txt");
            MatchCollection matches = Regex.Matches(content, query, RegexOptions.IgnoreCase);

            int cursor = 0;
            foreach (Match match in matches)
            {
                // Print part in front of match
                Console.Write(content.Substring(cursor, (match.Index - cursor)));
                // Move cursor to start of match
                cursor = match.Index;
                // Color based on what is matched
                if (match.Groups["query"].Success)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                }
                else if (match.Groups["url"].Success)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                else if (match.Groups["date"].Success)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                // Print match
                Console.Write(content.Substring(cursor, match.Length));
                // Move the cursor to after most current match
                cursor += match.Length;
                // Default back to black
                Console.BackgroundColor = ConsoleColor.Black;
            }
            // Print last part of text (after last match)
            Console.Write(content.Substring(cursor));

            Console.ReadKey();
        }
    }
}