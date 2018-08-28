using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface ITransaction
    {
        RepositoryActionResult<Transaction> Delete(int id);
        IQueryable<Transaction> Get();
        IQueryable<Transaction> Get(int id);
        RepositoryActionResult<Transaction> Insert(Transaction t);
        RepositoryActionResult<Transaction> Update(Transaction t);
    }
}