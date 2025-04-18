﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Runtime;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Globalization;

using System.Runtime.ExceptionServices;

namespace Задания_1_10
{
    internal class TextFile
    {
        private string _filepath;

        public string FilePath
        {
            get { return _filepath; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Path name cannot be empty.");
                if (Path.GetExtension(value).ToLower() != ".txt")
                    throw new ArgumentException("File must be .txt extension");
                _filepath = value;
            }
        }
        public TextFile()
        {
            _filepath = "C:\\Users\\Viktor\\Desktop\\Лабораторные работы по языкам программирования\\Лабораторная работа 7\\base.txt";
        }
        public TextFile(string filepath)
        {
            this.FilePath = filepath;
        }

        public static void FillFileInLines(string filePath, int count, int minVal, int maxVal)
        {
            Random rand = new Random();
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                for (int i = 0; i < count; i++)
                {
                    int number = rand.Next(minVal, maxVal + 1);
                    sw.WriteLine(number);
                }
            }
        }
        public static int CountMaxEntry(string filePath, ref int maxNum)
        {
            string[] lines = File.ReadAllLines(filePath);
            int[] numbers = new int[lines.Length];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = int.Parse(lines[i]);
            }

            if (numbers.Length == 0)
                return 0;

            maxNum = numbers.Max();
            int localMax = maxNum;
            int maxCount = numbers.Count(n => n == localMax);
            return maxCount;
        }

        public void RunEx1()
        {
            int numCount = 20;
            int minVal = 1;
            int maxVal = 100;
            int maxNum = minVal - 1;

            FillFileInLines(this.FilePath, numCount, minVal, maxVal);
            int maxEntry = CountMaxEntry(this.FilePath, ref maxNum);
            Console.WriteLine($"Maximal number is {maxNum}, it occurs {maxEntry} time(s)");
        }

        public static void FillNumbersInLine(string filePath, int lines, int numberPerLine, int minVal, int maxVal)
        {
            Random rand = new Random();

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < lines; i++)
                {
                    string line = "";
                    for (int j = 0; j < numberPerLine; j++)
                    {
                        {
                            int randNum = rand.Next(minVal, maxVal + 1);
                            line += randNum + " ";
                        }
                    }
                    writer.WriteLine(line.Trim());
                }
            }
        }

        public static int CountEven(string filePath)
        {
            {
                int count = 0;
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] numStr = line.Split(new char[] { ' ' });

                    foreach (string num in numStr)
                    {
                        if (int.TryParse(num, out int number))
                        {
                            if (number % 2 == 0)
                            {
                                count++;
                            }
                        }
                    }
                }
                return count;
            }
        }

        public void RunEx2()
        {
            int linesCount = 20;
            int numberPerLine = 10;
            int minVal = 0;
            int maxVal = 100;

            FillNumbersInLine(this.FilePath, linesCount, numberPerLine, minVal, maxVal);
            int countEv = CountEven(this.FilePath);
            Console.WriteLine($"Quantity of even numbers in file is {countEv}");
        }

        public static void CopyLines(string inputFile, string outputFile, string substring)
        {
            string[] lines = File.ReadAllLines(inputFile);
            List<string> matchedLines = new List<string>();
            foreach (string line in lines)
            {
                if (line.IndexOf(substring, StringComparison.OrdinalIgnoreCase) >= 0)
                { matchedLines.Add(line); }
            }
            File.WriteAllLines(outputFile, matchedLines);
        }

        public void RunEx3()
        {
            string inputFile = "C:\\Users\\Viktor\\Desktop\\Лабораторные работы по языкам программирования\\Лабораторная работа 7\\ex3.txt";
            string substring;
            do
            {
                Console.Write("Enter substring: ");
                substring = Console.ReadLine();
            } while (substring == null);

            CopyLines(inputFile, this.FilePath, substring);
            Console.WriteLine($"Strings written to file: {this.FilePath}");
        }

        public static void FillBinaryFile(string filePath, int count, int minVal, int maxVal)
        {
            Random rand = new Random();
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                for (int i = 0; i < count; i++)
                {
                    int number = rand.Next(minVal, maxVal + 1);
                    writer.Write(number);
                }
            }
        }

        public static int DifferenceMaxMin(string filePath, ref int minVal, ref int maxVal)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.OpenOrCreate)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int number = reader.ReadInt32();
                    if (number > maxVal) maxVal = number;
                    if (number < minVal) minVal = number;
                }
            }
            return maxVal - minVal;
        }

        public void RunEx4()
        {
            int count = 20;
            int minVal = 0;
            int maxVal = 100;
            int localMax = minVal - 1;
            int localMin = maxVal + 1;

            FillBinaryFile(this.FilePath, count, minVal, maxVal);
            int diff = DifferenceMaxMin(this.FilePath, ref localMin, ref localMax);
            Console.WriteLine($"Difference of {localMax} and {localMin} = {diff}");
        }

        public void FillFileToys(string filePath)
        {
            List<ToyEx5> toys = new List<ToyEx5>
            {
                new ToyEx5("Lego", 799.99m, 5, 99),
                new ToyEx5("Auto", 499.99m, 2, 7),
                new ToyEx5("Doll", 749.99m, 4, 8),
                new ToyEx5("Puzzles", 849.99m, 6, 12),
                new ToyEx5("Bow", 599.99m, 7, 11)
            };

            XmlSerializer serializer = new XmlSerializer(typeof(List<ToyEx5>));
            byte[] xmlBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.Serialize(ms, toys);
                xmlBytes = ms.ToArray();
            }

            using (BinaryWriter bw = new BinaryWriter(new FileStream(filePath, FileMode.Create)))
            {
                bw.Write(xmlBytes.Length);
                bw.Write(xmlBytes);
            }
        }

        public List<ToyEx5> DeserializeToys(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ToyEx5>));
            using (BinaryReader br = new BinaryReader(new FileStream(filePath, FileMode.Open)))
            {
                int length = br.ReadInt32();
                byte[] xmlBytes = br.ReadBytes(length);
                using (MemoryStream ms = new MemoryStream(xmlBytes))
                {
                    return (List<ToyEx5>)serializer.Deserialize(ms);
                }
            }
        }

        public List<ToyEx5> GetExpensiveToys(List<ToyEx5> toys, decimal k)
        {
            decimal maxPrice = 0.00m;
            foreach (var toy in toys)
            {
                if (toy.Price > maxPrice)
                    maxPrice = toy.Price;
            }

            List<ToyEx5> expensiveToys = new List<ToyEx5>();
            foreach (var toy in toys)
            {
                if ((maxPrice - toy.Price) <= k)
                    expensiveToys.Add(toy);
            }
            return expensiveToys;
        }

        public void RunEx5()
        {
            decimal k;
            string filePath = "C:\\Users\\Viktor\\Desktop\\Лабораторные работы по языкам программирования\\Лабораторная работа 7\\ex5.txt";

            Console.WriteLine("Ex5 starts here");
            bool isValid = false;
            do
            {
                Console.Write("Enter k in rubles: ");
                string s = (Console.ReadLine());
                isValid = Decimal.TryParse(s, out k) && k >= 0;
                if (!isValid)
                    Console.WriteLine("Enter positive decimal value");
            } while (!isValid);

            List<ToyEx5> toys = new List<ToyEx5>();

            FillFileToys(filePath);
            toys = DeserializeToys(filePath);

            if (toys.Count == 0)
            {
                Console.WriteLine("No toys were found in file.");
                return;
            }

            List<ToyEx5> expensiveToys = GetExpensiveToys(toys, k);
            Console.WriteLine("Most expensive toys:");
            foreach (var toy in expensiveToys)
            {
                Console.WriteLine($"{toy.Name}, price: {toy.Price} Ages:{toy.AgeFrom}-{toy.AgeTo}");
            }
        }

        public void RunEx6()
        {
            Console.WriteLine("Enter elements of list with space:");
            string input = Console.ReadLine();

            string[] elements = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (elements.Length == 0)
            {
                Console.WriteLine("List is empty.");
                return;
            }
            List<string> list = new List<string>(elements);

            string firstElement = list[0];
            list.RemoveAt(0);
            list.Add(firstElement);

            Console.Write("Result is ");
            foreach (string element in list)
            {
                Console.Write($"{element} ");
            }
        }

        public void RunEx7()
        {
            bool isValid;
            int quantity;

            Console.WriteLine("Here starts Ex7");
            Console.WriteLine("Enter linked list with spaces");
            string input = Console.ReadLine();
            string[] elements = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (elements.Length == 0)
            {
                Console.WriteLine("List is empty.");
                return;
            }

            LinkedList<string> list = new LinkedList<string>(elements);
            if (list.First != null && list.Last != null && list.First.Value == list.Last.Value)
            {
                list.RemoveFirst();
                if (list.Count > 0)
                {
                    list.RemoveLast();
                }
            }

            var current = list.First;
            for (int i = 0; i < list.Count; i++)
            {
                var prevNode = current.Previous ?? list.Last;
                var nextNode = current.Next ?? list.First;
                var next = current.Next;

                if (prevNode.Value == nextNode.Value)
                {
                    var nodeToRemove = current;
                    current = next;
                    list.Remove(nodeToRemove);
                }
                else
                {
                    current = next;
                }
            }
            Console.WriteLine("New list is");
            foreach (var element in list)
            {
                Console.Write($"{element} ");
            }
            Console.WriteLine("\n");
        }

        public void RunEx8()
        {
            var allFactories = new HashSet<string>
            { "Factory1", "Factory2", "Factory3", "Factory4" };

            var purchased = new List<HashSet<string>>
            {
                new HashSet<string> { "Factory1", "Factory2", "Factory4" },
                new HashSet<string> { "Factory2", "Factory4" },
                new HashSet<string> { "Factory1", "Factory4" },
            };

            var purchasedByAll = new HashSet<string>(purchased[0]);
            for (int i = 1; i < purchased.Count; i++)
            {
                purchasedByAll.IntersectWith(purchased[i]);
            }

            var purchasedBySome = new HashSet<string>();
            foreach (var purchase in purchased)
            {
                purchasedBySome.UnionWith(purchase);
            }

            var purchasedByNone = new HashSet<string>(allFactories);
            purchasedByNone.ExceptWith(purchasedBySome);

            Console.WriteLine("Here starts Ex8");
            Console.WriteLine($"Factories, those furniture was purchased by all: {string.Join(" ,", purchasedByAll)}");
            Console.WriteLine($"Factories, those furniture was purchased by some: {string.Join(" ,", purchasedBySome)}");
            Console.WriteLine($"Factories, those furniture was purchased by none: {string.Join(" ,", purchasedByNone)}");
            Console.WriteLine("\n");
        }

        public void RunEx9()
        {
            string text = File.ReadAllText(this.FilePath);

            MatchCollection matches = Regex.Matches(text, @"\p{L}+");
            List<string> words = new List<string>();
            foreach (System.Text.RegularExpressions.Match m in matches)
            {
                words.Add(m.Value);
            }
            if (words.Count == 0)
            {
                Console.WriteLine("No words in file.");
                return;
            }

            HashSet<char> voiceless = new HashSet<char>();
            string voicelessChars = "ПФКТШСХЦЧЩпфктшсхцчщ";
            for (int i = 0; i < voicelessChars.Length; i++)
            {
                voiceless.Add(voicelessChars[i]);
            }

            HashSet<char> intersectionOdd = null;
            for (int i = 0; i < words.Count; i += 2)
            {
                string word = words[i];
                HashSet<char> lettersInWord = new HashSet<char>();
                for (int j = 0; j < word.Length; j++)
                {
                    char ch = word[j];
                    if (voiceless.Contains(ch))
                    {
                        lettersInWord.Add(Char.ToUpper(ch));
                    }
                }
                if (intersectionOdd == null)
                {
                    intersectionOdd = lettersInWord;
                }
                else
                {
                    HashSet<char> newIntersection = new HashSet<char>();
                    foreach (char c in intersectionOdd)
                    {
                        if (lettersInWord.Contains(c))
                        {
                            newIntersection.Add(c);
                        }
                    }
                    intersectionOdd = newIntersection;
                }

            }

            if (intersectionOdd == null || intersectionOdd.Count == 0)
            {
                Console.WriteLine("Voiceless consonants were not found in odd words.");
                return;
            }

            HashSet<char> result = new HashSet<char>();
            foreach (char letter in intersectionOdd)
            {
                bool missingInEven = false;
                for (int i = 1; i < words.Count; i += 2)
                {
                    string word = words[i];
                    bool found = false;
                    for (int j = 0; j < word.Length; j++)
                    {
                        if (Char.ToUpper(word[j]) == letter)
                        {
                            found = true; break;
                        }
                    }
                    if (!found)
                    {
                        missingInEven = true; break;
                    }
                }
                if (missingInEven)
                {
                    result.Add(letter);
                }
            }

            char[] resultArr = new char[result.Count];
            int idx = 0;
            foreach (char c in result)
            {
                resultArr[idx++] = c;
            }
            CultureInfo culture = new CultureInfo("ru-RU");
            Array.Sort(resultArr, (a, b) => culture.CompareInfo.Compare(a.ToString(), b.ToString(), CompareOptions.StringSort));

            Console.WriteLine("Voiceless consonants, satisfying the condition: ");
            for (int i = 0; i < resultArr.Length; i++)
            {
                Console.Write(resultArr[i]);
                if (i < resultArr.Length - 1)
                    Console.Write(", ");
            }
            Console.WriteLine("\n");
        }

        public void RunEx10()
        {
            string filePath = "C:\\Users\\Viktor\\Desktop\\Лабораторные работы по языкам программирования\\Лабораторная работа 7\\ex10.txt";
            string[] lines = File.ReadAllLines(filePath);
            if (lines.Length == 0)
            {
                Console.WriteLine("File is empty");
                return;
            }

            int n;
            if (!int.TryParse(lines[0].Trim(), out n))
            {
                Console.WriteLine("First string must contain N number of entries");
                return;
            }

            if (lines.Length < n + 1)
            {
                Console.WriteLine("File contains less entries than N.");
                return;
            }
            Dictionary<int, (int minPrice, int count)> data = new Dictionary<int, (int, int)>();
            int[] fatTypes = { 15, 20, 25 };
            foreach (int fat in fatTypes)
            {
                data[fat] = (int.MaxValue, 0);
            }

            for (int i = 1; i <= n; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length != 4)
                {
                    Console.WriteLine("Incorrect format of entry: " + line);
                    continue;
                }

                int fat;
                int price;
                if (!int.TryParse(tokens[2], out fat) || !int.TryParse(tokens[3], out price))
                {
                    Console.WriteLine("Error in conversion data: " + line);
                    continue;
                }

                if (!data.ContainsKey(fat))
                {
                    continue;
                }

                var current = data[fat];
                if (price < current.minPrice)
                {
                    data[fat] = (price, 1);
                }
                else if (price == current.minPrice)
                {
                    data[fat] = (current.minPrice, current.count + 1);
                }
            }
            Console.WriteLine("Here starts Ex10");
            string output = "";
            foreach (int fat in fatTypes)
            {
                output += data[fat].count.ToString() + " ";
            }
            Console.WriteLine(output.Trim());
        }
    }
}

