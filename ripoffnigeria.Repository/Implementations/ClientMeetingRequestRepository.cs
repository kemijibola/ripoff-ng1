using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class ClientMeetingRequestRepository : IClientMeetingRequest
    {
        RipOffContext _ctx;

        public ClientMeetingRequestRepository(RipOffContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = false;
        }

        public RepositoryActionResult<ClientMeetingRequest> Delete(int id)
        {
            try
            {
                var exp = _ctx.ClientMeetingRequests.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.ClientMeetingRequests.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<ClientMeetingRequest>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<ClientMeetingRequest>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ClientMeetingRequest>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public System.Linq.IQueryable<ClientMeetingRequest> Get()
        {
            return _ctx.ClientMeetingRequests;
        }
        public System.Linq.IQueryable<ClientMeetingRequest> Get(int id)
        {
            return _ctx.ClientMeetingRequests.Where(d => d.Id == id);
        }
        public System.Linq.IQueryable<ClientMeetingRequest> getReportByUserIdInClientInitiation(string userId)
        {
            return _ctx.ClientMeetingRequests.Where(d => d.UserId == userId);
        }
        public RepositoryActionResult<ClientMeetingRequest> Insert(ClientMeetingRequest t)
        {
            try
            {
                _ctx.ClientMeetingRequests.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<ClientMeetingRequest>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<ClientMeetingRequest>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ClientMeetingRequest>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<ClientMeetingRequest> Update(ClientMeetingRequest t)
        {
            try
            {
                var existingData = _ctx.ClientMeetingRequests.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<ClientMeetingRequest>(t, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.ClientMeetingRequests.Attach(t);
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<ClientMeetingRequest>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<ClientMeetingRequest>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ClientMeetingRequest>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}