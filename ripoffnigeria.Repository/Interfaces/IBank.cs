using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IBank
    {
        RepositoryActionResult<Bank> Delete(int id);
        IQueryable<Bank> Get();
        IQueryable<Bank> Get(int id);
        RepositoryActionResult<Bank> Insert(Bank t);
        RepositoryActionResult<Bank> Update(Bank t);
    }
}