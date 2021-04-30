using System.Threading.Tasks;
using LawnKeeper.Domain;

namespace LawnKeeper.Services
{
    public class DatabaseBackupService
    {
        private readonly IDbRestoreExecutor _executor;

        public DatabaseBackupService(IDbRestoreExecutor executor)
        {
            _executor = executor;
        }

        public void Backup()
        {
            _executor.Backup();
        }

        public bool RestoreLatest()
        {
            return _executor.RestoreLatest();
        }
    }
}