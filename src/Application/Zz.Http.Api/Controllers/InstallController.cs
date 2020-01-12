using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Engine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zz.Core.Data;
using Zz.Core.Exceptions;
using Zz.Http.Api.Models.Installation;
using Zz.Http.Core.Controllers;
using Zz.Services.Installers;

namespace Zz.Http.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Install")]
    public class InstallController : BaseController
    {
        #region Fields

        #endregion

        #region Ctor
        public InstallController()
        {

        }
        #endregion

        #region SqlServer Utilities
        protected virtual bool EnsureDatabaseExists(string connectionString)
        {
            /*
             * 使用循环验证连接是否成功
             * 有时创建时间过长，如果这时程序已经开始创建表和示例数据，这时会报错，因此需要确认创建完成。
             * 
             */

            const int maxConnectionTimes = 10;
            const int suspendedTimeout = 1000 * 2; // milliseconds

            for (int i = 0; i < maxConnectionTimes; i++)
            {
                if (SqlServerExists(connectionString))
                    return true;

                Thread.Sleep(suspendedTimeout);
            }
            return false;
        }

        protected virtual bool SqlServerExists(string connectionString)
        {
            try
            {
                // MySql
                //using (var connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
                using (var connection = new SqlConnection(connectionString))
                    connection.Open();
            }
            catch
            {
                return false;
            }
            return true;
        }

        protected virtual string CreateDatabase(string connectionString)
        {
            //创建数据库的思路：
            /*
             * 1、记录下要创建的数据库名称
             * 2、替换连接字符中数据库名称为"master"
             * 3、连接到"master"，（因为master数据库肯定存在）
             * 4、执行创建数据库语句（先使用数据库名称构造创建语句）
             */

            try
            {
                //var connectionBuilder = new MySql.Data.MySqlClient.MySqlConnectionStringBuilder(connectionString);
                var connectionBuilder = new SqlConnectionStringBuilder(connectionString);
                string commandText = $"CREATE DATABASE {connectionBuilder.InitialCatalog}";

                connectionBuilder.InitialCatalog = "master";
                using (var connection = new SqlConnection(connectionBuilder.ToString()))
                {
                    connection.Open();
                    using (var command = new SqlCommand(commandText, connection))
                        command.ExecuteNonQuery();
                }

                if (!EnsureDatabaseExists(connectionString))
                    return $"创建数据超时！";
            }
            catch (Exception ex)
            {
                return $"创建数据错误：{ex.Message}";
            }
            return string.Empty;
        }
        #endregion

        [Route("Plugins")]
        public IActionResult Plugins()
        {
            return SuccessMessage();
        }

        [Route("Install")]
        public IActionResult Install(InstallModel model)
        {
            var dbSettings = DbSettingsManager.Load();
            if (dbSettings != null && !string.IsNullOrEmpty(dbSettings.DbConnectionString))
                return SuccessMessage();

            // For Tests
            int databaseIndex = 111;
            model.DbConnectionString = $"Data Source=47.98.165.82;Initial Catalog=Surfing{databaseIndex};Persist Security Info=True;User ID=sa;Password=z;Connect Timeout=360";

            //MySql 
            //model.DbConnectionString = $"Data Source=10.0.0.7;Database=Surfing{databaseIndex};User ID=zeroing;Password=123456;pooling=true;port=3306;sslmode=none;CharSet=utf8;";

            model.CreateDatabaseIfNotExists = true;
            model.AdminEmail = "admin";
            model.AdminPassword = "123456";

            if (!ModelState.IsValid)
                return ErrorMessage();

            if (!SqlServerExists(model.DbConnectionString))
            {
                if (!model.CreateDatabaseIfNotExists)
                    throw new ZzException("数据库不存在！");

                var createDatabaseError = CreateDatabase(model.DbConnectionString);
                if (!string.IsNullOrEmpty(createDatabaseError))
                    throw new ZzException(createDatabaseError);
            }

            dbSettings.DbConnectionString = model.DbConnectionString;
            DbSettingsManager.Save(dbSettings);

            var _installerService = EngineContext.Resolve<IInstallerService>();
            _installerService.InitializeDatabase();
            _installerService.InstallManager(model.AdminEmail, model.AdminPassword);

            if (model.UseSampleData)
                _installerService.InitializeSampleData();

            // Database configuration set
            // 

            return SuccessMessage();
        }
    }
}