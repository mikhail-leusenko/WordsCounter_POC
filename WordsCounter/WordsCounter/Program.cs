using System;

namespace WordsCounter
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Full File Path:");

            var filePath = Console.ReadLine();

            var extractedText = FileProcessor.GetStringFromFile(filePath);

            var wordOccurrences = FileProcessor.CalculateWordOccurrences(extractedText);

            Console.WriteLine("Such words occurrences has been detected:");

            foreach(var occurence in wordOccurrences)
            {
                Console.WriteLine(occurence.Value + ": " + occurence.Key);
            }
        }
    }
}
