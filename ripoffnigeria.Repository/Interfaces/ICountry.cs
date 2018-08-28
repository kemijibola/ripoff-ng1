using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface ICountry
    {
        RepositoryActionResult<Country> Delete(int id);
        IQueryable<Country> Get();
        RepositoryActionResult<Country> Insert(Country t);
        RepositoryActionResult<Country> Update(Country t);  
    }
}