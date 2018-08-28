using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class FirmRegionRepository : IFirmRegion
    {
        RipOffContext _ctx;

        public FirmRegionRepository(RipOffContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = false;
        }

        public RepositoryActionResult<FirmRegion> Delete(int id)
        {
            try
            {
                var exp = _ctx.FirmRegions.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.FirmRegions.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<FirmRegion>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<FirmRegion>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<FirmRegion>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public System.Linq.IQueryable<FirmRegion> Get()
        {
            return _ctx.FirmRegions;
        }

        public RepositoryActionResult<FirmRegion> Insert(FirmRegion t)
        {
            try
            {
                _ctx.FirmRegions.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<FirmRegion>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<FirmRegion>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<FirmRegion>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<FirmRegion> Update(FirmRegion t)
        {
            try
            {
                var existingData = _ctx.FirmRegions.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<FirmRegion>(t, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.FirmRegions.Attach(t);
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<FirmRegion>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<FirmRegion>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<FirmRegion>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}