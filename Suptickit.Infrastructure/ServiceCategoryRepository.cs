using Microsoft.EntityFrameworkCore;
using Suptickit.Application;
using Suptickit.Domain.Models;
using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Infrastructure
{
    public class ServiceCategoryRepository : IPartCategoryRepository
    {
        private readonly SuptickitContext _db;
        public ServiceCategoryRepository(SuptickitContext db)
        {
            _db = db;
        }

        public async Task<ServiceResponse<PartCategory>> AddAsync(PartCategory category)
        {
            try
            {

                category.DateCreated = DateTime.UtcNow;
                _db.PartsCategories.Add(category);
                await _db.SaveChangesAsync();
                return new ServiceResponse<PartCategory> { Data = category, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<PartCategory> { Success = false, Message = "Failed to create category, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<IEnumerable<PartCategory>>> GetAllAsync()
        {
            try
            {

                var categories = await _db.PartsCategories.ToListAsync();
                return new ServiceResponse<IEnumerable<PartCategory>> { Data = categories, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<IEnumerable<PartCategory>> { Success = false, Message = "Failed to fetch categories, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<PartCategory>> GetByIdAsync(int id)
        {
            try
            {

                var category = await _db.PartsCategories.FindAsync(id);
                return new ServiceResponse<PartCategory> { Data = category, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<PartCategory> { Success = false, Message = "Failed to fetch category, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<PartCategory>> RemoveAsync(int id)
        {
            try
            {

                var category = await _db.PartsCategories.FindAsync(id);
                _db.PartsCategories.Remove(category);
                await _db.SaveChangesAsync();
                return new ServiceResponse<PartCategory> { Data = category, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<PartCategory> { Success = false, Message = "Failed to delete category, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<PartCategory>> UpdateAsync(PartCategory cat)
        {
            try
            {

                var oldCat = await _db.PartsCategories.FindAsync(cat.Id);
                oldCat.Description=cat.Description;
                oldCat.Name=cat.Name;
                await _db.SaveChangesAsync();
                return new ServiceResponse<PartCategory> { Data = oldCat, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<PartCategory> { Success = false, Message = "Failed to update category, " + ex.Message };
            }
        }
    }
}
