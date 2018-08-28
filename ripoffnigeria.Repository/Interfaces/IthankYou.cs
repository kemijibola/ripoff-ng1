using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IthankYouEmail
    {
        RepositoryActionResult<thankYouEmail> Delete(int id);
        IQueryable<thankYouEmail> Get();
        RepositoryActionResult<thankYouEmail> Insert(thankYouEmail t);
        RepositoryActionResult<thankYouEmail> Update(thankYouEmail t);
    }
}