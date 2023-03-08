﻿using Simply.Data.Database;
using System.Data.SQLite;

namespace SimplyCrud_TestDb_SQLite
{
    public class SimpleSQLiteDatabase_PersonalFile : SimpleDatabase
    {
        public SimpleSQLiteDatabase_PersonalFile() : 
            base(Create<SQLiteConnection>(
                "Data Source=D:\\GitProjects\\SI.Crud.Extensions.Repo\\SI.Crud.Extensions.Repo\\DataSources\\Sqlite\\personalfile_sqlite_file_base_1.db;Version=3;"))
        {
        }
    }
}