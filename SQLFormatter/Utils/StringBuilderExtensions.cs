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

        public static StringBuilder RemoveLastTab(this StringBuilder stringBuilder)
        {
            if (stringBuilder[^1] == '\t')
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
            IncrementLevel();
        }

        public static int IndentationLevel { get; private set; }

        public static void IncrementLevel()
        {
            IndentationLevel++;
        }

        public static void DecrementLevel()
        {
            IndentationLevel--;
        }

        public void Dispose()
        {
            DecrementLevel();
        }
    }
}
