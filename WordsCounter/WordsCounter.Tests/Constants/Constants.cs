namespace WordsCounter.Tests.Constants
{
    public static class Constants
    {

        // File names constants

        public const string TestFile = "\\test.txt";
        public const string EmptyTestFile = "\\test_empty.txt";
        public const string TestJsonFile = "\\test.json";
        public const string UnexistingFile = "\\DoesNotExist.txt";

        // Strings to process
        public const string TestString = "Go,  Do   do,      that#, thing's, that@ ,you do so well. (email@some.text)";
        public const string TestStringEdgeCase = " do, did, done, do@did.done";

        // Exceptions messages
        public const string EmptyFileException = "The specified file is empty.";
        public const string PathIsNullOrEmptyException = "The path to file was not specified.";
        public const string UnsupportedExtensionException = "The specified extension is not supported.";
        public const string SpecifiedFileNotFoundException = "File was not found by specified path. Check the path that you specified.";
    }
}
