namespace SQLFormatter
{
    using System;
    using System.IO;
    using System.Linq;
    using Arguments;
    using Formatting;

    class Program
    {
        static void Main(string[] args)
        {
            var argsParser = new ArgumentsParser(args)
                .Add(ArgumentConfiguration.InputFileArgument)
                .Add(ArgumentConfiguration.OutputFileArgument)
                .Parse();

            if (argsParser.Errors.Any())
            {
                Console.Error.WriteLine(argsParser.Errors[0]);
                return;
            }
            
            if (argsParser.RemainingArguments.Any())
            {
                Console.Error.WriteLine("Unrecognized arguments");
                return;
            }

            var inputFile = argsParser.Get<string>(ArgumentConfiguration.InputFileArgument.Name);
            var outputFile = argsParser.Get<string>(ArgumentConfiguration.OutputFileArgument.Name);

            var inputString = File.ReadAllText(inputFile);

            var sqlFormatter = new SqlFormattingManager();

            var outputSql = sqlFormatter.FormatSql(inputString);

            File.WriteAllText(outputFile, outputSql);
        }
    }
}
