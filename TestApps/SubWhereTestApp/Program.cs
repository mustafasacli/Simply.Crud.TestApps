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
            Console.WriteLine("---------------------------");

            var field = whereClause.GetType()
                .GetFields(BindingFlags.NonPublic |
                         BindingFlags.Instance)
                .Where(f => f.FieldType == typeof(ISimpleDatabase) || (f.FieldType.GetInterfaces()?.Contains(typeof(ISimpleDatabase)) ?? false))
                .FirstOrDefault();

            ISimpleDatabase db = field.GetValue(whereClause) as ISimpleDatabase;
            Console.WriteLine(db.ToString());
            Console.WriteLine("---------------------------");
            Console.ReadKey();

            var whereClause2 = postgreSqlDatabase.Where<User>(/*tableAliasInfo: TableAliasInfo.New("u")*/)
                 .AddAndCondition(a => a.IsActive == 1 && a.IsDeleted == 0)
                 .SubWhere(q => q.UserType, ConditionTypes.And,
                 SimpleSql.New("select ut2.id from user_types ut2 where ut2.is_active=?", new object[] { 1 })
                 ).AddAllPropertiesForSelect();

            var cmd2 = whereClause2.GetCommand();
            Console.WriteLine(cmd2.CommandText);
            Console.WriteLine("---------------------------");
            Console.ReadKey();

            var whereClause3 = postgreSqlDatabase.Where<User>(tableAliasInfo: TableAliasInfo.New("u3"))
                 .AddAndCondition(a => a.IsActive == 1 && a.IsDeleted == 0)
                 .SubWhere(SimpleSql.New("\"u3\".\"user_type\""), ConditionTypes.And,
                 // Sub Where Clause Start
                 postgreSqlDatabase.Where<UserType>(tableAliasInfo: TableAliasInfo.New("ut3"))
                 .AddAndCondition(q => q.IsActive == 1)
                 .AddPropertyForSelect(p => p.Id)
                 // Sub Where Clause End)
                 , isInOrNot: false
                 ).AddAllPropertiesForSelect();

            var cmd3 = whereClause3.GetCommand();
            Console.WriteLine(cmd3.CommandText);
            Console.WriteLine("---------------------------");
            Console.ReadKey();

            var whereClause4 = postgreSqlDatabase.Where<User>(tableAliasInfo: TableAliasInfo.New("u4"))
                 .AddAndCondition(a => a.IsActive == 1 && a.IsDeleted == 0)
                 .SubWhere(SimpleSql.New("\"u4\".\"user_type\""), ConditionTypes.And,
                 SimpleSql.New("select ut4.id from user_types ut4 where ut4.is_active=?", new object[] { 1 })
                 , isInOrNot: false
                 ).AddAllPropertiesForSelect();

            var cmd4 = whereClause4.GetCommand();
            Console.WriteLine(cmd4.CommandText);
            Console.WriteLine("---------------------------");
            Console.ReadKey();

            /*
            var whereClause3 = whereClause.Union(whereClause2);
            var cmd3 = whereClause3.GetCommand();
            Console.WriteLine(cmd3.CommandText);
            */
            Console.ReadKey();

        }
    }
}