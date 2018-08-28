using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class CaseUpdateRepository : ICaseUpdate
    {
        RipOffContext _ctx;

        public CaseUpdateRepository(RipOffContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = false;
        }

        public RepositoryActionResult<CaseUpdate> Delete(int id)
        {
            try
            {
                var exp = _ctx.CaseUpdates.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.CaseUpdates.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<CaseUpdate>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<CaseUpdate>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<CaseUpdate>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public System.Linq.IQueryable<CaseUpdate> Get()
        {
            return _ctx.CaseUpdates;
        }
        public System.Linq.IQueryable<CaseUpdate> Get(int id)
        {
            return _ctx.CaseUpdates.Where(d => d.Id == id);
        }

        public RepositoryActionResult<CaseUpdate> Insert(CaseUpdate t)
        {
            try
            {
                _ctx.CaseUpdates.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<CaseUpdate>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<CaseUpdate>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<CaseUpdate>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<CaseUpdate> Update(CaseUpdate t)
        {
            try
            {
                var existingData = _ctx.CaseUpdates.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<CaseUpdate>(t, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.CaseUpdates.Attach(t);
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<CaseUpdate>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<CaseUpdate>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<CaseUpdate>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}