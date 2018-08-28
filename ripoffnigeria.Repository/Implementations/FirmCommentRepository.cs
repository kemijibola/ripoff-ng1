using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class FirmCommentRepository : IFirmComment
    {
     RipOffContext _ctx;

   public FirmCommentRepository(RipOffContext ctx)
        {
            _ctx = ctx;                
            _ctx.Configuration.LazyLoadingEnabled = false;             
        }

        public RepositoryActionResult<FirmComment> Delete(int id)
        {
            try
            {
                var exp = _ctx.FirmComments.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.FirmComments.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<FirmComment>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<FirmComment>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<FirmComment>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public System.Linq.IQueryable<FirmComment> Get()
        {
            return _ctx.FirmComments;
        }

        public RepositoryActionResult<FirmComment> Insert(FirmComment t)
        {
            try
            {
                _ctx.FirmComments.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<FirmComment>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<FirmComment>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<FirmComment>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<FirmComment> Update(FirmComment t)
        {
            try
            {
                var existingData = _ctx.FirmComments.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<FirmComment>(t, RepositoryActionStatus.NotFound);
                }
              
                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.FirmComments.Attach(t);        
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<FirmComment>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<FirmComment>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<FirmComment>(null, RepositoryActionStatus.Error, ex);
            }
        }   
    }
}