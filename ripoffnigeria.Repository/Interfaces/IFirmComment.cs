using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IFirmComment
    {

        RepositoryActionResult<FirmComment> Delete(int id);
        IQueryable<FirmComment> Get();
        RepositoryActionResult<FirmComment> Insert(FirmComment t);
        RepositoryActionResult<FirmComment> Update(FirmComment t);    
    }
}