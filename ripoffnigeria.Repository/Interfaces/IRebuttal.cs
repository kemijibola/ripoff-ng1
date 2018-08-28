using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IRebuttal
    {
        RepositoryActionResult<Rebuttal> Delete(int id);
        IQueryable<Rebuttal> Get();
        IQueryable<Rebuttal> GetByReportId(int reportId);
        RepositoryActionResult<Rebuttal> Insert(Rebuttal t);
        RepositoryActionResult<Rebuttal> Update(Rebuttal t); 
    }
}