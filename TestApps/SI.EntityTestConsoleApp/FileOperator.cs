﻿namespace SI.EntityTestConsoleApp
{

    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class FileOperator
    {
        private static readonly Lazy<FileOperator> lazyOp =
            new Lazy<FileOperator>(() => new FileOperator());

        private FileOperator()
        {
        }

        public static FileOperator Instance
        { get { return lazyOp.Value; } }

        public void Write(string filePath, List<string> rows, bool writeLine = false)
        {
            if (rows == null || rows.Count < 1)
                return;

            FileMode fileMode = File.Exists(filePath) ? FileMode.Append : FileMode.OpenOrCreate;

            using (StreamWriter writer = new StreamWriter(
                       new FileStream(filePath, fileMode))
            { AutoFlush = true })
            {
                if (!writeLine)
                {
                    string str;
                    rows.ForEach(s =>
                    {
                        str = s ?? string.Empty;
                        if (str.EndsWith("\r\n") || str.EndsWith("\n"))
                        {
                            writer.Write(str);
                        }
                        else
                        {
                            writer.WriteLine(str);
                        }
                    });
                }
                else
                {
                    rows.ForEach(s =>
                    {
                        writer.WriteLine(s ?? string.Empty);
                    });
                }
            }
        }
    }
}
