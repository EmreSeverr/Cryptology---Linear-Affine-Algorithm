using System;
using System.Collections.Generic;

namespace Cryptology___Linear_Affine_Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Cryptology - Linear,Affine Algorithm\n");

            int a = 0;
            int b = 0;
            int action = -1;
            String keyword = "";

            bool isCorrectAction = false;
            int isTryAgain = -1;

            do
            {
                isCorrectAction = false;

                Console.WriteLine("Formula : y = ax+b");

                Console.Write("Please enter 'a' value : ");
                a = Convert.ToInt32(Console.ReadLine());


                Console.Write("Please enter 'b' value : ");
                b = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("1 - For Encryption");
                Console.WriteLine("2 - For Decryption");
                Console.Write("Please select the action you want to do : ");
                action = Convert.ToInt32(Console.ReadLine());

                switch (action)
                {
                    case 1:
                        Console.Write("Enter the word to be encrypted : ");
                        keyword = Console.ReadLine();
                        String encryptedWord = Encrypt(a, b, keyword);
                        Console.WriteLine($"Encrypted text : {encryptedWord}");
                        break;
                    case 2:
                        Console.Write("Enter the word to be decrypted : ");
                        keyword = Console.ReadLine();
                        String decryptedWord = Decrypt(a, b, keyword);
                        Console.WriteLine($"Decrypted text : {decryptedWord}");
                        break;
                    default:
                        Console.Write("Plese enter a valid value.");
                        isCorrectAction = true;
                        break;
                }

                Console.Write("Would you like to try again? (1-0) : ");
                isTryAgain = Convert.ToInt32(Console.ReadLine());
                if (isTryAgain == 1)
                    isCorrectAction = true;


            } while (isCorrectAction);


            Console.ReadKey();
        }

        private static String Encrypt(int a, int b, String keyword)
        {
            var encryptedWord = new List<char>();
            int hexValue;
            int encryptedChar;

            foreach (var character in keyword)
            {
                hexValue = GetCharDataSet().FindIndex(c => c.Equals(character));
                encryptedChar = (a * hexValue) + b;
                encryptedWord.Add(GetCharDataSet()[(encryptedChar % GetCharDataSet().Count)]);
            }

            return String.Join("", encryptedWord);
        }

        private static String Decrypt(int a, int b, String keyword)
        {
            var encryptedWordHex = new List<int>();
            var encryptedWord = new List<char>();

            int multiplicativeInverse = -1;

            for (int i = 0; i < 100000; i++)
            {
                if ((a * i) % GetCharDataSet().Count == 1)
                {
                    multiplicativeInverse = i;
                    break;
                }
            }

            foreach (var character in keyword)
            {
                var extractionBValue = GetCharDataSet().FindIndex(ch => ch.Equals(character)) - b;

                encryptedWordHex.Add(TakeMod(((TakeMod(extractionBValue, GetCharDataSet().Count)) * multiplicativeInverse), GetCharDataSet().Count));
            }

            foreach (var character in encryptedWordHex)
            {
                encryptedWord.Add(GetCharDataSet()[character]);
            }

            return String.Join("", encryptedWord);
        }

        private static List<Char> GetCharDataSet()
        {
            List<char> dataSet = new List<char>();

            for (int i = 33; i < 126; i++)
            {
                dataSet.Add((char)i);
            }
            dataSet.Add(' ');
            return dataSet;
        }

        private static int TakeMod(int x, int m) => (x % m + m) % m;
    }
}
