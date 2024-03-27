using Microsoft.EntityFrameworkCore;
using Suptickit.Application;
using Suptickit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Infrastructure
{
    public class PartRepository : IPartRepository
    {
        private readonly SuptickitContext _db;
        public PartRepository(SuptickitContext db) {
            _db = db;
        }
        public async Task<ServiceResponse<Part>> AddAsync(Part part)
        {
            try
            {part.DateCreated = DateTime.UtcNow;
                _db.Parts.Add(part);
                await _db.SaveChangesAsync();
                return new ServiceResponse<Part> { Data = part, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Part> { Success = false, Message="Failed to create service, "+ex.Message };
            }
        }

        public async Task<ServiceResponse<IEnumerable<Part>>> GetAllAsync()
        {
            try
            {
                var services=await _db.Parts.ToListAsync();
               
                return new ServiceResponse<IEnumerable<Part>> { Data = services, Success = true };
            }
            catch (Exception ex)
            {
                return  new ServiceResponse<IEnumerable<Part>> {  Success = true, Message="Failed to retrieve services, "+ex.Message};
            }
        }

        public async Task<ServiceResponse<Part>> GetByIdAsync(int id)
        {
            try
            {
                var service=await _db.Parts.FindAsync(id);
                if (service == null) return new ServiceResponse<Part> { Success = false, Message = "No matching service for id "+id };
                return new ServiceResponse<Part> { Data = service, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Part> { Success = false, Message = "Failed to retrieve service, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<Part>> RemoveAsync(int id)
        {
            try
            {
                var service = await _db.Parts.FindAsync(id);
                _db.Parts.Remove(service);
                await _db.SaveChangesAsync();
                return new ServiceResponse<Part> { Data = service, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Part> { Success = false, Message = "Failed to delete service, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<Part>> UpdateAsync(Part service)
        {
            try
            {
                var oldService = await _db.Parts.FindAsync(service.Id);
                oldService.PartCategoryId = service.PartCategoryId;
                oldService.IsLineEditAllowed = service.IsLineEditAllowed;
                oldService.Price=service.Price;
                oldService.Description = service.Description;
                oldService.Name = service.Name;
                await _db.SaveChangesAsync();
                return new ServiceResponse<Part> { Data = oldService, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Part> { Success = false, Message = "Failed to update service, " + ex.Message };
            }
        }
    }
}
