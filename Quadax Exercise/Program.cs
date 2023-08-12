using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Quadax_Exercise
{
    class Program
    {
        static void Main(string[] args)
        {
            RandomNumberGenerator randomNumberGenerator = new RandomNumberGenerator();
            UserInteraction userInteraction = new UserInteraction();
            bool userwon = false;

            randomNumberGenerator.GetAllDigits();
            string getnumber = randomNumberGenerator.CompleteNumber;

            for (int i = 0; i < 10; i++)
            {
                userInteraction.SendMessageToUser();
                userInteraction.GetDigitsFromUser();

                string receivedDigitsFromConsole = userInteraction.GetSetDigitsFromUser;

                if (Regex.IsMatch(receivedDigitsFromConsole, @"^\d{4}$") == false)
                {
                    Console.WriteLine("You must enter exactly 4 digits.");
                    continue;
                }

                var (numPlus, numMinus) = randomNumberGenerator.NumberOfMinusPlusSigns(receivedDigitsFromConsole);

                if (numPlus == 4)
                {
                    userwon = true;
                    Console.WriteLine("You won!");
                    break;
                }
                else
                {
                    for (int plus = 0; plus < numPlus; plus++)
                    {
                        userInteraction.PrintPlus();
                    }

                    for (int minus = 0; minus < numMinus; minus++)
                    {
                        userInteraction.PrintMinus();
                    }

                    Console.WriteLine();
                }
            }

            if (!userwon)
            {
                Console.WriteLine("You lost. Sorry");
            }
        }





        public class RandomNumberGenerator : IRandomNumberGenerator
        {

            private string Number = "";
            public string CompleteNumber => Number;

            public RandomNumberGenerator()
            {


            }

            private string ComputeRandomNumbers()
            {
                string answer = "";
                Random rnd = new Random();
                int next = rnd.Next(1, 6);
                answer = next.ToString();
                return answer;
            }

            public void GetAllDigits()
            {
                string digit4 = ""; // 4 digit number
                Random rnd = new Random();
                for (int i = 0; i < 4; i++)
                {
                    digit4 += rnd.Next(1, 6); // this.ComputeRandomNumbers();
                }

                this.Number = digit4;
            }

            

            public (int, int) NumberOfMinusPlusSigns(string incomingDigits)
            {
                int position = 0;
                int plus = 0;
                int minus = 0;
                Dictionary<char, int> trackNumDigits = new Dictionary<char, int>();

                foreach(var digit in incomingDigits)
                {
                    trackNumDigits.TryGetValue(digit, out var thenumUserEntered);
                    if (digit == this.Number[position] /*&& (thenum < maxdigits || thenum == 0)*/)
                    {
                        plus++;     
                        if (thenumUserEntered == 0)
                        {
                            trackNumDigits[digit] = 1;
                        }
                        else
                        {
                            trackNumDigits[digit]++;
                        }
                    }

                    position++;
                }

                foreach (var digit in incomingDigits)
                {
                    int maxdigitsUserEntered = incomingDigits/*this.Number*/.Count(cnt => cnt == digit);
                    int maxdigitsRandom = this.Number.Count(cnt => cnt == digit);
                    trackNumDigits.TryGetValue(digit, out var thenumUserEntered);

                    //if (digit == this.Number[position] /*&& (thenum < maxdigits || thenum == 0)*/)
                    //{
                    //    plus++;
                    //    //trackNumDigits[digit] = plus;
                    //}
                    //else
                    //{
                        //is it in the rest of the string?
                        // get substring
                        //var gotrestOfString = this.Number.Substring(/*position*/0, /*4 - position*/this.Number.Length);
                        // is it there?

                        var otherPositions = this.Number/*gotrestOfString*/.FirstOrDefault(fd => fd == digit);
                        if (otherPositions != '\0'  && ((thenumUserEntered  < maxdigitsUserEntered && thenumUserEntered < maxdigitsRandom) || thenumUserEntered == 0))
                        {
                            // don't go above total count
                            minus++;
                            if (thenumUserEntered == 0)
                            {
                                trackNumDigits[digit] = 1;
                            }
                            else
                            {
                                trackNumDigits[digit]++;
                            }
                        }

                        // now look back
                        //gotrestOfString = this.Number.Substring(4 - position,)
                    //}

                    //position++;
                }

                return (plus, minus);
            }
        }

        public class UserInteraction : IUserInteraction
        {
            private string DigitsFromUser = "";
            public string GetSetDigitsFromUser
            {
                get
                { return DigitsFromUser; }

                set { DigitsFromUser = value; }
            }

            public void SendMessageToUser()
            {
                Console.WriteLine($"Enter a 4 digit number from 1 to 6: " +
                    $"Press <Enter> when done.");
            }

            public UserInteraction()
            {

            }
            public void GetDigitsFromUser()
            {
                this.DigitsFromUser = Console.ReadLine();

            }

            public void PrintRandomlyGeneratedNumber()
            {
                Console.WriteLine($"Randomly generated string is {this.DigitsFromUser}");
            }

            public void PrintPlus()
            {
                Console.Write("+");
            }

            public void PrintMinus()
            {
                Console.Write($"-");
            }
        }


        public interface IUserInteraction
        {
            void GetDigitsFromUser();
            void SendMessageToUser();
            void PrintRandomlyGeneratedNumber();
            void PrintPlus();
            void PrintMinus();
        }

        public interface IRandomNumberGenerator
        {
            void GetAllDigits();
            (int, int) NumberOfMinusPlusSigns(string incomingDigits);
        }
    }
}
