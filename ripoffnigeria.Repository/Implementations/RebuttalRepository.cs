using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class RebuttalRepository:IRebuttal
    {
        RipOffContext _ctx;

        public RebuttalRepository(RipOffContext ctx)
        {
            _ctx = ctx;                
            _ctx.Configuration.LazyLoadingEnabled = false;             
        }

        public RepositoryActionResult<Rebuttal> Delete(int id)
        {
            try
            {
                var exp = _ctx.RipOffLawyers.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.RipOffLawyers.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<Rebuttal>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<Rebuttal>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Rebuttal>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public System.Linq.IQueryable<Rebuttal> Get()
        {
            return _ctx.Rebuttals.Include("City").Include("State").Include("rebuttalImage");
        }
        public IQueryable<Rebuttal> GetByReportId(int reportId)
        {
            return _ctx.Rebuttals.Where(d => d.ReportId == reportId).Include("City").Include("State").Include("rebuttalImage");

        }
        public RepositoryActionResult<Rebuttal> Insert(Rebuttal t)
        {
            try
            {
                _ctx.Rebuttals.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Rebuttal>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Rebuttal>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Rebuttal>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Rebuttal> Update(Rebuttal t)
        {
            try
            {
                var existingData = _ctx.RipOffLawyers.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<Rebuttal>(t, RepositoryActionStatus.NotFound);
                }
              
                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.Rebuttals.Attach(t);        
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Rebuttal>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Rebuttal>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Rebuttal>(null, RepositoryActionStatus.Error, ex);
            }
        }   
    }
}