using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Concordance
{

    class Program
    {
        static void Main(string[] args)
        {
            StreamReader file = null;
            StreamWriter output = null;
            BinarySearchTree tree = null;

            // Ask for file to read from.
            do
            {
                Console.WriteLine("Enter filepath of the desired input text file (must end with '.txt')");
                String filepath = Console.ReadLine();
                while (!filepath.EndsWith(".txt"))
                {
                    Console.WriteLine("file name must end with '.txt'!");
                    filepath = Console.ReadLine();
                }

                try
                {
                    file = new StreamReader(filepath);
                    tree = BuildTree(file);
                    file.Close();
                    Console.WriteLine("The file was read successfully.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (file == null);

            // Ask for a file to read to. (If file does not exist, it will create one)
            do
            {
                Console.WriteLine("Enter filepath of the desired output text file (must end with '.txt')");
                String filepath = Console.ReadLine();
                while (!filepath.EndsWith(".txt"))
                {
                    Console.WriteLine("file name must end with '.txt'!");
                    filepath = Console.ReadLine();
                }

                try
                {
                    output = new StreamWriter(filepath);
                    output.Write(tree);
                    output.Close();
                    Console.WriteLine("The file was created successfully, it contains the Concordance of the read file.");
                    System.Threading.Thread.Sleep(1500);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (output == null);

        }

        static BinarySearchTree BuildTree(StreamReader file) // O(n) where "n" is the number of words in the file.
        {
            BinarySearchTree tree = new BinarySearchTree();

            Regex rgx = new Regex("[^A-Za-z']"); //used to filter all non-alphabet from the final words after splitting each sentence

            String line;
            uint lineNumber = 1;

            while ((line = file.ReadLine()) != null) // this loop reads every line of the file into "line".
            {
                string[] words = line.Split(' ', ',', '-', '_', '!', '.', '—', ':', ';', '?'); // word separators. note we didn't include the character ' as it can be used in a word.
                for (int i = 0; i < words.Length; i++) // this loop reads every word in the line.
                {
                    if (!words[i].Any(char.IsDigit))
                    {
                        words[i] = rgx.Replace(words[i], "").ToLower();
                        if (words[i].EndsWith("'s")) // example: in the phrase "Apple's stem" we want to turn the first word into "apple"
                            words[i] = words[i][0..^2];

                        if (words[i].Length != 0 && !(words[i].StartsWith("'") || words[i].EndsWith("'")))
                            tree.Insert(words[i], lineNumber); // O(lg(n)) on average, O(n) worst case.
                    }
                }

                lineNumber++;
            }
            return tree;
        }
    }
}
