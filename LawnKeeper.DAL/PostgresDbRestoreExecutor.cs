using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawnKeeper.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LawnKeeper.DAL
{
    public class PostgresDbRestoreExecutor : IDbRestoreExecutor
    {
        private readonly LawnKeeperDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public PostgresDbRestoreExecutor(LawnKeeperDbContext context, IConfiguration configuration, ILogger<PostgresDbRestoreExecutor> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public void Backup()
        {
            Environment.SetEnvironmentVariable("PGPASSWORD", "rootservgeevich");
            Process.Start("C:\\Program Files\\PostgreSQL\\13\\bin\\pg_dump.exe",
                "-U postgres -d lawnkeeper -f C:\\Backup\\lawnkeeper.dump -Fc")
                ?.WaitForExit();
        }

        public bool RestoreLatest()
        {
            Environment.SetEnvironmentVariable("PGPASSWORD", "rootservgeevich");
            Process.Start("C:\\Program Files\\PostgreSQL\\13\\bin\\pg_restore.exe",
                "-d lawnkeeper -U postgres -C C:\\Backup\\lawnkeeper.dump")
                ?.WaitForExit();
            return true;
        }
    }
}