using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class BankRepository : IBank
    {
        RipOffContext _ctx;

        public BankRepository(RipOffContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = false;
        }

        public RepositoryActionResult<Bank> Delete(int id)
        {
            try
            {
                var exp = _ctx.Banks.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.Banks.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<Bank>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<Bank>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Bank>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public System.Linq.IQueryable<Bank> Get()
        {
            return _ctx.Banks;
        }
        public System.Linq.IQueryable<Bank> Get(int id)
        {
            return _ctx.Banks.Where(d => d.Id == id);
        }

        public RepositoryActionResult<Bank> Insert(Bank t)
        {
            try
            {
                _ctx.Banks.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Bank>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Bank>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Bank>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Bank> Update(Bank t)
        {
            try
            {
                var existingData = _ctx.Banks.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<Bank>(t, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.Banks.Attach(t);
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Bank>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Bank>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Bank>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}