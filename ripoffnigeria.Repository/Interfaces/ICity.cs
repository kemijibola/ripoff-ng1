using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface ICity
    {
        RepositoryActionResult<City> Delete(int id);
        IQueryable<City> Get();
        RepositoryActionResult<City> Insert(City t);
        RepositoryActionResult<City> Update(City t);  
    }
}