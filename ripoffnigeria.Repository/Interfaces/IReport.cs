using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IReport
    {
        RepositoryActionResult<Report> Delete(int id);
        IQueryable<Report> Get();
        IQueryable<Report> GetFullReport();
        RepositoryActionResult<Report> Insert(Report t);
        RepositoryActionResult<Report> Update(Report t); 
    }
}