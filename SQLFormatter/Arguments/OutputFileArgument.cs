namespace SQLFormatter.Arguments
{
    public class OutputFileArgument : IArgument
    {
        public string Name { get; } = "OutputFile";

        public string[] Options { get; } = { "-o", "--outputFile" };

        public bool Optional { get; } = false;

        public bool HasValue { get; } = true;

        public ValidationType ValidationType { get; } = ValidationType.CanCreateFile;
    }
}
