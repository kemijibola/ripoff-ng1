﻿using System;
using System.Data.Entity;
using System.Linq;
using ripoffnigeria.DTO;
using ripoffnigeria.Repository.Entities;
using ripoffnigeria.Repository.Interfaces;

namespace ripoffnigeria.Repository.Implementations
{
    public class ReportRejectionRepository : IReportRejection
    {
        RipOffContext _ctx;

        public ReportRejectionRepository(RipOffContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = false;
        }

        public RepositoryActionResult<ReportRejection> Delete(int id)
        {
            try
            {
                var exp = _ctx.ReportRejections.FirstOrDefault(e => e.Id == id);
                if (exp != null)
                {
                    _ctx.ReportRejections.Remove(exp);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<ReportRejection>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<ReportRejection>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ReportRejection>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public System.Linq.IQueryable<ReportRejection> Get()
        {
            return _ctx.ReportRejections;
        }
        public System.Linq.IQueryable<ReportRejection> Get(int id)
        {
            return _ctx.ReportRejections.Where(d => d.Id == id);
        }

        public RepositoryActionResult<ReportRejection> Insert(ReportRejection t)
        {
            try
            {
                _ctx.ReportRejections.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<ReportRejection>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<ReportRejection>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ReportRejection>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<ReportRejection> Update(ReportRejection t)
        {
            try
            {
                var existingData = _ctx.ReportRejections.FirstOrDefault(exp => exp.Id == t.Id);

                if (existingData == null)
                {
                    return new RepositoryActionResult<ReportRejection>(t, RepositoryActionStatus.NotFound);
                }

                _ctx.Entry(existingData).State = EntityState.Detached;
                _ctx.ReportRejections.Attach(t);
                _ctx.Entry(t).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<ReportRejection>(t, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<ReportRejection>(t, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ReportRejection>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}