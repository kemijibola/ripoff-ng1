using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class LawCategoryRepository : ILawCategory
    {
      RipOffContext _ctx;

      public LawCategoryRepository(RipOffContext ctx)
        {
            _ctx = ctx;                
            _ctx.Configuration.LazyLoadingEnabled = false;             
        }

        public RepositoryActionResult<LawCategory> Delete(int id)
        {
            try
            {
                var exp = _ctx.LawCategories.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.LawCategories.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<LawCategory>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<LawCategory>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<LawCategory>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public System.Linq.IQueryable<LawCategory> Get()
        {
            return _ctx.LawCategories;
        }

        public System.Linq.IQueryable<LawCategory> Get(int id)
        {
            return _ctx.LawCategories.Where(d=>d.Id == id);
        }

        public RepositoryActionResult<LawCategory> Insert(LawCategory t)
        {
            try
            {
                _ctx.LawCategories.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<LawCategory>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<LawCategory>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<LawCategory>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<LawCategory> Update(LawCategory t)
        {
            try
            {
                var existingData = _ctx.LawCategories.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<LawCategory>(t, RepositoryActionStatus.NotFound);
                }
              
                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.LawCategories.Attach(t);        
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<LawCategory>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<LawCategory>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<LawCategory>(null, RepositoryActionStatus.Error, ex);
            }
        }   
    }
}