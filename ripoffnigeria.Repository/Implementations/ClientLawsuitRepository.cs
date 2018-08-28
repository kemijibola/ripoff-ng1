using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class ClientLawsuitRepository : IClientLawsuit
    {
        RipOffContext _ctx;

        public ClientLawsuitRepository(RipOffContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = false;
        }

        public RepositoryActionResult<ClientLawsuit> Delete(int id)
        {
            try
            {
                var exp = _ctx.ClientLawsuits.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.ClientLawsuits.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<ClientLawsuit>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<ClientLawsuit>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ClientLawsuit>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public System.Linq.IQueryable<ClientLawsuit> Get()
        {
            return _ctx.ClientLawsuits;
        }
        public System.Linq.IQueryable<ClientLawsuit> Get(int id)
        {
            return _ctx.ClientLawsuits.Where(d => d.Id == id);
        }

        public RepositoryActionResult<ClientLawsuit> Insert(ClientLawsuit t)
        {
            try
            {
                _ctx.ClientLawsuits.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<ClientLawsuit>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<ClientLawsuit>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ClientLawsuit>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<ClientLawsuit> Update(ClientLawsuit t)
        {
            try
            {
                var existingData = _ctx.ClientLawsuits.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<ClientLawsuit>(t, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.ClientLawsuits.Attach(t);
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<ClientLawsuit>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<ClientLawsuit>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ClientLawsuit>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}