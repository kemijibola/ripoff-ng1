using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class TransactionRepository : ITransaction
    {
        RipOffContext _ctx;

        public TransactionRepository(RipOffContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = false;
        }

        public RepositoryActionResult<Transaction> Delete(int id)
        {
            try
            {
                var exp = _ctx.Transactions.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.Transactions.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<Transaction>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<Transaction>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Transaction>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public System.Linq.IQueryable<Transaction> Get()
        {
            return _ctx.Transactions;
        }
        public System.Linq.IQueryable<Transaction> Get(int id)
        {
            return _ctx.Transactions.Where(d => d.Id == id);
        }

        public RepositoryActionResult<Transaction> Insert(Transaction t)
        {
            try
            {
                _ctx.Transactions.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Transaction>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Transaction>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Transaction>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Transaction> Update(Transaction t)
        {
            try
            {
                var existingData = _ctx.Transactions.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<Transaction>(t, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.Transactions.Attach(t);
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Transaction>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Transaction>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Transaction>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}