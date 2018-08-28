using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IFirmRegion
    {
        RepositoryActionResult<FirmRegion> Delete(int id);
        IQueryable<FirmRegion> Get();
        RepositoryActionResult<FirmRegion> Insert(FirmRegion t);
        RepositoryActionResult<FirmRegion> Update(FirmRegion t);
    }
}