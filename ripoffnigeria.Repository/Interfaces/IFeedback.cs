using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IFeedback
    {
        RepositoryActionResult<Feedback> Delete(int id);
        IQueryable<Feedback> Get();
        RepositoryActionResult<Feedback> Insert(Feedback t);
        RepositoryActionResult<Feedback> Update(Feedback t);
    }
}