using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface ILocationType
    {
        RepositoryActionResult<LocationType> Delete(int id);
        IQueryable<LocationType> Get();
        RepositoryActionResult<LocationType> Insert(LocationType t);
        RepositoryActionResult<LocationType> Update(LocationType t); 
    }
}