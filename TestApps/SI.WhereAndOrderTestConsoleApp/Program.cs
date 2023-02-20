using Simply.Common;
using Simply.Crud.Condition;
using Simply.Crud.Enums;
using Simply.Crud.Interfaces;
using Simply.Crud.Objects;
using Simply.Data.Enums;
using Simply.Data.Objects;
using SI.Test.Entities;
using System;

namespace SI.WhereAndOrderTestConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ITableAliasInfo aliasInfo = TableAliasInfo.New(useTableName: true);
            var builder = WhereBuilderFactory.New<LogEntry>(DbConnectionTypes.MySql, tableAliasInfo: TableAliasInfo.New(aliasName: "Tbl", useTableName: false));
            builder.AddCondition(ConditionTypes.And, p => p.LogTime < DateTime.Today.AddDays(1.0) && p.LogTime >= DateTime.Today);
            builder.AddListContainsCondition(ConditionTypes.And, p => p.UserId, new int?[] { 3, 4, 5, 6 }, isInOrNot: true);
            var builder2 = WhereBuilderFactory.New<LogEntry>(DbConnectionTypes.MySql, tableAliasInfo: TableAliasInfo.New(aliasName: "Tbl", useTableName: false));
            builder2.AddCondition(ConditionTypes.And, p => p.ErrorCode == "Err14");
            string msg = null;
            builder2.AddCondition(ConditionTypes.And, p => p.Message == msg || msg == null);
            builder.AppendCondition(ConditionTypes.And, builder2);
            var cmdDef = builder.GetCommand();
            Console.WriteLine(cmdDef.CommandText);
            Console.WriteLine("**********************************************");
            int counter = 0;
            foreach (var parameter in cmdDef.CommandParameters)
            {
                var cmdParameter = parameter as DbCommandParameter;
                Console.WriteLine(counter + ". Parameter Name: " + cmdParameter.ParameterName);
                Console.WriteLine(counter + ". Parameter value: " + (cmdParameter.Value.IsNullOrDbNull() ? "*null*" : cmdParameter.Value.ToString()));
                counter++;
                Console.WriteLine("-------------------------------------------");
            }
            Console.ReadKey();
        }
    }
}