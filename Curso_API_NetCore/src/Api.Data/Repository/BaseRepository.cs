using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MyContext _context;
        private DbSet<T> _dataSet;
        public BaseRepository(MyContext context)
        {
            _context = context;
            _dataSet = context.Set<T>();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var res = await _dataSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (res == null)
                    return false;

                _dataSet.Remove(res);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public async Task<bool> ExisteAsync(Guid id)
        {
            return await _dataSet.AnyAsync(p => p.Id.Equals(id));
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if (item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }
                item.CreateAt = DateTime.UtcNow;
                _dataSet.Add(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await _dataSet.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await _dataSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var res = await _dataSet.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
                if (res == null)
                    return null;

                item.UpdateAt = DateTime.UtcNow;
                item.CreateAt = res.CreateAt;
                _context.Entry(res).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;
        }
    }
}
