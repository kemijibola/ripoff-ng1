using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class StateRepository:IState
    {

     RipOffContext _ctx;

   public StateRepository(RipOffContext ctx)
        {
            _ctx = ctx;                
            _ctx.Configuration.LazyLoadingEnabled = false;             
        }

        public RepositoryActionResult<State> Delete(int id)
        {
            try
            {
                var exp = _ctx.States.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.States.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<State>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<State>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<State>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public System.Linq.IQueryable<State> Get()
        {
            return _ctx.States;
        }
        public IQueryable<State> GetByCountry(int countryId)
        {
            return _ctx.States.Where(d => d.CountryId == countryId);

        }
        public RepositoryActionResult<State> Insert(State t)
        {
            try
            {
                _ctx.States.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<State>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<State>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<State>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<State> Update(State t)
        {
            try
            {
                var existingData = _ctx.States.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<State>(t, RepositoryActionStatus.NotFound);
                }
              
                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.States.Attach(t);        
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<State>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<State>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<State>(null, RepositoryActionStatus.Error, ex);
            }
        }   
    }
}