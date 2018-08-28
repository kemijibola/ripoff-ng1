using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface ILawTypeCategory
    {
        RepositoryActionResult<LawTypeCategory> Delete(int id);
        IQueryable<LawTypeCategory> Get();
        IQueryable<LawTypeCategory> Get(int id);
        RepositoryActionResult<LawTypeCategory> Insert(LawTypeCategory t);
        RepositoryActionResult<LawTypeCategory> Update(LawTypeCategory t);   
    }
}