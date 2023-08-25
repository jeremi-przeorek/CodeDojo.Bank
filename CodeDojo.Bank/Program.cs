namespace CodeDojo.Bank // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        private static readonly IReadOnlyDictionary<string[], char> _digits = new Dictionary<string[], char>()
        {
            { new[] { " _ ", "| |", "|_|" }, '0' },
            { new[] { "   ", "  |", "  |" }, '1' },
            { new[] { " _ ", " _|", "|_ " }, '2' },
            { new[] { " _ ", " _|", " _|" }, '3' },
            { new[] { "   ", "|_|", "  |" }, '4' },
            { new[] { " _ ", "|_ ", " _|" }, '5' },
            { new[] { " _ ", "|_ ", "|_|" }, '6' },
            { new[] { " _ ", "  |", "  |" }, '7' },
            { new[] { " _ ", "|_|", "|_|" }, '8' },
            { new[] { " _ ", "|_|", " _|" }, '9' }
        };

        private static readonly IReadOnlyDictionary<char, char[]> _similarDigits = new Dictionary<char, char[]>()
        {
            { '0', new[] { '8' } },
            { '1', new[] { '7' } },
            { '2', Array.Empty<char>() },
            { '3', new[] { '9' } },
            { '4', Array.Empty<char>() },
            { '5', new[] { '9', '6' } },
            { '6', new[] { '5', '8' } },
            { '7', new[] { '1' } },
            { '8', new[] { '0', '6', '9' } },
            { '9', new[] { '8', '5', '3' } }
        };

        private const char InvalidChar = '?';

        static void Main(string[] args)
        {
            string[] text = File.ReadAllLines("input.txt");

            List<char> output = new();

            var alternativesWithPosition = new Dictionary<int, List<char>>();

            for (int i = 0; i < text.Length - 4; i += 4)
            {
                var linesToProcess = text[i..(i + 4)];
                for (int j = 0; j < linesToProcess[0].Length / 3; j++)
                {
                    var top = new string(linesToProcess[0].Skip(j * 3).Take(3).ToArray());
                    var middle = new string(linesToProcess[1].Skip(j * 3).Take(3).ToArray());
                    var bottom = new string(linesToProcess[2].Skip(j * 3).Take(3).ToArray());

                    var matchingDigit = _digits.Keys.FirstOrDefault(x => x[0] == top
                                                                         && x[1] == middle
                                                                         && x[2] == bottom);

                    if (matchingDigit is not null)
                    {
                        var value = _digits[matchingDigit];
                        output.Add(value);
                    }
                    else
                    {
                        output.Add(InvalidChar);
                        Console.WriteLine(top);
                        Console.WriteLine(middle);
                        Console.WriteLine(bottom);
                        var alternativesForDigits =
                            GetAlternativesForDigits(
                                new KeyValuePair<string[], char>(new[] { top, middle, bottom }, '?'));
                        alternativesWithPosition.Add(j, alternativesForDigits);
                    }
                }

                foreach (var alt in alternativesWithPosition)
                {
                    Console.WriteLine(alt.Key);
                    alt.Value.ForEach(Console.WriteLine);
                    
                    //Replacing happens here
                    entry[alt.Key] = 
                }
                
                
                alternativesWithPosition.Clear();
                
                string entry = new string(output.ToArray());
                entry = entry switch
                {
                    string e when IsIllegible(e) => entry + " ILL",
                    string e when !IsCheckSumValid(e) => entry + " ERR",
                    _ => entry
                };

                Console.WriteLine(entry);

                if (entry.EndsWith("ERR"))
                {
                    List<string> validAlternatives = new();
                    string digits = entry[0..9];
                    for (var index = 0; index < digits.Length; index++)
                    {
                        var digit = digits[index];
                        char[] possibleDigits = _similarDigits[digit];

                        foreach (var possibleDigit in possibleDigits)
                        {
                            char[] digitsCopy = digits.ToCharArray();
                            digitsCopy[index] = possibleDigit;

                            string input = new string(digitsCopy);
                            if (IsCheckSumValid(input))
                            {
                                validAlternatives.Add(input);
                            }
                        }
                    }

                    if (validAlternatives.Count == 1)
                    {
                        Console.WriteLine(validAlternatives[0]);
                    }

                    if (validAlternatives.Count() > 1)
                    {
                        validAlternatives.ForEach(x => Console.WriteLine(x));
                    }
                }

                output.Clear();
            }
        }

        static bool IsCheckSumValid(string input)
        {
            IEnumerable<char> reversedInput = input.Reverse();

            int counter = 1;
            int sum = 0;
            foreach (var letter in reversedInput)
            {
                int output;
                bool isValid = int.TryParse(letter.ToString(), out output);
                if (isValid)
                {
                    sum += output * counter;
                }

                counter++;
            }

            return sum % 11 == 0;
        }

        static bool IsIllegible(string input)
            => input.Any(x => x is '?');

        private static List<char> GetAlternativesForDigits(KeyValuePair<string[], char> digit)
        {
            List<char> output = new();

            for (int line = 0; line < digit.Key.Length; line++)
            {
                for (int c = 0; c < digit.Key[line].Length; c++)
                {
                    string lineShape = new string(digit.Key[line]);
                    char[] ch = lineShape.ToCharArray();
                    ch[c] = ch[c] == ' ' ? c == 1 ? '_' : line > 0 ? '|' : ' ' : ' ';
                    string newLineShape = new string(ch);
                    String[] newDigitShape = new string[digit.Key.Length];


                    for (int i = 0; i < newDigitShape.Length; i++)
                    {
                        newDigitShape[i] = i != line ? digit.Key[i] : newLineShape;
                    }


                    var result = _digits.FirstOrDefault(d =>
                        d.Key[0] == newDigitShape[0] && d.Key[1] == newDigitShape[1] && d.Key[2] == newDigitShape[2]);
                    if (result.Value != 0)
                    {
                        if (result.Value != digit.Value)
                        {
                            Console.WriteLine($"for {digit.Value} an alternative is {result.Value} ");
                            output.Add(result.Value);
                        }
                    }
                }
            }

            return output;
        }
    }
}