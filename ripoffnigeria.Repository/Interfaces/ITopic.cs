using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface ITopic
    {
        RepositoryActionResult<Topic> Delete(int id);
        IQueryable<Topic> Get();
        RepositoryActionResult<Topic> Insert(Topic t);
        RepositoryActionResult<Topic> Update(Topic t);   
    }
}