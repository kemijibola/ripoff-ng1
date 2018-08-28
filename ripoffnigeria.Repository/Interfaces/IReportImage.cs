using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IReportImage
    {
        RepositoryActionResult<ReportImage> Delete(int id);
        IQueryable<ReportImage> Get();
        RepositoryActionResult<ReportImage> Insert(ReportImage t);
        RepositoryActionResult<ReportImage> Update(ReportImage t); 
    }
}