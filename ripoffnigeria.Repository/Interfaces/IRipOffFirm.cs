using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IRipOffFirm
    {

        RepositoryActionResult<RipOffFirm> Delete(int id);
        IQueryable<RipOffFirm> Get();
        IQueryable<RipOffFirm> GetFirm();
        RepositoryActionResult<RipOffFirm> Insert(RipOffFirm t);
        RepositoryActionResult<RipOffFirm> Update(RipOffFirm t);
    }
}