namespace SQLFormatter.Arguments
{
    public interface IArgument
    {
        public string Name { get; }

        public string[] Options { get; }

        public bool Optional { get; }

        public bool HasValue { get; }

        public ValidationType ValidationType { get; }
    }
}
