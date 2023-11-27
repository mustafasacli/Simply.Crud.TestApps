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
using System.Reflection;
using Simply.Data.Interfaces;

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

            var field = whereClause.GetType()
                .GetFields(BindingFlags.NonPublic |
                         BindingFlags.Instance)
                .Where(f => f.FieldType == typeof(ISimpleDatabase) || (f.FieldType.GetInterfaces()?.Contains(typeof(ISimpleDatabase)) ?? false))
                .FirstOrDefault();

            ISimpleDatabase db = field.GetValue(whereClause) as ISimpleDatabase;
            Console.WriteLine(db.ToString());
            Console.ReadKey();
        }
    }
}
