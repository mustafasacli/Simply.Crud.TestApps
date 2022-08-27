using Simply.Crud;
using Simply.Crud.Condition;
using Simply.Crud.Enums;
using Simply.Crud.Interfaces;
using Simply.Crud.Join;
using Simply.Crud.Objects;
using Simply.Data.Objects;
using Simply.Data.QuerySettings;
using SI.Test.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace SI.EntityPredicateTestConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var connectionName = ConfigurationManager.AppSettings["connTypeName"];
            var connectionStringName = ConfigurationManager.AppSettings["connStringName"];
            //IDbConnection sqlConn = DxConnectionFactory.Instance.GetConnection(connectionName);
            //sqlConn.ConnectionString = ConfigurationManager.AppSettings[connectionStringName];
            ITableAliasInfo aliasInfo = TableAliasInfo.New(aliasName: "t1", useTableName: false);
            //var files = GetFiles(sqlConn, "2020-04-18-11-26-31-047900");
            //files = GetFiles(sqlConn, "2020-04-18-11-26-31-927932");
            //var query = Build(sqlConn, "2020-04-18-11-26-31-047900");
            //Console.WriteLine("First Command: " + query.CommandText);
            //var usrTyp = 12;

            //PersonalFile file = new PersonalFile();
            //Expression<Func<PersonalFile, object>> expression=new Expression<Func<PersonalFile, object>> { }

            //var names = file.GetMemberNames(ff => new { ff.Id, ff.FileName, ff.FileContent });
            //query = sqlConn.BuildBaseCommand<User>(u => u.FirstName != null && u.UserType == usrTyp && u.IsActive == true &&
            //u.CreationDate > DateTime.Today.AddDays(-10.0), aliasInfo);
            //Console.WriteLine("First Command: " + query.CommandText);

            //var cmdDef = sqlConn.BuildCommandDefinition<User>(u => u.FirstName != null && u.UserType == usrTyp && u.IsActive == true &&
            // u.CreationDate > DateTime.Today.AddDays(-10.0), isOdbc: true);
            //Console.WriteLine(cmdDef.CommandText);

            //cmdDef = sqlConn.BuildCommandDefinition<User>(u => u.UserType == usrTyp && u.IsActive == true &&
            // u.CreationDate > DateTime.Today.AddDays(-10.0), isOdbc: false);
            //Console.WriteLine(cmdDef.CommandText);
            //query = Build(sqlConn, "2020-04-18-11-26-31-927932");

            var str = GetInfo((User u) => u.Id);
            Console.WriteLine(str);
            var builder = WhereBuilderFactory.New<User>(/*sqlConn.GetDbConnectionType()*/Simply.Data.Enums.DbConnectionTypes.Oracle, aliasInfo);
            /*builder
                .AddCondition(ConditionTypes.And, p => p.IsActive==true )
                .AddCondition(ConditionTypes.And, p => p.IsDeleted == false)
                .AddListContainsCondition(ConditionTypes.And, p => p.UserType, new int[] { 2, 4 })
                .AddAndCondition(u => !u.LastName.Contains("Ety"))
                .AddAndCondition(u => u.FirstName.Contains("Ali"))
                .AddAndCondition(u => u.FirstName.Substring(4, 3) == "Ali")
                .AddAndCondition(u => u.CreationDate < u.UpdateTime)
                .AddOrderClause(s => s.CreationDate)
                .AddPropertyForSelect(q => q.Id, "UserId")
                .AddPropertyForSelect(q => q.FirstName)
                .AddPropertyForSelect(q => q.LastName);*/

            builder
                .AddCondition(ConditionTypes.And, p => p.IsActive == 1 && p.IsDeleted == 0
                && !p.LastName.Contains("Ety") && p.CreationDate < p.UpdateTime && p.FirstName.Contains("Ali")
                && p.FirstName.Substring(4, 3) == "Ali")
                .AddListContainsCondition(ConditionTypes.And, p => p.UserType, new int[] { 2, 4 })

                //.AddAndCondition(p => p.FirstName.Contains("Ali"))
                //.AddAndCondition(p => p.FirstName.Substring(4, 3) == "Ali")
                //.AddAndCondition(u => u.CreationDate < u.UpdateTime)
                .AddOrderClause(s => s.CreationDate)
                .AddPropertyForSelect(q => q.Id, "UserId")
                .AddPropertyForSelect(q => q.FirstName)
                .AddPropertyForSelect(q => q.LastName);

            IJoinBuilder<User, UserType> joinBuilder = JoinBuilderFactory.BuildJoin<User, UserType>(JoinTypes.LeftJoin, QuerySettingsFactory.GetQuerySetting(Simply.Data.Enums.DbConnectionTypes.Oracle));
            joinBuilder.AddJoin(p => p.UserType, q => q.Id);
            var whereUserType = WhereBuilderFactory.New<UserType>(Simply.Data.Enums.DbConnectionTypes.Oracle)
                .AddPropertyForSelect(q => q.TypeName, columnAlias: "UserTypeName");
            builder = builder.AddJoin(joinBuilder, whereUserType);

            /**
             * SELECT t1.Id AS UserId, t1.FirstName, t1.LastName FROM public.user t1 WHERE ( ( ( (t1.IsActive = @p0) ) AND ( t1.UserType IN ( @p1, @p2 ) ) ) AND ( (FirstName LIKE '%'  +  @p3  +  '%' = @p4) ) ) AND ( (t1.FirstName = SUBSTR(FirstName, 4, 3) = @p5) )
             *
             */
            var cmdDefinition = builder.GetCommand(includeOrder: true);
            Console.Write(cmdDefinition.CommandText);
            Console.ReadKey();
        }

        public static List<PersonalFile> GetFiles(IDbConnection connection, string time)
        {
            var result = connection.Select<PersonalFile>(q => q.CreationDate == time && time != null && q.IsActive == 1);//time.ToString("yyyy-MM-dd HH:mm:ss"));
            return result.ToList();
        }

        public static BaseDbCommand Build(IDbConnection connection, string time)
        {
            //connection.PartialRead<PersonalFile>(p => p.Id == 10, q => new { q.CreatedBy, q.FileName, q.FileContent });
            var result = connection.BuildBaseCommand<PersonalFile>(q => q.CreationDate == time && time != null && q.IsActive == 1);//time.ToString("yyyy-MM-dd HH:mm:ss"));
            return result;
        }

        public static string GetInfo<T, P>(Expression<Func<T, P>> action) where T : class
        {
            var expression = (MemberExpression)action.Body;
            string name = expression.Member.Name;

            return name;
        }
    }
}