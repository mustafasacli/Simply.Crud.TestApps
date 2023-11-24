using SI.Test.Entities;
using Simply.Crud.Enums;
using Simply.Crud;
using Simply.Crud.Condition;
using Simply.Data;
using SimplyCrud_TestDb_PostgreSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simply.Crud.Objects;

namespace SubWhereTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SimplePostgreSqlDatabase postgreSqlDatabase = new SimplePostgreSqlDatabase();
            var whereClause = postgreSqlDatabase.Where<User>(/*tableAliasInfo: TableAliasInfo.New("u")*/)
                 .AddAndCondition(a => a.IsActive == 1 && a.IsDeleted == 0)
                 .SubWhere(q => q.UserType, ConditionTypes.And,
                 // Sub Where Clause Start
                 postgreSqlDatabase.Where<UserType>(/*tableAliasInfo: TableAliasInfo.New("ut")*/)
                 .AddAndCondition(q => q.IsActive == 1)
                 .AddPropertyForSelect(p => p.Id)
                 // Sub Where Clause End
                 ).AddAllPropertiesForSelect();

            var cmd = whereClause.GetCommand();
            Console.WriteLine(cmd.CommandText);
            Console.ReadKey();
        }
    }
}
