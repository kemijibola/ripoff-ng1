using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class ReportRepository:IReport
    {
         RipOffContext _ctx;

         public ReportRepository(RipOffContext ctx)
        {
            _ctx = ctx;                
            _ctx.Configuration.LazyLoadingEnabled = false;             
        }

         public RepositoryActionResult<Report> Delete(int id)
        {
            try
            {
                var exp = _ctx.Reports.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.Reports.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<Report>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<Report>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Report>(null, RepositoryActionStatus.Error, ex);
            }
        }
         public System.Linq.IQueryable<Report> GetFullReport()
         {
             return _ctx.Reports.Include("State").Include("City").Include("Topic").Include("Category").Include("LocationType").Include("Rebuttals").Include("ReportImages").Include("ClientLawsuits").Include("CaseUpdates");
         }
         public System.Linq.IQueryable<Report> Get()
        {
            return _ctx.Reports;
        }

         public RepositoryActionResult<Report> Insert(Report t)
        {
            try
            {
                _ctx.Reports.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Report>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Report>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Report>(null, RepositoryActionStatus.Error, ex);
            }
        }

         public RepositoryActionResult<Report> Update(Report t)
        {
            try
            {
                var existingData = _ctx.Reports.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<Report>(t, RepositoryActionStatus.NotFound);
                }
              
                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.Reports.Attach(t);        
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Report>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Report>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Report>(null, RepositoryActionStatus.Error, ex);
            }
        }   
    }
}