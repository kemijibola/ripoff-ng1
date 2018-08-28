using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class RipOffFirmRepository:IRipOffFirm
    {
        RipOffContext _ctx;

        public RipOffFirmRepository(RipOffContext ctx)
        {
            _ctx = ctx;                
            _ctx.Configuration.LazyLoadingEnabled = false;             
        }

        public RepositoryActionResult<RipOffFirm> Delete(int id)
        {
            try
            {
                var exp = _ctx.RipOffFirms.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.RipOffFirms.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<RipOffFirm>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<RipOffFirm>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<RipOffFirm>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public System.Linq.IQueryable<RipOffFirm> GetFirm()
        {
            return _ctx.RipOffFirms;
        }


        public System.Linq.IQueryable<RipOffFirm> Get()
        {
            return _ctx.RipOffFirms.Include("FirmCategories").Include("FirmImages");
        }

        public RepositoryActionResult<RipOffFirm> Insert(RipOffFirm t)
        {
            try
            {
                _ctx.RipOffFirms.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<RipOffFirm>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<RipOffFirm>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<RipOffFirm>(null, RepositoryActionStatus.Error, ex);
            }
        }
        //public bool UpdateFirm(int id, int lawfirm)
        //{
        //    try
        //    {
        //        var firm = _ctx.RipOffFirms.SingleOrDefault(c => c.Id == id);

        //        if (firm == null)
        //            throw new Exception(string.Format("Firm with id: '{0}' not found", id.ToString()));

        //        firm.firmlike = lawfirm;
        //        // firm.profileView = lawfirm.profileView;


        //        _ctx.Entry(firm).State = EntityState.Modified;
        //        _ctx.SaveChanges();
        //        return true;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        public RepositoryActionResult<RipOffFirm> Update(RipOffFirm t)
        {
            try
            {
                var existingData = _ctx.RipOffFirms.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<RipOffFirm>(t, RepositoryActionStatus.NotFound);
                }
              
                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.RipOffFirms.Attach(t);        
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<RipOffFirm>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<RipOffFirm>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<RipOffFirm>(null, RepositoryActionStatus.Error, ex);
            }
        }   
    }
}