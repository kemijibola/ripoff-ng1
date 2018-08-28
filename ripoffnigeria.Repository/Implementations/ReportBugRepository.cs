using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class ReportBugRepository : IReportBug
    {
        RipOffContext _ctx;

        public ReportBugRepository(RipOffContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = false;
        }

        public RepositoryActionResult<ReportBug> Delete(int id)
        {
            try
            {
                var exp = _ctx.ReportBugs.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.ReportBugs.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<ReportBug>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<ReportBug>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ReportBug>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public System.Linq.IQueryable<ReportBug> Get()
        {
            return _ctx.ReportBugs;
        }

        public RepositoryActionResult<ReportBug> Insert(ReportBug t)
        {
            try
            {
                _ctx.ReportBugs.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<ReportBug>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<ReportBug>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ReportBug>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<ReportBug> Update(ReportBug t)
        {
            try
            {
                var existingData = _ctx.Feedbacks.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<ReportBug>(t, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.ReportBugs.Attach(t);
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<ReportBug>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<ReportBug>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ReportBug>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}