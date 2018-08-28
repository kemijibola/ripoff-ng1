using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class RebuttalImageRepository:IRebuttalImage
    {
         RipOffContext _ctx;

         public RebuttalImageRepository(RipOffContext ctx)
        {
            _ctx = ctx;                
            _ctx.Configuration.LazyLoadingEnabled = false;             
        }

         public RepositoryActionResult<RebuttalImage> Delete(int id)
        {
            try
            {
                var exp = _ctx.RebuttalImages.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.RebuttalImages.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<RebuttalImage>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<RebuttalImage>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<RebuttalImage>(null, RepositoryActionStatus.Error, ex);
            }
        }

         public System.Linq.IQueryable<RebuttalImage> Get()
        {
            return _ctx.RebuttalImages;
        }

         public RepositoryActionResult<RebuttalImage> Insert(RebuttalImage t)
        {
            try
            {
                _ctx.RebuttalImages.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<RebuttalImage>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<RebuttalImage>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<RebuttalImage>(null, RepositoryActionStatus.Error, ex);
            }
        }

         public RepositoryActionResult<RebuttalImage> Update(RebuttalImage t)
        {
            try
            {
                var existingData = _ctx.RebuttalImages.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<RebuttalImage>(t, RepositoryActionStatus.NotFound);
                }
              
                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.RebuttalImages.Attach(t);        
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<RebuttalImage>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<RebuttalImage>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<RebuttalImage>(null, RepositoryActionStatus.Error, ex);
            }
        }   
    }
}