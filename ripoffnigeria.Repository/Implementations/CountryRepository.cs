using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class CountryRepository:ICountry
    {
         RipOffContext _ctx;

          public CountryRepository(RipOffContext ctx)
        {
            _ctx = ctx;                
            _ctx.Configuration.LazyLoadingEnabled = false;             
        }

        public RepositoryActionResult<Country> Delete(int id)
        {
            try
            {
                var exp = _ctx.Countries.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.Countries.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<Country>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<Country>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Country>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public System.Linq.IQueryable<Country> Get()
        {
            return _ctx.Countries;
        }

        public RepositoryActionResult<Country> Insert(Country t)
        {
            try
            {
                _ctx.Countries.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Country>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Country>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Country>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Country> Update(Country t)
        {
            try
            {
                var existingData = _ctx.Countries.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<Country>(t, RepositoryActionStatus.NotFound);
                }
              
                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.Countries.Attach(t);        
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Country>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Country>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Country>(null, RepositoryActionStatus.Error, ex);
            }
        }   
    }
}