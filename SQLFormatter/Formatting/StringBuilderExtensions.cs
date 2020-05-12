namespace SQLFormatter.Formatting
{
    using System;
    using System.Text;

    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendIndentedLine(this StringBuilder stringBuilder)
        {
            return stringBuilder
                .AppendLine()
                .Append(new string('\t', IndentationContext.IndentationLevel));
        }

        public static IndentationContext CreateIndentationContext(this StringBuilder indentationContext)
        {
            return new IndentationContext();
        }
    }

    public class IndentationContext : IDisposable
    {
        public IndentationContext()
        {
            IndentationLevel++;
        }

        public static int IndentationLevel { get; private set; }

        public void Dispose()
        {
            IndentationLevel--;
        }
    }
}
