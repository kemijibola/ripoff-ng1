using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class ReportImageRepository:IReportImage
    {
         RipOffContext _ctx;

         public ReportImageRepository(RipOffContext ctx)
        {
            _ctx = ctx;                
            _ctx.Configuration.LazyLoadingEnabled = false;             
        }

         public RepositoryActionResult<ReportImage> Delete(int id)
        {
            try
            {
                var exp = _ctx.ReportImages.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.ReportImages.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<ReportImage>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<ReportImage>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ReportImage>(null, RepositoryActionStatus.Error, ex);
            }
        }

         public System.Linq.IQueryable<ReportImage> Get()
        {
            return _ctx.ReportImages;
        }

        public RepositoryActionResult<ReportImage> Insert(ReportImage t)
        {
            try
            {
                _ctx.ReportImages.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<ReportImage>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<ReportImage>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ReportImage>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<ReportImage> Update(ReportImage t)
        {
            try
            {
                var existingData = _ctx.ReportImages.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<ReportImage>(t, RepositoryActionStatus.NotFound);
                }
              
                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.ReportImages.Attach(t);        
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<ReportImage>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<ReportImage>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ReportImage>(null, RepositoryActionStatus.Error, ex);
            }
        }   
    }
}