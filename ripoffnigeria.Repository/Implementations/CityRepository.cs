using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class CityRepository:ICity
    {
        RipOffContext _ctx;

        public CityRepository(RipOffContext ctx)
        {
            _ctx = ctx;                
            _ctx.Configuration.LazyLoadingEnabled = false;             
        }

        public RepositoryActionResult<City> Delete(int id)
        {
            try
            {
                var exp = _ctx.Cities.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.Cities.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<City>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<City>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<City>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public System.Linq.IQueryable<City> Get()
        {
            return _ctx.Cities;
        }

        public RepositoryActionResult<City> Insert(City t)
        {
            try
            {
                _ctx.Cities.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<City>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<City>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<City>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<City> Update(City t)
        {
            try
            {
                var existingData = _ctx.Cities.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<City>(t, RepositoryActionStatus.NotFound);
                }
              
                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.Cities.Attach(t);        
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<City>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<City>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<City>(null, RepositoryActionStatus.Error, ex);
            }
        }   
    }
}