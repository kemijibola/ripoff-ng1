using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IFirmCategory
    {
        RepositoryActionResult<FirmCategory> Delete(int id);
        IQueryable<FirmCategory> Get();
        RepositoryActionResult<FirmCategory> Insert(FirmCategory t);
        RepositoryActionResult<FirmCategory> Update(FirmCategory t);  
    }
}