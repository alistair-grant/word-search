using System;

namespace WordSearch
{
    class Program
    {
        static char[,] Grid = new char[,]
        {
            {'C', 'P', 'K', 'X', 'O', 'I', 'G', 'H', 'S', 'F', 'C', 'H'},
            {'Y', 'G', 'W', 'R', 'I', 'A', 'H', 'C', 'Q', 'R', 'X', 'K'},
            {'M', 'A', 'X', 'I', 'M', 'I', 'Z', 'A', 'T', 'I', 'O', 'N'},
            {'E', 'T', 'W', 'Z', 'N', 'L', 'W', 'G', 'E', 'D', 'Y', 'W'},
            {'M', 'C', 'L', 'E', 'L', 'D', 'N', 'V', 'L', 'G', 'P', 'T'},
            {'O', 'J', 'A', 'A', 'V', 'I', 'O', 'T', 'E', 'E', 'P', 'X'},
            {'C', 'D', 'B', 'P', 'H', 'I', 'A', 'W', 'V', 'X', 'U', 'I'},
            {'L', 'G', 'O', 'S', 'S', 'B', 'R', 'Q', 'I', 'A', 'P', 'K'},
            {'E', 'O', 'I', 'G', 'L', 'P', 'S', 'D', 'S', 'F', 'W', 'P'},
            {'W', 'F', 'K', 'E', 'G', 'O', 'L', 'F', 'I', 'F', 'R', 'S'},
            {'O', 'T', 'R', 'U', 'O', 'C', 'D', 'O', 'O', 'F', 'T', 'P'},
            {'C', 'A', 'R', 'P', 'E', 'T', 'R', 'W', 'N', 'G', 'V', 'Z'}
        };

        static string[] Words = new string[] 
        {
            "CARPET",
            "CHAIR",
            "DOG",
            "BALL",
            "DRIVEWAY",
            "FISHING",
            "FOODCOURT",
            "FRIDGE",
            "GOLF",
            "MAXIMIZATION",
            "PUPPY",
            "SPACE",
            "TABLE",
            "TELEVISION",
            "WELCOME",
            "WINDOW"
        };

        static int[,] Deltas = new int[,]
        {
            { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, -1 }, { 0, 1 }, { 1, -1 }, { 1, 0 }, { 1, 1 }
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Word Search");

            for (int y = 0; y < 12; y++)
            {
                for (int x = 0; x < 12; x++)
                {
                    Console.Write(Grid[y, x]);
                    Console.Write(' ');
                }
                Console.WriteLine("");

            }

            Console.WriteLine("");
            Console.WriteLine("Found Words");
            Console.WriteLine("------------------------------");

            FindWords();

            Console.WriteLine("------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Press any key to end");
            Console.ReadKey();
        }

        private static void FindWords()
        {
            foreach (string word in Words)
            {
                for (int startY = 0; startY < 12; startY++)
                {
                    for (int startX = 0; startX < 12; startX++)
                    {
                        int endX = startX;
                        int endY = startY;

                        if (Matches(word, ref endX, ref endY))
                        {
                            Console.WriteLine($"{word} found at ({startX}, {startY}) to ({endX}, {endY})");
                        }
                    }
                }
            }
        }

        private static bool Matches(char letter, int x, int y)
        {
            if (OutOfRange(x, y))
            {
                return false;
            }

            return letter == Grid[y, x];
        }

        private static bool Matches(string word, ref int endX, ref int endY)
        {
            int startX = endX;
            int startY = endY;

            for (int i = 0; i < 8; i++)
            {
                int deltaX = Deltas[i, 1];
                int deltaY = Deltas[i, 0];

                endX = startX;
                endY = startY;

                if (Matches(word, ref endX, ref endY, deltaX, deltaY))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool Matches(string word, ref int endX, ref int endY, int deltaX, int deltaY)
        {
            int length = word.Length;

            return Matches(word, 0, length, ref endX, ref endY, deltaX, deltaY);
        }

        private static bool Matches(string word, int startIndex, int length, ref int endX, ref int endY, int deltaX, int deltaY)
        {
            char letter = word[startIndex];
            if (!Matches(letter, endX, endY))
            {
                return false;
            }

            startIndex++;
            if (startIndex >= length)
            {
                return true;
            }

            endX += deltaX;
            endY += deltaY;

            return Matches(word, startIndex,length, ref endX, ref endY, deltaX, deltaY);
        }

        private static bool OutOfRange(int x, int y)
        {
            return x < 0 || x >= 12 || y < 0 || y >= 12;
        }
    }
}
