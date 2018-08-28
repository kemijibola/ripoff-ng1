using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IClient
    {
        RepositoryActionResult<Client> Delete(string id);
        IQueryable<Client> Get();
        IQueryable<Client> Get(string id);
        RepositoryActionResult<Client> Insert(Client t);
        RepositoryActionResult<Client> Update(Client t);
    }
}