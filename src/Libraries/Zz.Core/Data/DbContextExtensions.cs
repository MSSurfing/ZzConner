using Microsoft.EntityFrameworkCore.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Zz.Core.Data
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// 读取Sql语句集（按行读取遇到"GO"截止）
        /// </summary>
        /// <param name="reader">文件流</param>
        /// <returns></returns>
        private static string ReadNextStatementFromStream(StreamReader reader)
        {
            var sb = new StringBuilder();
            while (true)
            {
                var lineOfText = reader.ReadLine();
                if (lineOfText == null)
                {
                    if (sb.Length > 0)
                        return sb.ToString();

                    return null;
                }

                if (lineOfText.TrimEnd().ToUpper() == "GO")
                    break;

                sb.Append(lineOfText + Environment.NewLine);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 执行Sql文件
        /// </summary>
        /// <param name="path">文件路径</param>
        public static void ExecuteSqlFile(this IDbContext context, string path)
        {
            var statements = new System.Collections.Generic.List<string>();

            using (var stream = File.OpenRead(path))
            using (var reader = new StreamReader(stream))
            {
                string statement;
                while ((statement = ReadNextStatementFromStream(reader)) != null)
                    if (!string.IsNullOrWhiteSpace(statement))
                        statements.Add(statement);
            }

            foreach (string stmt in statements)
                context.ExecuteSqlCommand(stmt);
        }
    }
}
