using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class CategoryRepository:ICategory
    {
        RipOffContext _ctx;

        public CategoryRepository(RipOffContext ctx)
        {
            _ctx = ctx;                
            _ctx.Configuration.LazyLoadingEnabled = false;             
        }

        public RepositoryActionResult<Category> Delete(int id)
        {
            try
            {
                var exp = _ctx.Categories.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.Categories.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<Category>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<Category>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Category>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public System.Linq.IQueryable<Category> Get()
        {
            return _ctx.Categories;
        }

        public RepositoryActionResult<Category> Insert(Category t)
        {
            try
            {
                _ctx.Categories.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Category>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Category>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Category>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Category> Update(Category t)
        {
            try
            {
                var existingData = _ctx.Categories.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<Category>(t, RepositoryActionStatus.NotFound);
                }
              
                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.Categories.Attach(t);        
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Category>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Category>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Category>(null, RepositoryActionStatus.Error, ex);
            }
        }   
    }
}