using Mst.Dexter.Factory;
using SI.Test.Entities;
using Simply.Crud;
using Simply.Crud.DatabaseExtensions;
using Simply.Data;
using Simply.Data.Database;
using Simply.Data.Interfaces;
using SimplyCrud_TestDb_MySql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace SI.EntityTestConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var logFileFormat = "info_#H#.log";
            var errorFileFormat = "error_#H#.log";
            var content = new byte[] { 89, 19, 12, 34, 56, 78, 74, 35, 10, 45, 67, 13 };
            var content_updated = new byte[] { 89, 19, 12, 34, 56, 78, 74, 35, 10, 45, 67, 13, 90, 123, 49, 58 };
            var personFile = new PersonalFile();
            personFile.FileName = "Some File";
            personFile.FileContent = new byte[] { 12, 34, 56, 78, 74, 35, 10, 45, 67, 13, 90, 123 };
            personFile.CreatedBy = 1;
            personFile.CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
            personFile.IsActive = 1;
            var connectionName = ConfigurationManager.AppSettings["connTypeName"];
            var connectionStringName = ConfigurationManager.AppSettings["connStringName"];
            IDbConnection sqlConn = DxConnectionFactory.Instance.GetConnection(connectionName);
            sqlConn.ConnectionString = ConfigurationManager.AppSettings[connectionStringName];
            ISimpleDatabase database = new SimpleDatabase(sqlConn);

            string sCrudMode = ConfigurationManager.AppSettings["crudMode"];
            int iCrudMode;
            int.TryParse(sCrudMode, out iCrudMode);

            string sEntityId = ConfigurationManager.AppSettings["entityId"];
            int entityId;
            int.TryParse(sEntityId, out entityId);

            int count = 10000;

            try
            {
                string s = ConfigurationManager.AppSettings["insertKeyCount"];
                int.TryParse(s, out count);

                if (count < 10)
                    count = 10;

                if (count > 1000000)
                    count = 1000000;
            }
            catch
            {
            }

            Stopwatch sw;
            object o;
            object[] objArr;
            long ticks = 0L;
            long msec = 0L;
            long result = 0L;

            bool isBulkMode = false;

            try
            {
                string s4 = ConfigurationManager.AppSettings["isBulkMode"];
                s4 = s4.Trim();
                isBulkMode = s4 == "1";
            }
            catch
            {
            }
            logFileFormat = logFileFormat.Replace("#H#", iCrudMode + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss_ffffff"));
            errorFileFormat = errorFileFormat.Replace("#H#", iCrudMode + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss_ffffff"));
            if (isBulkMode)
            {
                #region [ Bulk Crud ]

                try
                {
                    List<PersonalFile> files = new List<PersonalFile>();
                    sw = null;
                    sw = new Stopwatch();
                    objArr = new object[] { };

                    //insert
                    if (iCrudMode == 1)
                    {
                        for (int counter = 0; counter < count; counter++)
                        {
                            var p = new PersonalFile
                            {
                                CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff"),
                                FileName = "SomeFile",
                                FileContent = content,
                                CreatedBy = 1,
                                IsActive = 1//true
                            };
                            files.Add(p);
                        }

                        Console.WriteLine("Entity bulk insert started.");
                        sw.Start();
                        var executionResult = database.InsertAndGetId(files);
                        var objs = executionResult.ExecutionResult;
                        sw.Stop();
                    }/// update
                    else if (iCrudMode == 2)
                    {
                        for (int counter = 0; counter < count; counter++)
                        {
                            var p = new PersonalFile
                            {
                                Id = (long)(entityId + counter),
                                CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff"),
                                FileName = "SomeFile Updated " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffffff"),
                                FileContent = content_updated,
                                CreatedBy = 1,
                                IsActive = 1//true
                            };

                            files.Add(p);
                        }

                        sw.Start();
                        result = database.Update(files);// sqlConn.UpdateInternal(files);
                        sw.Stop();
                    }//delete
                    else if (iCrudMode == 3)
                    {
                        for (int counter = 0; counter < count; counter++)
                        {
                            files.Add(new PersonalFile { Id = (long)(entityId + counter) });
                        }

                        sw.Start();
                        result = database.Delete(files); // sqlConn.DeleteInternal(files);
                        sw.Stop();
                    }
                    Console.WriteLine($"Geçen Süre : {sw.ElapsedMilliseconds} ms");
                    Console.WriteLine($"Geçen Süre : {sw.ElapsedTicks} Ticks");
                    FileOperator.Instance.Write(
                        logFileFormat, new List<string>
                        {
                            $"Geçen Süre : {sw.ElapsedMilliseconds} ms",
                            $"Geçen Süre : {sw.ElapsedTicks} Ticks",
                            "-------------------------------------------------"
                        }, true);

                    int i = 0;
                    objArr.ToList().ForEach(x =>
                    {
                        Console.WriteLine($"Query Result of {i} : {x};");
                        FileOperator.Instance.Write(
                        logFileFormat, new List<string>
                        {
                            $"Query Result of {i} : {x};",
                            "-------------------------------------------------"
                        }, true);
                        i++;
                    });

                    Console.WriteLine("-------------------------------------------------");

                    msec += sw.ElapsedMilliseconds;
                    ticks += sw.ElapsedTicks;
                }
                finally
                {
                    database.Close();
                    //sqlConn.CloseIfNot();
                }

                #endregion [ Bulk Crud ]
            }
            else
            {
                #region [ One by one Crud  ]

                for (int counter = 0; counter < count; counter++)
                {
                    try
                    {
                        sw = null;
                        sw = new Stopwatch();
                        //insert
                        if (iCrudMode == 1)
                        {
                            personFile.CreationDate = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffffff");
                            sw.Start();
                            o = database.InsertAndGetId(personFile); // sqlConn.InsertAndGetIdInternal(personFile);
                            sw.Stop();
                        }//update
                        else if (iCrudMode == 2)
                        {
                            personFile.Id = (long)(entityId + counter);
                            personFile.CreationDate = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffffff");
                            personFile.FileName = "SomeFile Updated " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffffff");
                            personFile.FileContent =
                                        new byte[] { 89, 19, 12, 34, 56, 78, 74, 35, 10, 45, 67, 13, 90, 123, 49, 58 };

                            sw.Start();
                            o = database.Update(personFile); // sqlConn.UpdateInternal(personFile);
                            sw.Stop();
                        }//delete
                        else if (iCrudMode == 3)
                        {
                            personFile.Id = (long)(entityId + counter);
                            sw.Start();
                            o = database.Delete(personFile); // sqlConn.DeleteInternal(personFile);
                            sw.Stop();
                        }
                        else
                        {
                            break;
                        }

                        Console.WriteLine(o);
                        Console.WriteLine($"Geçen Süre : {sw.ElapsedMilliseconds} ms");
                        Console.WriteLine($"Geçen Süre : {sw.ElapsedTicks} Ticks");
                        Console.WriteLine("-------------------------------------------------");
                        FileOperator.Instance.Write(
                           logFileFormat, new List<string>
                            {
                               o?.ToString()??string.Empty,
                            $"Geçen Süre : {sw.ElapsedMilliseconds} ms",
                            $"Geçen Süre : {sw.ElapsedTicks} Ticks",
                            "-------------------------------------------------"
                            }, true);

                        msec += sw.ElapsedMilliseconds;
                        ticks += sw.ElapsedTicks;
                    }
                    catch (Exception e)
                    {
                        FileOperator.Instance.Write(
                            errorFileFormat, new List<string>
                            {
                            $"Message : {e.Message}",
                            $"StackTrace : {e.StackTrace}",
                            "-----------------------------------------------"
                            }, true);

                        if (e.InnerException != null)
                        {
                            FileOperator.Instance.Write(
                                errorFileFormat, new List<string>
                                {
                            $"Inner Message : {e.InnerException.Message}",
                            $"Inner StackTrace : {e.InnerException.StackTrace}",
                            "-----------------------------------------------"
                                }, true);
                        }
                        FileOperator.Instance.Write(
                           errorFileFormat, new List<string>
                           {"*****************************************"}, true);
                    }
                    finally
                    {
                        //if (sqlConn.State != ConnectionState.Closed)
                        //    sqlConn.Close();
                        database.Close();
                    }
                    System.Threading.Thread.Sleep(100);
                }

                Console.WriteLine($"Toplam Kayıt Sayısı : {count}");
                Console.WriteLine($"Toplam Süre : {msec} ms, Ortalama: {(double)msec / count}");
                Console.WriteLine($"Toplam Süre Tik : {ticks} tick, Ortalama: {(double)ticks / count}");
                FileOperator.Instance.Write(
                    logFileFormat, new List<string>
                    {
                            $"Toplam Kayıt Sayısı : {count}",
                            $"Toplam Süre : {msec} ms, Ortalama: {(double)msec / count}",
                            $"Toplam Süre Tik : {ticks} tick, Ortalama: {(double)ticks / count}"
                    }, true);

                #endregion [ One by one Crud  ]
            }

            bool getAllRecords = false;

            try
            {
                string s3 = ConfigurationManager.AppSettings["getAllRecords"];
                s3 = s3.Trim();
                getAllRecords = s3 == "1";
            }
            catch
            {
            }

            if (getAllRecords)
            {
                List<PersonalFile> files = new List<PersonalFile>();

                Stopwatch swh = new Stopwatch();
                // sqlConn.OpenIfNot();
                swh.Start();
                files = database.GetAll<PersonalFile>(); // sqlConn.GetAll<PersonalFile>();

                swh.Stop();
                Console.WriteLine("-------------------------------------------------");
                foreach (var file in files)
                {
                    Console.WriteLine($"Id: {file.Id}, FileName: {file.FileName}, CreatedBy: {file.CreatedBy}, CreationDate : {file.CreationDate}");
                }
                Console.WriteLine("-------------------------------------------------");
                FileOperator.Instance.Write(
                   logFileFormat, new List<string>
                   {
                       $"Toplam kayıt sayısı : {files.Count}, Geçen zaman(msec): {swh.ElapsedMilliseconds}, Geçen zaman(tick): {swh.ElapsedTicks}, Ortalama (msec): {(double)swh.ElapsedMilliseconds / files.Count}"
                   }, true);
                Console.WriteLine($"Toplam kayıt sayısı : {files.Count}, Geçen zaman(msec): {swh.ElapsedMilliseconds}, Geçen zaman(tick): {swh.ElapsedTicks}, Ortalama (msec): {(double)swh.ElapsedMilliseconds / files.Count}");
            }
            FileOperator.Instance.Write(
                   logFileFormat, new List<string>
                   {
                            "********************************************************"
                   }, true);

            Console.Read();
        }
    }
}