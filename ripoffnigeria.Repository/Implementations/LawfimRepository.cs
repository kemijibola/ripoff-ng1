using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class LawfirmRepository : ILawfirm
    {
        RipOffContext _ctx;

        public LawfirmRepository(RipOffContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = false;
        }

        public RepositoryActionResult<LawFirm> Delete(int id)
        {
            try
            {
                var exp = _ctx.LawFirms.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.LawFirms.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<LawFirm>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<LawFirm>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<LawFirm>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public System.Linq.IQueryable<LawFirm> Get()
        {
            return _ctx.LawFirms;
        }

        public RepositoryActionResult<LawFirm> Insert(LawFirm t)
        {
            try
            {
                _ctx.LawFirms.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<LawFirm>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<LawFirm>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<LawFirm>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<LawFirm> Update(LawFirm t)
        {
            try
            {
                var existingData = _ctx.LawFirms.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<LawFirm>(t, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.LawFirms.Attach(t);
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<LawFirm>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<LawFirm>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<LawFirm>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}