namespace SQLFormatter.Formatting
{
    using System;
    using System.Linq;
    using System.Text;
    using Microsoft.SqlServer.Management.SqlParser.Parser;
    using Microsoft.SqlServer.Management.SqlParser.SqlCodeDom;

    public static class Formatter
    {
        public static string Format(ParseResult parseResult)
        {
            var statements = parseResult.Script.Batches
                .SelectMany(batch => batch.Statements)
                .ToList();

            var formattedStatements = statements
                .Select(Format)
                .ToList();

            var output = string.Join(string.Concat(Enumerable.Repeat(Environment.NewLine, 2)), formattedStatements);

            return output;
        }

        private static string Format(SqlCodeObject sqlCodeObject)
        {
            switch (sqlCodeObject)
            {
                case SqlSelectStatement selectStatement:
                    return FormatQueryExpression(selectStatement.SelectSpecification.QueryExpression) + ";";

                case SqlVariableDeclareStatement declareStatement:
                    return FormatVariableDeclareStatement(declareStatement);

                case SqlSelectClause selectClause:
                    return FormatSelectClause(selectClause);

                case SqlFromClause fromClause:
                    return FormatClause("FROM", fromClause);

                case SqlWhereClause whereClause:
                    return FormatClause("WHERE", whereClause);

                case SqlSelectExpression selectExpression:
                    return Format(selectExpression.Children.ToList()[0]);

                case SqlScalarVariableAssignment scalarVariableAssignment:
                    return FormatScalarVariableAssignment(scalarVariableAssignment);

                case SqlScalarSubQueryExpression subQueryExpression:
                    return FormatSubQueryExpression(subQueryExpression);

                case SqlScalarRefExpression scalarRefExpression:
                    return scalarRefExpression.Sql;
            }

            //throw new ArgumentException();

            return string.Empty;
        }

        private static string FormatScalarVariableAssignment(SqlScalarVariableAssignment scalarVariableAssignment)
        {
            return $"{scalarVariableAssignment.Variable.Sql} = {scalarVariableAssignment.Value.Sql}";
        }

        private static string FormatSelectClause(SqlSelectClause clause)
        {
            var stringBuilder = new StringBuilder("SELECT");

            var children = clause.Children.ToList();

            if (children.Count == 1 && children[0] is SqlSelectStarExpression)
            {
                stringBuilder.Append(" *");
            }
            else
            {
                using (stringBuilder.CreateIndentationContext())
                {
                    var formattedChildren = children
                        .Select(Format)
                        .ToList();

                    var isFirst = true;

                    foreach (var child in formattedChildren)
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                        }
                        else
                        {
                            stringBuilder
                                .Append(",");
                        }

                        stringBuilder
                            .AppendIndentedLine()
                            .Append(child);
                    }
                }
            }

            return stringBuilder.ToString();
        }

        private static string FormatClause(string statementSql, SqlCodeObject clause)
        {
            var stringBuilder = new StringBuilder(statementSql);

            var children = clause.Children.ToList();

            if (children.Count == 1 && children[0] is SqlSelectStarExpression)
            {
                stringBuilder.Append(" *");
            }
            else
            {
                using (stringBuilder.CreateIndentationContext())
                {

                    var formattedChildren = children
                        .Select(s => s.Sql)
                        .ToList();

                    foreach (var child in formattedChildren)
                    {
                        stringBuilder
                            .AppendIndentedLine()
                            .Append(child);
                    }

                }
            }

            return stringBuilder.ToString();
        }

        private static string FormatQueryExpression(SqlQueryExpression queryExpression)
        {
            var formattedClauses = queryExpression.Children
                .Select(Format)
                .ToList();

            var stringBuilder = new StringBuilder();
            
            var isFirst = true;

            foreach (var clause in formattedClauses)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    stringBuilder.AppendIndentedLine();
                }

                stringBuilder.Append(clause);
            }

            return stringBuilder.ToString();
        }

        private static string FormatVariableDeclareStatement(SqlVariableDeclareStatement variableDeclareStatement)
        {
            var stringBuilder = new StringBuilder();

            var isFirst = true;
            foreach (var variableDeclaration in variableDeclareStatement.Declarations)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    stringBuilder.AppendIndentedLine();
                }

                stringBuilder
                    .Append($"DECLARE {variableDeclaration.Name} {variableDeclaration.Type.Sql.ToUpper()}");

                if (variableDeclaration.Value != null)
                {
                    stringBuilder.Append($" = {Format(variableDeclaration.Value)}");
                }

                stringBuilder.Append(";");
            }

            return stringBuilder.ToString();
        }

        private static string FormatSubQueryExpression(SqlScalarSubQueryExpression subQueryExpression)
        {
            var stringBuilder = new StringBuilder("(");
            using(stringBuilder.CreateIndentationContext()){
                stringBuilder
                    .AppendIndentedLine()
                    .Append(FormatQueryExpression(subQueryExpression.QueryExpression));
            }
            
            stringBuilder.AppendIndentedLine().Append(")");

            return stringBuilder.ToString();
        }
    }
}
