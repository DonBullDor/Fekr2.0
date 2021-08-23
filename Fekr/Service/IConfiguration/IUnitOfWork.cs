using Service.Interfaces;
using System.Threading.Tasks;

namespace Service.IConfiguration
{
    public interface IUnitOfWork
    {
        IUserRepository Etudiants { get; }
        Task CompleteAsync();
    }
}
