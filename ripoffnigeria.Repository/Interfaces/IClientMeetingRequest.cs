using System.Linq;
using ripoffnigeria.DTO;

namespace ripoffnigeria.Repository.Interfaces
{
    public interface IClientMeetingRequest
    {
        RepositoryActionResult<ClientMeetingRequest> Delete(int id);
        IQueryable<ClientMeetingRequest> Get();
        IQueryable<ClientMeetingRequest> Get(int id);
        IQueryable<ClientMeetingRequest> getReportByUserIdInClientInitiation(string userId);
        RepositoryActionResult<ClientMeetingRequest> Insert(ClientMeetingRequest t);
        RepositoryActionResult<ClientMeetingRequest> Update(ClientMeetingRequest t);
    }
}