using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordsCounter
{
    public static class FileProcessor
    {
        /// <summary>
        /// Calculate occurences of each unique word.
        /// </summary>
        /// <param name="extractedText">String representation of text which was extracted from file. </param>
        /// <returns>A dictionary with word as a Key and a count is a Value. </returns>
        public static Dictionary<string, int> CalculateWordOccurrences(string extractedText)
        {
            var words = RemoveExtraCharacters(extractedText);

            if (!words.Any())
            {
                throw new InvalidOperationException("The specified file is empty.");
            }

            var calculatedWordOccurrences = new Dictionary<string, int>();

            foreach(var word in words)
            {
                if (calculatedWordOccurrences.ContainsKey(word))
                {
                    continue;
                }
                else
                {
                    calculatedWordOccurrences.Add(word, 
                        words.Where(x => x == word).Count());
                }
            }

            return calculatedWordOccurrences;
        }
        
        /// <summary>
        /// Gets the string representation of the file content.
        /// </summary>
        /// <param name="filePath">Full path to the file. </param>
        /// <returns>Stringh representation of the specified file's content. </returns>
        public static string GetStringFromFile(string filePath)
        {
            ValidateFilePath(filePath);

            try
            {
                return File.ReadAllText(filePath);
            }
            catch(FileNotFoundException ex)
            {
                throw new FileNotFoundException("File was not found by specified path. Check the path that you specified.", ex);
            }
        }

        private static void ValidateFilePath(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("The path to file was not specified.");
            }
            if (Path.GetExtension(filePath) != ".txt")
            {
                throw new NotSupportedException("The specified extension is not supported.");
            }
        }

        private static List<string> RemoveExtraCharacters(string extractedText)
        {
            var words = extractedText.Split(" ").ToList();

            var modifiedWords = new List<string>();

            foreach(var word in words)
            {
                if(word != "")
                {
                    modifiedWords.Add(word
                        .Trim(new Char[] { '!', '#', '$', '^', '&', '*', '(', ')', '=', '+', '\\', '|', ',', ':', ';', '?', ' ', '.', '@', '{' ,'}', '[', ']', '<', '>' }));
                }    
            }

            return modifiedWords;
        }
    }
}
