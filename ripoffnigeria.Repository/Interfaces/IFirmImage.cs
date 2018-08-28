using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IFirmImage
    {
        RepositoryActionResult<FirmImage> Delete(int id);
        IQueryable<FirmImage> Get();
        RepositoryActionResult<FirmImage> Insert(FirmImage t);
        RepositoryActionResult<FirmImage> Update(FirmImage t);   
    }
}