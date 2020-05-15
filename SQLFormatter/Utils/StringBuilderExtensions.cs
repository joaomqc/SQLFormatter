namespace SQLFormatter.Utils
{
    using System;
    using System.Text;

    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendIndentedLine(this StringBuilder stringBuilder)
        {
            return stringBuilder
                .RemoveLastSpace()
                .AppendLine()
                .Append(new string('\t', IndentationContext.IndentationLevel));
        }

        public static IndentationContext CreateIndentationContext(this StringBuilder indentationContext)
        {
            return new IndentationContext();
        }

        public static StringBuilder RemoveLastSpace(this StringBuilder stringBuilder)
        {
            if (stringBuilder[^1] == ' ')
            {
                stringBuilder.Length--;
            }

            return stringBuilder;
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
