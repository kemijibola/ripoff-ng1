using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class LocationTypeRepository:ILocationType
    {
        RipOffContext _ctx;

        public LocationTypeRepository(RipOffContext ctx)
        {
            _ctx = ctx;                
            _ctx.Configuration.LazyLoadingEnabled = false;             
        }

        public RepositoryActionResult<LocationType> Delete(int id)
        {
            try
            {
                var exp = _ctx.LocationTypes.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.LocationTypes.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<LocationType>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<LocationType>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<LocationType>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public System.Linq.IQueryable<LocationType> Get()
        {
            return _ctx.LocationTypes;
        }

        public RepositoryActionResult<LocationType> Insert(LocationType t)
        {
            try
            {
                _ctx.LocationTypes.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<LocationType>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<LocationType>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<LocationType>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<LocationType> Update(LocationType t)
        {
            try
            {
                var existingData = _ctx.LocationTypes.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<LocationType>(t, RepositoryActionStatus.NotFound);
                }
              
                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.LocationTypes.Attach(t);        
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<LocationType>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<LocationType>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<LocationType>(null, RepositoryActionStatus.Error, ex);
            }
        }   
    }
}