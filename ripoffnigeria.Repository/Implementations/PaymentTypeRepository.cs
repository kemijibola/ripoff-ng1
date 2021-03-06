﻿using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class PaymentTypeRepository : IPaymentType
    {
        RipOffContext _ctx;

        public PaymentTypeRepository(RipOffContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = false;
        }

        public RepositoryActionResult<PaymentType> Delete(int id)
        {
            try
            {
                var exp = _ctx.PaymentTypes.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.PaymentTypes.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<PaymentType>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<PaymentType>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<PaymentType>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public System.Linq.IQueryable<PaymentType> Get()
        {
            return _ctx.PaymentTypes;
        }

        public RepositoryActionResult<PaymentType> Insert(PaymentType t)
        {
            try
            {
                _ctx.PaymentTypes.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<PaymentType>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<PaymentType>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<PaymentType>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<PaymentType> Update(PaymentType t)
        {
            try
            {
                var existingData = _ctx.Cities.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<PaymentType>(t, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.PaymentTypes.Attach(t);
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<PaymentType>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<PaymentType>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<PaymentType>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}