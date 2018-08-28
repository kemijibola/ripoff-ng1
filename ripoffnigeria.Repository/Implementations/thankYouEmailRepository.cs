using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class thankYouEmailRepository : IthankYouEmail
    {
        RipOffContext _ctx;

        public thankYouEmailRepository(RipOffContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = false;
        }

        public RepositoryActionResult<thankYouEmail> Delete(int id)
        {
            try
            {
                var exp = _ctx.thankYouEmails.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.thankYouEmails.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<thankYouEmail>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<thankYouEmail>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<thankYouEmail>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public System.Linq.IQueryable<thankYouEmail> Get()
        {
            return _ctx.thankYouEmails;
        }

        public RepositoryActionResult<thankYouEmail> Insert(thankYouEmail t)
        {
            try
            {
                _ctx.thankYouEmails.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<thankYouEmail>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<thankYouEmail>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<thankYouEmail>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<thankYouEmail> Update(thankYouEmail t)
        {
            try
            {
                var existingData = _ctx.thankYouEmails.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<thankYouEmail>(t, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.thankYouEmails.Attach(t);
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<thankYouEmail>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<thankYouEmail>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<thankYouEmail>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}