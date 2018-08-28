using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface ICategory
    {
 
        RepositoryActionResult<Category> Delete(int id);
        IQueryable<Category> Get();
        RepositoryActionResult<Category> Insert(Category t);
        RepositoryActionResult<Category> Update(Category t);    
    }
}