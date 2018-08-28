using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface ICaseUpdate
    {
        RepositoryActionResult<CaseUpdate> Delete(int id);
        IQueryable<CaseUpdate> Get();
        IQueryable<CaseUpdate> Get(int id);
        RepositoryActionResult<CaseUpdate> Insert(CaseUpdate t);
        RepositoryActionResult<CaseUpdate> Update(CaseUpdate t);
    }
}