using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IRebuttalImage
    {
        RepositoryActionResult<RebuttalImage> Delete(int id);
        IQueryable<RebuttalImage> Get();
        RepositoryActionResult<RebuttalImage> Insert(RebuttalImage t);
        RepositoryActionResult<RebuttalImage> Update(RebuttalImage t); 
    }
}