using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class trackUserRepository: ItrackUser
    {
    RipOffContext _ctx;

    public trackUserRepository(RipOffContext ctx)
        {
            _ctx = ctx;                
            _ctx.Configuration.LazyLoadingEnabled = false;             
        }

        public RepositoryActionResult<trackUser> Delete(int id)
        {
            try
            {
                var exp = _ctx.trackUsers.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.trackUsers.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<trackUser>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<trackUser>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<trackUser>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public System.Linq.IQueryable<trackUser> Get()
        {
            return _ctx.trackUsers;
        }

        public RepositoryActionResult<trackUser> Insert(trackUser t)
        {
            try
            {
                _ctx.trackUsers.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<trackUser>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<trackUser>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<trackUser>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<trackUser> Update(trackUser t)
        {
            try
            {
                var existingData = _ctx.trackUsers.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<trackUser>(t, RepositoryActionStatus.NotFound);
                }
              
                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.trackUsers.Attach(t);        
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<trackUser>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<trackUser>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<trackUser>(null, RepositoryActionStatus.Error, ex);
            }
        }   
    }
}