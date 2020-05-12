namespace SQLFormatter.Formatting
{
    using Microsoft.SqlServer.Management.SqlParser.Parser;

    public static class Formatter
    {
        public static string Format(ParseResult parseResult)
        {
            var formattingVisitor = new FormattingVisitor();

            parseResult.Script.Accept(formattingVisitor);

            return formattingVisitor.GetFormattedResult();
        }
    }
}
