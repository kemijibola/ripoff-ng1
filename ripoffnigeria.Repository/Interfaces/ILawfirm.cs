using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface ILawfirm
    {
        RepositoryActionResult<LawFirm> Delete(int id);
        IQueryable<LawFirm> Get();
        RepositoryActionResult<LawFirm> Insert(LawFirm t);
        RepositoryActionResult<LawFirm> Update(LawFirm t);
    }
}