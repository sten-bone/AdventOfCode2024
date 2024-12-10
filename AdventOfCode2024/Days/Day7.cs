namespace AdventOfCode2024.Days;

public class Day7
{
    public static long GetTotalCalibration(string input)
    {
        return ParseInput(input).Where(EquationIsSolvable).Sum(x => x.Value);
    }

    public static long GetTotalCalibrationWithConcatenation(string input)
    {
        return ParseInput(input).Where(EquationIsSolvableWithConcatenation).Sum(x => x.Value);
    }

    private static bool EquationIsSolvable(Equation equation)
    {
        var operatorCount = equation.Numbers.Count - 1;
        var operatorBinary = 0;

        while (operatorBinary < Math.Pow(2, operatorCount + 1))
        {
            var currentOperatorIndex = 0;
            var calculation = equation.Numbers.Aggregate((value, curr) =>
            {
                return operatorBinary.GetBinaryCharAt(currentOperatorIndex++, operatorCount) switch

                {
                    '0' => value + curr,
                    '1' => value * curr,
                    _ => throw new ArgumentException()
                };
            });

            if (calculation == equation.Value)
            {
                return true;
            }

            operatorBinary++;
        }

        return false;
    }

    private static bool EquationIsSolvableWithConcatenation(Equation equation)
    {
        var operatorCount = equation.Numbers.Count - 1;
        var operatorTernary = 0;

        while (operatorTernary < Math.Pow(3, operatorCount + 1))
        {
            var currentOperatorIndex = 0;
            var calculation = equation.Numbers.Aggregate((value, curr) =>
            {
                return operatorTernary.GetTernaryCharAt(currentOperatorIndex++, operatorCount) switch

                {
                    '0' => value + curr,
                    '1' => value * curr,
                    '2' => long.Parse($"{value}{curr}"),
                    _ => throw new ArgumentException()
                };
            });

            if (calculation == equation.Value)
            {
                return true;
            }

            operatorTernary++;
        }

        return false;
    }

    private static List<Equation> ParseInput(string input)
    {
        var list = new List<Equation>();

        var reader = new StringReader(input);
        while (reader.ReadLine() is { } line)
        {
            var split = line.Split(':', StringSplitOptions.TrimEntries);
            list.Add(new Equation(long.Parse(split[0]),
                split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList()));
        }

        return list;
    }
}

public record struct Equation(long Value, List<long> Numbers);

public static class Day7Extensions
{
    public static char GetBinaryCharAt(this int value, int index, int length) =>
        Convert.ToString(value, 2).PadLeft(length, '0').ToCharArray()[index];

    public static char GetTernaryCharAt(this int value, int index, int length) =>
       value.ConvertToTernary().PadLeft(length, '0').ToCharArray()[index];

    private static string ConvertToTernary(this int value)
    {
        if (value == 0) return string.Empty;

        var remainder = value % 3;
        value /= 3;

        return $"{value.ConvertToTernary()}{remainder}";
    }
}