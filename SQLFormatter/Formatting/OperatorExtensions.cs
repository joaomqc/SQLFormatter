namespace SQLFormatter.Formatting
{
    using System;
    using Microsoft.SqlServer.Management.SqlParser.SqlCodeDom;

    public static class OperatorExtensions
    {
        public static string GetStringRepresentation(this SqlBinaryScalarOperatorType operatorType)
        {
            switch (operatorType)
            {
                case SqlBinaryScalarOperatorType.None:
                    return string.Empty;

                case SqlBinaryScalarOperatorType.Add:
                    return "+";

                case SqlBinaryScalarOperatorType.Assign:
                    return "=";

                case SqlBinaryScalarOperatorType.BitwiseAnd:
                    return "&";

                case SqlBinaryScalarOperatorType.BitwiseOr:
                    return "|";

                case SqlBinaryScalarOperatorType.BitwiseXor:
                    return "^";

                case SqlBinaryScalarOperatorType.Divide:
                    return "/";

                case SqlBinaryScalarOperatorType.Equals:
                    return "=";

                case SqlBinaryScalarOperatorType.GreaterThan:
                    return ">";

                case SqlBinaryScalarOperatorType.GreaterThanOrEqual:
                    return ">=";

                case SqlBinaryScalarOperatorType.LessThan:
                    return "<";

                case SqlBinaryScalarOperatorType.LessThanOrEqual:
                    return "<=";

                case SqlBinaryScalarOperatorType.Modulus:
                    return "%";

                case SqlBinaryScalarOperatorType.Multiply:
                    return "*";

                case SqlBinaryScalarOperatorType.Subtract:
                    return "-";

                case SqlBinaryScalarOperatorType.NotEqualTo:
                    return "<>";

                case SqlBinaryScalarOperatorType.NotGreaterThan:
                    return "!>";

                case SqlBinaryScalarOperatorType.NotLessThan:
                    return "!<";

                default:
                    throw new ArgumentOutOfRangeException(nameof(operatorType));
            }
        }
    }
}
