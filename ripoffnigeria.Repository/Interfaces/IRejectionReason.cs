using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IRejectionReason
    {
        RepositoryActionResult<RejectionReason> Delete(int id);
        IQueryable<RejectionReason> Get();
        IQueryable<RejectionReason> Get(int id);
        RepositoryActionResult<RejectionReason> Insert(RejectionReason t);
        RepositoryActionResult<RejectionReason> Update(RejectionReason t);
    }
}