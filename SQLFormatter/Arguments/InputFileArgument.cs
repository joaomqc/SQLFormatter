namespace SQLFormatter.Arguments
{
    public class InputFileArgument : IArgument
    {
        public string Name { get; } = "InputFile";

        public string[] Options { get; } = { "-i", "--inputFile" };

        public bool Optional { get; } = false;

        public bool HasValue { get; } = true;

        public ValidationType ValidationType { get; } = ValidationType.FileExists;
    }
}
