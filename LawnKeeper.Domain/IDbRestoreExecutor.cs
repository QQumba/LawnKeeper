using System.Threading.Tasks;

namespace LawnKeeper.Domain
{
    public interface IDbRestoreExecutor
    {
        void Backup();
        bool RestoreLatest();
    }
}