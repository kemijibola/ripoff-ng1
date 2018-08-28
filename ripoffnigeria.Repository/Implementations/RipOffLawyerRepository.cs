using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class RipOffLawyerRepository : IRipOffLawyer
    {
     RipOffContext _ctx;

   public RipOffLawyerRepository(RipOffContext ctx)
        {
            _ctx = ctx;                
            _ctx.Configuration.LazyLoadingEnabled = false;             
        }

   public RepositoryActionResult<RipOffLawyer> Delete(int id)
        {
            try
            {
                var exp = _ctx.RipOffLawyers.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.RipOffLawyers.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<RipOffLawyer>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<RipOffLawyer>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<RipOffLawyer>(null, RepositoryActionStatus.Error, ex);
            }
        }

   public System.Linq.IQueryable<RipOffLawyer> Get()
        {
            return _ctx.RipOffLawyers;
        }

   public RepositoryActionResult<RipOffLawyer> Insert(RipOffLawyer t)
        {
            try
            {
                _ctx.RipOffLawyers.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<RipOffLawyer>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<RipOffLawyer>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<RipOffLawyer>(null, RepositoryActionStatus.Error, ex);
            }
        }

   public RepositoryActionResult<RipOffLawyer> Update(RipOffLawyer t)
        {
            try
            {
                var existingData = _ctx.RipOffLawyers.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<RipOffLawyer>(t, RepositoryActionStatus.NotFound);
                }
              
                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.RipOffLawyers.Attach(t);        
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<RipOffLawyer>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<RipOffLawyer>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<RipOffLawyer>(null, RepositoryActionStatus.Error, ex);
            }
        }   
    }
}