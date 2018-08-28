using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface ILawCategory
    {
        RepositoryActionResult<LawCategory> Delete(int id);
        IQueryable<LawCategory> Get(int id);
        IQueryable<LawCategory> Get();
        RepositoryActionResult<LawCategory> Insert(LawCategory t);
        RepositoryActionResult<LawCategory> Update(LawCategory t);   
    }
}