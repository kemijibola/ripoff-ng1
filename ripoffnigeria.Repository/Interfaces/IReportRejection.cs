using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IReportRejection
    {
        RepositoryActionResult<ReportRejection> Delete(int id);
        IQueryable<ReportRejection> Get();
        IQueryable<ReportRejection> Get(int id);
        RepositoryActionResult<ReportRejection> Insert(ReportRejection t);
        RepositoryActionResult<ReportRejection> Update(ReportRejection t);
    }
}