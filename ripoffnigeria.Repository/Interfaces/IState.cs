using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IState
    {
        RepositoryActionResult<State> Delete(int id);
        IQueryable<State> Get();
        IQueryable<State> GetByCountry(int countryId);
        RepositoryActionResult<State> Insert(State t);
        RepositoryActionResult<State> Update(State t);   
    }
}