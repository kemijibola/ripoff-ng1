using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class FeedbackRepository : IFeedback
    {
        RipOffContext _ctx;

        public FeedbackRepository(RipOffContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = false;
        }

        public RepositoryActionResult<Feedback> Delete(int id)
        {
            try
            {
                var exp = _ctx.Feedbacks.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.Feedbacks.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<Feedback>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<Feedback>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Feedback>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public System.Linq.IQueryable<Feedback> Get()
        {
            return _ctx.Feedbacks;
        }

        public RepositoryActionResult<Feedback> Insert(Feedback t)
        {
            try
            {
                _ctx.Feedbacks.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Feedback>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Feedback>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Feedback>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Feedback> Update(Feedback t)
        {
            try
            {
                var existingData = _ctx.Feedbacks.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<Feedback>(t, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.Feedbacks.Attach(t);
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Feedback>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Feedback>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Feedback>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}