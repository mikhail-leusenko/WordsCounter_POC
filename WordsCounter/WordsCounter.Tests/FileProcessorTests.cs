using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordsCounter.Tests
{
    [TestFixture]
    public class FileProcessorTests
    {
        private string TestFilesFolderPath;
        private Dictionary<string, int> ExpectedWordsCount;
        private Dictionary<string, int> ExpectedWordsCountEdgeCase;

        [SetUp]
        public void SetUp()
        {
            this.TestFilesFolderPath = Directory.GetCurrentDirectory() + "\\TestFiles";
            this.ExpectedWordsCount = new Dictionary<string, int>()
            {
                { "Go", 1 },
                { "Do", 1},
                { "do", 2},
                { "that", 2},
                { "thing's", 1},
                { "you", 1},
                { "so", 1},
                { "well", 1},
                { "email@some.text", 1 }
            };

            this.ExpectedWordsCountEdgeCase = new Dictionary<string, int>()
            {
                { "do", 1 },
                { "did", 1 },
                { "done", 1 },
                { "do@did.done", 1 }
            };
        }

        [Test]
        public void CalculateWordOccurrences_WithValidText()
        {
            // Act
            var actualWordsCount = FileProcessor.CalculateWordOccurrences(Constants.Constants.TestString);

            // Assert
            Assert.IsTrue(VerifyTheDictionariesAreEqual(ExpectedWordsCount, actualWordsCount));
        }

        [Test]
        public void CalculateWordOccurrences_EmptyInput()
        {
            // Act
            try 
            { 
                var actualWordsCount = FileProcessor.CalculateWordOccurrences(String.Empty);
            }

            //Assert
            catch(Exception ex)
            {
                Assert.AreEqual(typeof(InvalidOperationException), ex.GetType());
                Assert.AreEqual(Constants.Constants.EmptyFileException, ex.Message);
            }
        }

        [Test]
        public void CalculateWordOccurrences_EdgeCase()
        {
            // Act
            var actualWordsCount = FileProcessor.CalculateWordOccurrences(Constants.Constants.TestStringEdgeCase);

            // Assert
            Assert.IsTrue(VerifyTheDictionariesAreEqual(ExpectedWordsCountEdgeCase, actualWordsCount));
        }


        [Test]
        public void GetStringFromFile_ValidFile()
        {
            // Arrange
            var expectedTextFromFile = "Go,  Do   do,      that#, thing's, that@ ,you do so well. (email@some.text)";

            // Act
            var actualTextFromFile = FileProcessor.GetStringFromFile(GetFileFullPath(Constants.Constants.TestFile));

            // Assert
            Assert.AreEqual(expectedTextFromFile, actualTextFromFile);
        }

        [Test]
        public void GetStringFromFile_EmptyPath()
        {
            // Act
            try
            {
                var extractedText = FileProcessor.GetStringFromFile(String.Empty);
            }

            // Assert
            catch (Exception ex)
            {
                Assert.AreEqual(typeof(ArgumentException), ex.GetType());
                Assert.AreEqual(Constants.Constants.PathIsNullOrEmptyException, ex.Message);
            }
        }

        [Test]
        public void GetStringFromFile_UnsupportedFileExtension()
        {
            // Act
            try
            {
                var extractedText = FileProcessor.GetStringFromFile(GetFileFullPath(Constants.Constants.TestJsonFile));
            }

            // Assert
            catch (Exception ex)
            {
                Assert.AreEqual(typeof(NotSupportedException), ex.GetType());
                Assert.AreEqual(Constants.Constants.UnsupportedExtensionException, ex.Message);
            }
        }

        [Test]
        public void GetStringFromFile_FileDoesNotExist()
        {
            try
            {
                var extractedText = FileProcessor.GetStringFromFile(GetFileFullPath(Constants.Constants.UnexistingFile));
            }

            // Assert
            catch (Exception ex)
            {
                Assert.AreEqual(typeof(FileNotFoundException), ex.GetType());
                Assert.AreEqual(Constants.Constants.SpecifiedFileNotFoundException, ex.Message);
            }
        }

        [Test]
        public void CalculateWordOccurrences_GetStringFromFile_Integration()
        {
            // Arrange
            var extractedText = FileProcessor.GetStringFromFile(GetFileFullPath(Constants.Constants.TestFile));

            // Act
            var actualWordsCount = FileProcessor.CalculateWordOccurrences(extractedText);

            // Assert
            Assert.IsTrue(VerifyTheDictionariesAreEqual(ExpectedWordsCount, actualWordsCount));
        }

        private string GetFileFullPath(string fileName)
        {
            return this.TestFilesFolderPath + fileName;
        }

        private bool VerifyTheDictionariesAreEqual(Dictionary<string, int> expectedDictionary, Dictionary<string, int> actualDictionary)
        {
            bool areEquals = false;

            if ((expectedDictionary.Count != actualDictionary.Count) ||
                expectedDictionary.Keys.Except(actualDictionary.Keys).Any() ||
                actualDictionary.Keys.Except(expectedDictionary.Keys).Any())
            {
                return false;
            }

            foreach (var key in expectedDictionary.Keys)
            {
                areEquals = expectedDictionary[key] == actualDictionary[key] ? true : false;
            }

            return areEquals;
        }
    }
}
