using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface ItrackUser
    {
        RepositoryActionResult<trackUser> Delete(int id);
        IQueryable<trackUser> Get();
        RepositoryActionResult<trackUser> Insert(trackUser t);
        RepositoryActionResult<trackUser> Update(trackUser t);  
    }
}