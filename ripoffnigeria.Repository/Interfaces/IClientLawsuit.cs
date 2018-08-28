using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IClientLawsuit
    {
        RepositoryActionResult<ClientLawsuit> Delete(int id);
        IQueryable<ClientLawsuit> Get();
        IQueryable<ClientLawsuit> Get(int id);
        RepositoryActionResult<ClientLawsuit> Insert(ClientLawsuit t);
        RepositoryActionResult<ClientLawsuit> Update(ClientLawsuit t);
    }
}