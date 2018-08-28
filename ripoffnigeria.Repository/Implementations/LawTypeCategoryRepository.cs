using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class LawTypeCategoryRepository : ILawTypeCategory
    {
       RipOffContext _ctx;

       public LawTypeCategoryRepository(RipOffContext ctx)
        {
            _ctx = ctx;                
            _ctx.Configuration.LazyLoadingEnabled = false;             
        }

       public RepositoryActionResult<LawTypeCategory> Delete(int id)
        {
            try
            {
                var exp = _ctx.LawTypeCategories.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.LawTypeCategories.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<LawTypeCategory>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<LawTypeCategory>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<LawTypeCategory>(null, RepositoryActionStatus.Error, ex);
            }
        }
       public System.Linq.IQueryable<LawTypeCategory> Get()
       {
           return _ctx.LawTypeCategories;
       }
       public System.Linq.IQueryable<LawTypeCategory> Get(int id)
        {
            return _ctx.LawTypeCategories.Where(d => d.Id == id);
        }

       public RepositoryActionResult<LawTypeCategory> Insert(LawTypeCategory t)
        {
            try
            {
                _ctx.LawTypeCategories.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<LawTypeCategory>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<LawTypeCategory>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<LawTypeCategory>(null, RepositoryActionStatus.Error, ex);
            }
        }

       public RepositoryActionResult<LawTypeCategory> Update(LawTypeCategory t)
        {
            try
            {
                var existingData = _ctx.LawTypeCategories.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<LawTypeCategory>(t, RepositoryActionStatus.NotFound);
                }
              
                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.LawTypeCategories.Attach(t);        
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<LawTypeCategory>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<LawTypeCategory>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<LawTypeCategory>(null, RepositoryActionStatus.Error, ex);
            }
        }   
    }
}