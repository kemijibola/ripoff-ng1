using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IReportBug
    {
        RepositoryActionResult<ReportBug> Delete(int id);
        IQueryable<ReportBug> Get();
        RepositoryActionResult<ReportBug> Insert(ReportBug t);
        RepositoryActionResult<ReportBug> Update(ReportBug t);
    }
}