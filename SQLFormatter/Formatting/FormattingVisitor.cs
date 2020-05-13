namespace SQLFormatter.Formatting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.SqlServer.Management.SqlParser.Parser;
    using Microsoft.SqlServer.Management.SqlParser.SqlCodeDom;
    using Utils;

    public class FormattingVisitor : SqlCodeObjectRecursiveVisitor
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        public string GetFormattedResult()
        {
            return _stringBuilder.ToString();
        }

        public override void Visit(SqlUnqualifiedJoinTableExpression codeObject)
        {
        }

        public override void Visit(SqlUnpivotTableExpression codeObject)
        {
        }

        public override void Visit(SqlUnpivotClause codeObject)
        {
        }

        public override void Visit(SqlUniqueConstraint codeObject)
        {
        }

        public override void Visit(SqlUnaryScalarExpression codeObject)
        {
        }

        public override void Visit(SqlUdtStaticMethodExpression codeObject)
        {
        }

        public override void Visit(SqlUpdateBooleanExpression codeObject)
        {
        }

        public override void Visit(SqlUdtStaticDataMemberExpression codeObject)
        {
        }

        public override void Visit(SqlUdtInstanceDataMemberExpression codeObject)
        {
        }

        public override void Visit(SqlTriggerEvent codeObject)
        {
        }

        public override void Visit(SqlTriggerDefinition codeObject)
        {
        }

        public override void Visit(SqlTriggerAction codeObject)
        {
        }

        public override void Visit(SqlTopSpecification codeObject)
        {
            _stringBuilder.Append("TOP(");

            codeObject.Value.Accept(this);
            
            _stringBuilder.Append(")");
        }

        public override void Visit(SqlTemporalPeriodDefinition codeObject)
        {
        }

        public override void Visit(SqlUdtInstanceMethodExpression codeObject)
        {
        }

        public override void Visit(SqlTargetTableExpression codeObject)
        {
        }

        public override void Visit(SqlUpdateMergeAction codeObject)
        {
        }

        public override void Visit(SqlUserDefinedScalarFunctionCallExpression codeObject)
        {
        }

        public override void Visit(SqlBackupLogStatement codeObject)
        {
        }

        public override void Visit(SqlBackupDatabaseStatement codeObject)
        {
        }

        public override void Visit(SqlBackupCertificateStatement codeObject)
        {
        }

        public override void Visit(SqlAlterViewStatement codeObject)
        {
        }

        public override void Visit(SqlAlterTriggerStatement codeObject)
        {
        }

        public override void Visit(SqlAlterProcedureStatement codeObject)
        {
        }

        public override void Visit(SqlUpdateSpecification codeObject)
        {
        }

        public override void Visit(SqlAlterLoginStatement codeObject)
        {
        }

        public override void Visit(SqlXmlNamespacesDeclaration codeObject)
        {
        }

        public override void Visit(SqlWhereClause codeObject)
        {
            _stringBuilder.Append("WHERE");

            using (_stringBuilder.CreateIndentationContext())
            {
                foreach (var child in codeObject.Children)
                {
                    _stringBuilder.AppendIndentedLine();

                    child.Accept(this);
                }

            }
        }

        public override void Visit(SqlViewDefinition codeObject)
        {
        }

        public override void Visit(SqlVariableDeclaration codeObject)
        {
        }

        public override void Visit(SqlVariableColumnAssignment codeObject)
        {
        }

        public override void Visit(SqlValuesInsertMergeActionSource codeObject)
        {
        }

        public override void Visit(SqlAlterFunctionStatement codeObject)
        {
        }

        public override void Visit(SqlBackupMasterKeyStatement codeObject)
        {
        }

        public override void Visit(SqlTableUdtInstanceMethodExpression codeObject)
        {
        }

        public override void Visit(SqlTableValuedFunctionRefExpression codeObject)
        {
        }

        public override void Visit(SqlSetClause codeObject)
        {
            for (var i = 0; i < codeObject.Assignments.Count; i++)
            {
                if (i > 0)
                {
                    _stringBuilder.AppendIndentedLine();
                }

                var children = codeObject.Assignments[i].Children.ToList();
                
                children[0].Accept(this);

                _stringBuilder.Append(" = ");

                children[1].Accept(this);
            }
        }

        public override void Visit(SqlSelectVariableAssignmentExpression codeObject)
        {
            codeObject.VariableAssignment.Accept(this);
        }

        public override void Visit(SqlSelectStarExpression codeObject)
        {
            codeObject.Qualifier?.Accept(this);

            _stringBuilder.Append("*");
        }

        public override void Visit(SqlSelectSpecificationInsertSource codeObject)
        {
        }

        public override void Visit(SqlSelectSpecification codeObject)
        {
        }

        public override void Visit(SqlSelectScalarExpression codeObject)
        {
            codeObject.Expression.Accept(this);

            if (codeObject.Alias != null)
            {
                _stringBuilder.Append(" AS ");

                codeObject.Alias.Accept(this);
            }
        }

        public override void Visit(SqlSimpleCaseExpression codeObject)
        {
        }

        public override void Visit(SqlSelectIntoClause codeObject)
        {
        }

        public override void Visit(SqlSearchedWhenClause codeObject)
        {
        }

        public override void Visit(SqlSearchedCaseExpression codeObject)
        {
        }

        public override void Visit(SqlScript codeObject)
        {
            var statements = codeObject.Batches
                .SelectMany(batch => batch.Statements)
                .ToList();

            for (var i = 0; i < statements.Count; i++)
            {
                if (i > 0)
                {
                    _stringBuilder
                        .AppendIndentedLine()
                        .AppendIndentedLine();
                }

                statements[i].Accept(this);
            }
        }

        public override void Visit(SqlScalarVariableRefExpression codeObject)
        {
            _stringBuilder.Append(codeObject.VariableName);
        }

        public override void Visit(SqlScalarVariableAssignment codeObject)
        {
            codeObject.Variable.Accept(this);

            _stringBuilder.Append(" = ");

            codeObject.Value.Accept(this);
        }

        public override void Visit(SqlScalarSubQueryExpression codeObject)
        {
            _stringBuilder
                .AppendIndentedLine()
                .Append("(");

            using (_stringBuilder.CreateIndentationContext())
            {
                _stringBuilder.AppendIndentedLine();

                codeObject.QueryExpression.Accept(this);
            }

            _stringBuilder
                .AppendIndentedLine()
                .Append(")");
        }

        public override void Visit(SqlSelectClause codeObject)
        {
            _stringBuilder.Append("SELECT");
            
            var children = codeObject.Children.ToList();

            if (children.Count == 1 && children[0] is SqlSelectStarExpression selectStarExpression)
            {
                _stringBuilder.Append(" ");

                selectStarExpression.Accept(this);
            }
            else
            {
                using (_stringBuilder.CreateIndentationContext())
                {
                    for (var i = 0; i < children.Count; i++)
                    {
                        if (i > 0)
                        {
                            _stringBuilder.Append(",");
                        }

                        _stringBuilder.AppendIndentedLine();

                        children[i].Accept(this);
                    }
                }
            }
        }

        public override void Visit(SqlTableVariableRefExpression codeObject)
        {
        }

        public override void Visit(SqlSimpleGroupByItem codeObject)
        {
        }

        public override void Visit(SqlSimpleOrderByItem codeObject)
        {
        }

        public override void Visit(SqlTableRefExpression codeObject)
        {
            _stringBuilder.Append(codeObject.Sql);
        }

        public override void Visit(SqlTableHint codeObject)
        {
        }

        public override void Visit(SqlTableFunctionReturnType codeObject)
        {
        }

        public override void Visit(SqlTableDefinition codeObject)
        {
        }

        public override void Visit(SqlTableConstructorInsertSource codeObject)
        {
        }

        public override void Visit(SqlTableConstructorExpression codeObject)
        {
        }

        public override void Visit(SqlSimpleOrderByClause codeObject)
        {
        }

        public override void Visit(SqlTableClrFunctionDefinition codeObject)
        {
        }

        public override void Visit(SqlStatisticsNoRecomputeIndexOption codeObject)
        {
        }

        public override void Visit(SqlStatisticsIncrementalIndexOption codeObject)
        {
        }

        public override void Visit(SqlSortInTempDbIndexOption codeObject)
        {
        }

        public override void Visit(SqlSortedDataReorgIndexOption codeObject)
        {
        }

        public override void Visit(SqlSortedDataIndexOption codeObject)
        {
        }

        public override void Visit(SqlSimpleWhenClause codeObject)
        {
        }

        public override void Visit(SqlStatisticsOnlyIndexOption codeObject)
        {
        }

        public override void Visit(SqlBackupServiceMasterKeyStatement codeObject)
        {
        }

        public override void Visit(SqlBackupTableStatement codeObject)
        {
        }

        public override void Visit(SqlBreakStatement codeObject)
        {
        }

        public override void Visit(SqlInlineTableVariableDeclareStatement codeObject)
        {
        }

        public override void Visit(SqlIfElseStatement codeObject)
        {
        }

        public override void Visit(SqlGrantStatement codeObject)
        {
        }

        public override void Visit(SqlExecuteStringStatement codeObject)
        {
        }

        public override void Visit(SqlExecuteModuleStatement codeObject)
        {
        }

        public override void Visit(SqlDropViewStatement codeObject)
        {
        }

        public override void Visit(SqlInsertStatement codeObject)
        {
        }

        public override void Visit(SqlDropUserStatement codeObject)
        {
        }

        public override void Visit(SqlDropTriggerStatement codeObject)
        {
        }

        public override void Visit(SqlDropTableStatement codeObject)
        {
        }

        public override void Visit(SqlDropSynonymStatement codeObject)
        {
        }

        public override void Visit(SqlDropSequenceStatement codeObject)
        {
        }

        public override void Visit(SqlDropSecurityPolicyStatement codeObject)
        {
        }

        public override void Visit(SqlDropSchemaStatement codeObject)
        {
        }

        public override void Visit(SqlDropTypeStatement codeObject)
        {
        }

        public override void Visit(SqlDropRuleStatement codeObject)
        {
        }

        public override void Visit(SqlMergeStatement codeObject)
        {
        }

        public override void Visit(SqlRestoreDatabaseStatement codeObject)
        {
        }

        public override void Visit(SqlVariableDeclareStatement codeObject)
        {
            var isFirst = true;
            foreach (var variableDeclaration in codeObject.Declarations)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    _stringBuilder.AppendIndentedLine();
                }

                _stringBuilder
                    .Append($"DECLARE {variableDeclaration.Name} ");

                variableDeclaration.Type.Accept(this);

                if (variableDeclaration.Value != null)
                {
                    _stringBuilder.Append(" = ");

                    variableDeclaration.Value.Accept(this);
                }

                _stringBuilder.Append(";");
            }
        }

        public override void Visit(SqlUseStatement codeObject)
        {
        }

        public override void Visit(SqlUpdateStatement codeObject)
        {
            _stringBuilder.Append("UPDATE");

            using (_stringBuilder.CreateIndentationContext())
            {
                _stringBuilder.AppendIndentedLine();

                codeObject.UpdateSpecification.Target.Accept(this);
            }

            if (codeObject.UpdateSpecification.SetClause != null)
            {
                _stringBuilder
                    .AppendIndentedLine()
                    .Append("SET");
         
                using (_stringBuilder.CreateIndentationContext())
                {
                    _stringBuilder.AppendIndentedLine();

                    codeObject.UpdateSpecification.SetClause.Accept(this);
                }
            }

            if (codeObject.UpdateSpecification.OutputClause != null)
            {
                _stringBuilder.AppendIndentedLine();

                codeObject.UpdateSpecification.OutputClause.Accept(this);
            }

            if (codeObject.UpdateSpecification.FromClause != null)
            {
                _stringBuilder
                    .AppendIndentedLine();

                codeObject.UpdateSpecification.FromClause.Accept(this);
            }

            if (codeObject.UpdateSpecification.WhereClause != null)
            {
                _stringBuilder
                    .AppendIndentedLine();

                codeObject.UpdateSpecification.WhereClause.Accept(this);
            }
        }

        public override void Visit(SqlTryCatchStatement codeObject)
        {
        }

        public override void Visit(SqlSetStatement codeObject)
        {
        }

        public override void Visit(SqlSetAssignmentStatement codeObject)
        {
        }

        public override void Visit(SqlSelectStatement codeObject)
        {
            codeObject.SelectSpecification.QueryExpression.Accept(this);
            _stringBuilder.Append(";");
        }

        public override void Visit(SqlReturnStatement codeObject)
        {
        }

        public override void Visit(SqlRestoreTableStatement codeObject)
        {
        }

        public override void Visit(SqlRestoreServiceMasterKeyStatement codeObject)
        {
        }

        public override void Visit(SqlRestoreMasterKeyStatement codeObject)
        {
        }

        public override void Visit(SqlRestoreLogStatement codeObject)
        {
        }

        public override void Visit(SqlRestoreInformationStatement codeObject)
        {
        }

        public override void Visit(SqlRevokeStatement codeObject)
        {
        }

        public override void Visit(SqlDropProcedureStatement codeObject)
        {
        }

        public override void Visit(SqlDropLoginStatement codeObject)
        {
        }

        public override void Visit(SqlDropFunctionStatement codeObject)
        {
        }

        public override void Visit(SqlCreateTableStatement codeObject)
        {
        }

        public override void Visit(SqlCreateSynonymStatement codeObject)
        {
        }

        public override void Visit(SqlCreateSchemaStatement codeObject)
        {
        }

        public override void Visit(SqlCreateRoleStatement codeObject)
        {
        }

        public override void Visit(SqlCreateProcedureStatement codeObject)
        {
        }

        public override void Visit(SqlCreateLoginWithPasswordStatement codeObject)
        {
        }

        public override void Visit(SqlCreateTriggerStatement codeObject)
        {
        }

        public override void Visit(SqlCreateLoginFromWindowsStatement codeObject)
        {
        }

        public override void Visit(SqlCreateLoginFromAsymKeyStatement codeObject)
        {
        }

        public override void Visit(SqlCreateIndexStatement codeObject)
        {
        }

        public override void Visit(SqlCreateFunctionStatement codeObject)
        {
        }

        public override void Visit(SqlContinueStatement codeObject)
        {
        }

        public override void Visit(SqlCompoundStatement codeObject)
        {
        }

        public override void Visit(SqlCommentStatement codeObject)
        {
        }

        public override void Visit(SqlCreateLoginFromCertificateStatement codeObject)
        {
        }

        public override void Visit(SqlCreateUserDefinedDataTypeStatement codeObject)
        {
        }

        public override void Visit(SqlCreateUserDefinedTableTypeStatement codeObject)
        {
        }

        public override void Visit(SqlCreateUserDefinedTypeStatement codeObject)
        {
        }

        public override void Visit(SqlDropDefaultStatement codeObject)
        {
        }

        public override void Visit(SqlDropDatabaseStatement codeObject)
        {
        }

        public override void Visit(SqlDropAggregateStatement codeObject)
        {
        }

        public override void Visit(SqlDenyStatement codeObject)
        {
        }

        public override void Visit(SqlDeleteStatement codeObject)
        {
        }

        public override void Visit(SqlDBCCStatement codeObject)
        {
        }

        public override void Visit(SqlCursorDeclareStatement codeObject)
        {
        }

        public override void Visit(SqlCreateViewStatement codeObject)
        {
        }

        public override void Visit(SqlCreateUserWithoutLoginStatement codeObject)
        {
        }

        public override void Visit(SqlCreateUserStatement codeObject)
        {
        }

        public override void Visit(SqlCreateUserFromExternalProviderStatement codeObject)
        {
        }

        public override void Visit(SqlCreateUserFromLoginStatement codeObject)
        {
        }

        public override void Visit(SqlCreateUserWithImplicitAuthenticationStatement codeObject)
        {
        }

        public override void Visit(SqlCreateUserFromCertificateStatement codeObject)
        {
        }

        public override void Visit(SqlCreateUserFromAsymKeyStatement codeObject)
        {
        }

        public override void Visit(SqlScalarRelationalFunctionDefinition codeObject)
        {
        }

        public override void Visit(SqlWhileStatement codeObject)
        {
        }

        public override void Visit(SqlScalarRefExpression codeObject)
        {
            _stringBuilder.Append(codeObject.Sql);
        }

        public override void Visit(SqlScalarClrFunctionDefinition codeObject)
        {
        }

        public override void Visit(SqlDmlSpecificationTableSource codeObject)
        {
        }

        public override void Visit(SqlDerivedTableExpression codeObject)
        {
            _stringBuilder.Append("(");

            using (_stringBuilder.CreateIndentationContext())
            {
                _stringBuilder.AppendIndentedLine();

                codeObject.QueryExpression.Accept(this);
            }

            _stringBuilder
                .AppendIndentedLine()
                .Append(")");
        }

        public override void Visit(SqlDeleteSpecification codeObject)
        {
        }

        public override void Visit(SqlDeleteMergeAction codeObject)
        {
        }

        public override void Visit(SqlDefaultValuesInsertSource codeObject)
        {
        }

        public override void Visit(SqlDefaultValuesInsertMergeActionSource codeObject)
        {
        }

        public override void Visit(SqlDmlTriggerDefinition codeObject)
        {
        }

        public override void Visit(SqlDdlTriggerDefinition codeObject)
        {
        }

        public override void Visit(SqlDataType codeObject)
        {
        }

        public override void Visit(SqlDataCompressionIndexOption codeObject)
        {
        }

        public override void Visit(SqlCursorVariableRefExpression codeObject)
        {
        }

        public override void Visit(SqlCursorVariableAssignment codeObject)
        {
        }

        public override void Visit(SqlCursorOption codeObject)
        {
        }

        public override void Visit(SqlCubeGroupByItem codeObject)
        {
        }

        public override void Visit(SqlDataTypeSpecification codeObject)
        {
            _stringBuilder.Append(codeObject.Sql.ToUpper());
        }

        public override void Visit(SqlCreateUserOption codeObject)
        {
        }

        public override void Visit(SqlDropExistingIndexOption codeObject)
        {
        }

        public override void Visit(SqlExecuteAsClause codeObject)
        {
        }

        public override void Visit(SqlFullTextColumn codeObject)
        {
        }

        public override void Visit(SqlFullTextBooleanExpression codeObject)
        {
        }

        public override void Visit(SqlFromClause codeObject)
        {
            _stringBuilder.Append("FROM");

            var children = codeObject.Children.ToList();

            for (var i = 0; i < children.Count; i++)
            {
                if (i > 0)
                {
                    _stringBuilder.Append(",");
                }

                var child = children[i];

                if (child is SqlDerivedTableExpression)
                {
                    _stringBuilder.AppendLine();

                    child.Accept(this);
                }
                else
                {
                    using (_stringBuilder.CreateIndentationContext())
                    {
                        _stringBuilder.AppendIndentedLine();

                        child.Accept(this);
                    }
                }
            }
        }

        public override void Visit(SqlForXmlRawClause codeObject)
        {
        }

        public override void Visit(SqlForXmlPathClause codeObject)
        {
        }

        public override void Visit(SqlForXmlExplicitClause codeObject)
        {
        }

        public override void Visit(SqlExecuteArgument codeObject)
        {
        }

        public override void Visit(SqlForXmlDirective codeObject)
        {
        }

        public override void Visit(SqlForXmlAutoClause codeObject)
        {
        }

        public override void Visit(SqlForeignKeyConstraint codeObject)
        {
        }

        public override void Visit(SqlForBrowseClause codeObject)
        {
        }

        public override void Visit(SqlFilterClause codeObject)
        {
        }

        public override void Visit(SqlFillFactorIndexOption codeObject)
        {
        }

        public override void Visit(SqlExistsBooleanExpression codeObject)
        {
        }

        public override void Visit(SqlForXmlClause codeObject)
        {
        }

        public override void Visit(SqlFunctionDefinition codeObject)
        {
        }

        public override void Visit(SqlConvertExpression codeObject)
        {
        }

        public override void Visit(SqlConditionClause codeObject)
        {
        }

        public override void Visit(SqlBuiltinScalarFunctionCallExpression codeObject)
        {
            _stringBuilder.Append($"{codeObject.FunctionName.ToUpper()}(");

            if (codeObject.Arguments != null)
            {
                for (var i = 0; i < codeObject.Arguments.Count; i++)
                {
                    if (i > 0)
                    {
                        _stringBuilder.Append(", ");
                    }

                    codeObject.Arguments[i].Accept(this);
                }
            }

            _stringBuilder.Append(")");
        }

        public override void Visit(SqlBooleanFilterExpression codeObject)
        {
        }

        public override void Visit(SqlBooleanExpression codeObject)
        {
        }

        public override void Visit(SqlBinaryScalarExpression codeObject)
        {
            codeObject.Left.Accept(this);

            _stringBuilder.Append($" {codeObject.Operator.GetStringRepresentation()} ");

            codeObject.Right.Accept(this);
        }

        public override void Visit(SqlBinaryQueryExpression codeObject)
        {
        }

        public override void Visit(SqlBinaryFilterExpression codeObject)
        {
        }

        public override void Visit(SqlCastExpression codeObject)
        {
        }

        public override void Visit(SqlBinaryBooleanExpression codeObject)
        {
        }

        public override void Visit(SqlBatch codeObject)
        {
        }

        public override void Visit(SqlAtTimeZoneExpression codeObject)
        {
        }

        public override void Visit(SqlAssignment codeObject)
        {
        }

        public override void Visit(SqlAllowRowLocksIndexOption codeObject)
        {
        }

        public override void Visit(SqlAllowPageLocksIndexOption codeObject)
        {
        }

        public override void Visit(SqlAllAnyComparisonBooleanExpression codeObject)
        {
        }

        public override void Visit(SqlBetweenBooleanExpression codeObject)
        {
        }

        public override void Visit(SqlConstraint codeObject)
        {
        }

        public override void Visit(SqlChangeTrackingContext codeObject)
        {
        }

        public override void Visit(SqlClrAssemblySpecifier codeObject)
        {
        }

        public override void Visit(SqlComputedColumnDefinition codeObject)
        {
        }

        public override void Visit(SqlCompressionPartitionRange codeObject)
        {
        }

        public override void Visit(SqlComparisonBooleanExpression codeObject)
        {
            codeObject.Left.Accept(this);

            _stringBuilder.Append(" = ");

            codeObject.Right.Accept(this);
        }

        public override void Visit(SqlCommonTableExpression codeObject)
        {
        }

        public override void Visit(SqlColumnRefExpression codeObject)
        {
            _stringBuilder.Append(codeObject.ColumnName.Value);
        }

        public override void Visit(SqlColumnIdentity codeObject)
        {
        }

        public override void Visit(SqlCheckConstraint codeObject)
        {
        }

        public override void Visit(SqlColumnDefinition codeObject)
        {
        }

        public override void Visit(SqlColumnAssignment codeObject)
        {
        }

        public override void Visit(SqlCollation codeObject)
        {
        }

        public override void Visit(SqlCollateScalarExpression codeObject)
        {
        }

        public override void Visit(SqlClrMethodSpecifier codeObject)
        {
        }

        public override void Visit(SqlClrFunctionBodyDefinition codeObject)
        {
        }

        public override void Visit(SqlClrClassSpecifier codeObject)
        {
        }

        public override void Visit(SqlDefaultConstraint codeObject)
        {
        }

        public override void Visit(SqlGlobalScalarVariableRefExpression codeObject)
        {
        }

        public override void Visit(SqlGrandTotalGroupByItem codeObject)
        {
        }

        public override void Visit(SqlGrandTotalGroupingSet codeObject)
        {
        }

        public override void Visit(SqlCompressionDelayIndexOption codeObject)
        {
        }

        public override void Visit(SqlBucketCountIndexOption codeObject)
        {
        }

        public override void Visit(SqlResumableIndexOption codeObject)
        {
        }

        public override void Visit(SqlOptimizeForSequentialKeyIndexOption codeObject)
        {
        }

        public override void Visit(SqlOnlineIndexOption codeObject)
        {
        }

        public override void Visit(SqlObjectReference codeObject)
        {
        }

        public override void Visit(SqlMaxDurationIndexOption codeObject)
        {
        }

        public override void Visit(SqlObjectIdentifier codeObject)
        {
            _stringBuilder.Append(codeObject.Sql);
        }

        public override void Visit(SqlScalarExpression codeObject)
        {
            // SqlParser does not work with coalesce
            // Must parse token list
            ParseTokens(codeObject.Tokens);
        }

        public override void Visit(SqlQueryExpression codeObject)
        {
        }

        public override void Visit(SqlNotBooleanExpression codeObject)
        {
        }

        public override void Visit(SqlMultistatementTableRelationalFunctionDefinition codeObject)
        {
        }

        public override void Visit(SqlMultistatementFunctionBodyDefinition codeObject)
        {
        }

        public override void Visit(SqlModuleViewMetadataOption codeObject)
        {
        }

        public override void Visit(SqlTableExpression codeObject)
        {
        }

        public override void Visit(SqlModuleSchemaBindingOption codeObject)
        {
        }

        public override void Visit(SqlOffsetFetchClause codeObject)
        {
        }

        public override void Visit(SqlOrderByItem codeObject)
        {
            codeObject.Expression.Accept(this);
        }

        public override void Visit(SqlRowConstructorExpression codeObject)
        {
        }

        public override void Visit(SqlRollupGroupByItem codeObject)
        {
        }

        public override void Visit(SqlQueryWithClause codeObject)
        {
        }

        public override void Visit(SqlQuerySpecification codeObject)
        {
            var children = codeObject.Children.ToList();

            for (var i = 0; i < children.Count; i++)
            {
                if (i > 0)
                {
                    _stringBuilder.AppendIndentedLine();
                }

                children[i].Accept(this);
            }
        }

        public override void Visit(SqlQualifiedJoinTableExpression codeObject)
        {
        }

        public override void Visit(SqlProcedureDefinition codeObject)
        {
        }

        public override void Visit(SqlOrderByClause codeObject)
        {
            _stringBuilder.Append("ORDER BY");

            using (_stringBuilder.CreateIndentationContext())
            {
                for (var i = 0; i < codeObject.Items.Count; i++)
                {
                    if (i > 0)
                    {
                        _stringBuilder.Append(",");
                    }

                    _stringBuilder.AppendIndentedLine();

                    codeObject.Items[i]
                        .Accept(this);
                }
            }
        }

        public override void Visit(SqlStorageSpecification codeObject)
        {
        }

        public override void Visit(SqlPivotTableExpression codeObject)
        {
        }

        public override void Visit(SqlPivotClause codeObject)
        {
        }

        public override void Visit(SqlParameterDeclaration codeObject)
        {
        }

        public override void Visit(SqlPadIndexOption codeObject)
        {
        }

        public override void Visit(SqlOutputIntoClause codeObject)
        {
        }

        public override void Visit(SqlOutputClause codeObject)
        {
            _stringBuilder.Append("OUTPUT");

            using (_stringBuilder.CreateIndentationContext())
            {
                for (var i = 0; i < codeObject.OutputExpressions.Count; i++)
                {
                    if (i > 0)
                    {
                        _stringBuilder.Append(",");
                    }

                    _stringBuilder
                        .AppendIndentedLine();

                    codeObject.OutputExpressions[i].Accept(this);
                }
            }
        }

        public override void Visit(SqlPrimaryKeyConstraint codeObject)
        {
        }

        public override void Visit(SqlModuleReturnsNullOnNullInputOption codeObject)
        {
        }

        public override void Visit(SqlModuleRecompileOption codeObject)
        {
        }

        public override void Visit(SqlModuleOption codeObject)
        {
        }

        public override void Visit(SqlInlineIndexConstraint codeObject)
        {
        }

        public override void Visit(SqlIndexOption codeObject)
        {
        }

        public override void Visit(SqlIndexHint codeObject)
        {
        }

        public override void Visit(SqlIndexedColumn codeObject)
        {
        }

        public override void Visit(SqlInBooleanExpressionQueryValue codeObject)
        {
        }

        public override void Visit(SqlInBooleanExpressionCollectionValue codeObject)
        {
        }

        public override void Visit(SqlInlineFunctionBodyDefinition codeObject)
        {
        }

        public override void Visit(SqlInBooleanExpression codeObject)
        {
        }

        public override void Visit(SqlIdentityFunctionCallExpression codeObject)
        {
        }

        public override void Visit(SqlIdentifier codeObject)
        {
            _stringBuilder.Append(codeObject.Value);
        }

        public override void Visit(SqlHavingClause codeObject)
        {
        }

        public override void Visit(SqlGroupingSetItemsCollection codeObject)
        {
        }

        public override void Visit(SqlGroupBySets codeObject)
        {
        }

        public override void Visit(SqlGroupByClause codeObject)
        {
        }

        public override void Visit(SqlIgnoreDupKeyIndexOption codeObject)
        {
        }

        public override void Visit(SqlInlineTableRelationalFunctionDefinition codeObject)
        {
        }

        public override void Visit(SqlInlineTableVariableDeclaration codeObject)
        {
        }

        public override void Visit(SqlInsertMergeAction codeObject)
        {
        }

        public override void Visit(SqlModuleNativeCompilationOption codeObject)
        {
        }

        public override void Visit(SqlModuleExecuteAsOption codeObject)
        {
        }

        public override void Visit(SqlModuleEncryptionOption codeObject)
        {
        }

        public override void Visit(SqlModuleCalledOnNullInputOption codeObject)
        {
        }

        public override void Visit(SqlInsertSource codeObject)
        {
        }

        public override void Visit(SqlMergeSpecification codeObject)
        {
        }

        public override void Visit(SqlMergeActionClause codeObject)
        {
        }

        public override void Visit(SqlMaxDegreeOfParallelismIndexOption codeObject)
        {
        }

        public override void Visit(SqlLoginPassword codeObject)
        {
        }

        public override void Visit(SqlLiteralExpression codeObject)
        {
            _stringBuilder.Append(codeObject.Sql);
        }

        public override void Visit(SqlLikeBooleanExpression codeObject)
        {
        }

        public override void Visit(SqlLargeDataStorageInformation codeObject)
        {
        }

        public override void Visit(SqlIsNullBooleanExpression codeObject)
        {
        }

        public override void Visit(SqlIntoClause codeObject)
        {
        }

        public override void Visit(SqlInsertSpecification codeObject)
        {
        }

        public override void Visit(SqlScalarFunctionReturnType codeObject)
        {
        }

        public override void Visit(SqlAggregateFunctionCallExpression codeObject)
        {
        }

        private void ParseTokens(IEnumerable<Token> codeObjectTokens)
        {
            var nonEmptyTokens = codeObjectTokens
                .Where(token => Tokens.LEX_WHITE != (Tokens) token.Id)
                .ToList();

            foreach (var token in nonEmptyTokens)
            {
                switch ((Tokens) token.Id)
                {
                    case (Tokens)40: // left parenthesis
                        _stringBuilder.Append("(");
                        break;

                    case (Tokens)41: // right parenthesis
                        _stringBuilder.Append(")");
                        break;

                    case (Tokens)44: // comma
                        _stringBuilder.Append(", ");
                        break;

                    case (Tokens)46: // period
                        _stringBuilder.Append(".");
                        break;

                    case Tokens.TOKEN_COALESCE:
                        _stringBuilder.Append("COALESCE");
                        break;

                    case Tokens.TOKEN_VARIABLE:
                        _stringBuilder.Append(token.Text);
                        break;

                    case Tokens.TOKEN_ID:
                        _stringBuilder.Append(token.Text);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
