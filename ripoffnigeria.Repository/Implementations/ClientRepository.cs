using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class ClientRepository : IClient
    {
        RipOffContext _ctx;

        public ClientRepository(RipOffContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = false;
        }

        public RepositoryActionResult<Client> Delete(string id)
        {
            try
            {
                var exp = _ctx.Clients.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.Clients.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<Client>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<Client>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Client>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public System.Linq.IQueryable<Client> Get()
        {
            return _ctx.Clients;
        }
        public System.Linq.IQueryable<Client> Get(string id)
        {
            return _ctx.Clients.Where(d => d.Id == id);
        }

        public RepositoryActionResult<Client> Insert(Client t)
        {
            try
            {
                _ctx.Clients.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Client>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Client>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Client>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Client> Update(Client t)
        {
            try
            {
                var existingData = _ctx.Clients.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<Client>(t, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.Clients.Attach(t);
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Client>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Client>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Client>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}