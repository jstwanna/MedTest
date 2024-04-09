using DbUp;
using DbUp.Engine.Output;
using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MedTest.Database
{
    public class DBMigrations : IUpgradeLog
    {
        private static readonly Regex toFindScriptRegex = new Regex(@"^MedTest\.Database\.Migrations\..*.sql$");

        private const string MigrationTableName = "migrations";

        private readonly string _connectionString;

        public DBMigrations (string connStr)
        {
            _connectionString = connStr;
        }

        public void Apply ()
        {
            EnsureDatabase.For.PostgresqlDatabase(_connectionString, this);

            var upgrader = DeployChanges.To
                .PostgresqlDatabase(_connectionString)
                .JournalToPostgresqlTable("public", MigrationTableName)
                .WithScriptsAndCodeEmbeddedInAssembly(Assembly.GetExecutingAssembly(), toFindScriptRegex.IsMatch)
                .WithTransactionPerScript()
                .LogTo(this)
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                throw new Exception(result.ErrorScript.Name, result.Error);
            }
        }

        public void WriteInformation (string format, params object[] args)
        {

        }

        public void WriteError (string format, params object[] args)
        {

        }

        public void WriteWarning (string format, params object[] args)
        {

        }
    }
}