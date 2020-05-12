namespace SQLFormatter.Formatting
{
    using Formatting;
    using Microsoft.SqlServer.Management.SqlParser.Parser;

    public class SqlFormattingManager
    {
        public string FormatSql(string inputSql)
        {
            var parseResult = Parser.Parse(inputSql);

            var outputString = Formatter.Format(parseResult);

            return outputString;
        }
    }
}
