using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class TopicRepository:ITopic
    {
      RipOffContext _ctx;

      public TopicRepository(RipOffContext ctx)
        {
            _ctx = ctx;                
            _ctx.Configuration.LazyLoadingEnabled = false;             
        }

        public RepositoryActionResult<Topic> Delete(int id)
        {
            try
            {
                var exp = _ctx.Topics.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.Topics.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<Topic>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<Topic>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Topic>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public System.Linq.IQueryable<Topic> Get()
        {
            return _ctx.Topics;
        }

        public RepositoryActionResult<Topic> Insert(Topic t)
        {
            try
            {
                _ctx.Topics.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Topic>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Topic>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Topic>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Topic> Update(Topic t)
        {
            try
            {
                var existingData = _ctx.Topics.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<Topic>(t, RepositoryActionStatus.NotFound);
                }
              
                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.Topics.Attach(t);        
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Topic>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Topic>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Topic>(null, RepositoryActionStatus.Error, ex);
            }
        }   
    }
}