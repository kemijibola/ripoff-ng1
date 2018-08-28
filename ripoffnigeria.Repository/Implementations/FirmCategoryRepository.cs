using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class FirmCategoryRepository : IFirmCategory
    {
       RipOffContext _ctx;

       public FirmCategoryRepository(RipOffContext ctx)
        {
            _ctx = ctx;                
            _ctx.Configuration.LazyLoadingEnabled = false;             
        }

        public RepositoryActionResult<FirmCategory> Delete(int id)
        {
            try
            {
                var exp = _ctx.FirmCategories.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.FirmCategories.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<FirmCategory>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<FirmCategory>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<FirmCategory>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public System.Linq.IQueryable<FirmCategory> Get()
        {
            return _ctx.FirmCategories.Include("LawCategory");
        }

        public RepositoryActionResult<FirmCategory> Insert(FirmCategory t)
        {
            try
            {
                _ctx.FirmCategories.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<FirmCategory>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<FirmCategory>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<FirmCategory>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<FirmCategory> Update(FirmCategory t)
        {
            try
            {
                var existingData = _ctx.FirmCategories.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<FirmCategory>(t, RepositoryActionStatus.NotFound);
                }
              
                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.FirmCategories.Attach(t);        
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<FirmCategory>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<FirmCategory>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<FirmCategory>(null, RepositoryActionStatus.Error, ex);
            }
        }   
    }
}