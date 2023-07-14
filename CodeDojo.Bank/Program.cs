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

        static void Main(string[] args)
        {
            string[] text = File.ReadAllLines("input.txt");

            List<char> output = new();

            for (int i = 0; i < text.Length - 4; i += 4)
            {
                var linesToProcess = text[i..(i + 4)]; //WOOOOOW
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
                }

                Console.WriteLine(new string(output.ToArray()));
                output.Clear();
            }

        }
    }
}