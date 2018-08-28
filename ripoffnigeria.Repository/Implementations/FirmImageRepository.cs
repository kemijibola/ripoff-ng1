using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class FirmImageRepository : IFirmImage
    {
       RipOffContext _ctx;

       public FirmImageRepository(RipOffContext ctx)
        {
            _ctx = ctx;                
            _ctx.Configuration.LazyLoadingEnabled = false;             
        }

        public RepositoryActionResult<FirmImage> Delete(int id)
        {
            try
            {
                var exp = _ctx.FirmImages.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.FirmImages.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<FirmImage>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<FirmImage>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<FirmImage>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public System.Linq.IQueryable<FirmImage> Get()
        {
            return _ctx.FirmImages;
        }

        public RepositoryActionResult<FirmImage> Insert(FirmImage t)
        {
            try
            {
                _ctx.FirmImages.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<FirmImage>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<FirmImage>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<FirmImage>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<FirmImage> Update(FirmImage t)
        {
            try
            {
                var existingData = _ctx.FirmImages.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<FirmImage>(t, RepositoryActionStatus.NotFound);
                }
              
                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.FirmImages.Attach(t);        
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<FirmImage>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<FirmImage>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<FirmImage>(null, RepositoryActionStatus.Error, ex);
            }
        }   
    }
}