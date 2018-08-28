using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class RejectionReasonRepository : IRejectionReason
    {
        RipOffContext _ctx;

        public RejectionReasonRepository(RipOffContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = false;
        }

        public RepositoryActionResult<RejectionReason> Delete(int id)
        {
            try
            {
                var exp = _ctx.Banks.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.Banks.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<RejectionReason>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<RejectionReason>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<RejectionReason>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public System.Linq.IQueryable<RejectionReason> Get()
        {
            return _ctx.RejectionReasons;
        }
        public System.Linq.IQueryable<RejectionReason> Get(int id)
        {
            return _ctx.RejectionReasons.Where(d => d.Id == id);
        }

        public RepositoryActionResult<RejectionReason> Insert(RejectionReason t)
        {
            try
            {
                _ctx.RejectionReasons.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<RejectionReason>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<RejectionReason>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<RejectionReason>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<RejectionReason> Update(RejectionReason t)
        {
            try
            {
                var existingData = _ctx.RejectionReasons.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<RejectionReason>(t, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.RejectionReasons.Attach(t);
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<RejectionReason>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<RejectionReason>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<RejectionReason>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}