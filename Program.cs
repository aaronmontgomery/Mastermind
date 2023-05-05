// See https://aka.ms/new-console-template for more information
Console.WriteLine("Mastermind");
Console.WriteLine("Aaron Montgomery");
Console.WriteLine("aaronmontgomery1989@outlook.com\n");

byte numberOfTurns; // setting
int numberOfDigits; // setting
int minValue; // setting
int maxValue; // setting
Random random;
byte[] answer;
string input;
IEnumerable<byte> inputs;
byte ip;
char[] score;
IList<char[]> scores;
Func<bool, char, char, char> selector;

numberOfTurns = 10;
numberOfDigits = 4;
minValue = 1;
maxValue = 6;
random = new Random();
answer = new byte[numberOfDigits];
scores = new List<char[]>();
selector = (x, y, z) => x ? y : z;

for (byte b = 0; b < answer.Length; b++)
{
    answer[b] = (byte)random.Next(minValue, maxValue);
}

//answer = new byte[] { 4, 3, 2, 1 }; // debug

for (byte numberOfTurnsRemaining = numberOfTurns; numberOfTurnsRemaining > 0; numberOfTurnsRemaining--)
{
    try
    {
        Console.WriteLine($"\nTurn #: {numberOfTurns - numberOfTurnsRemaining + 1}");
        input = Console.ReadLine()!;
        score = new char[numberOfDigits];
        inputs = input.Take(numberOfDigits).Select(x => byte.Parse(new ReadOnlySpan<char>(x)));
        for (byte b = 0; b < answer.Length; b++)
        {
            ip = inputs.ElementAt(b);
            score[b] = selector(answer[b] == ip, '+', selector(answer.Contains(ip), '-', ' '));
        }

        scores.Add(score);
        Console.WriteLine($"{string.Join(string.Empty, score.Select(x => selector(x == '+', '+', ' ')))}");
        Console.WriteLine($"{string.Join(string.Empty, score.Select(x => selector(x == '-', '-', ' ')))}");
        if (score.All(x => x == '+'))
        {
            numberOfTurnsRemaining = 1;
        }
    }

    catch
    {
        numberOfTurnsRemaining += 1;
    }
}

if (!scores.Last().All(x => x == '+'))
{
    Console.WriteLine("\nYou lose");
}
