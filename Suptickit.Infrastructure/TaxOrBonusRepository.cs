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
    public class TaxOrBonusRepository : ITaxOrBonusRepository
    {
        private readonly SuptickitContext _db;
        public TaxOrBonusRepository(SuptickitContext db)
        {
            _db = db;
        }
        public async Task<ServiceResponse<TaxOrBonus>> AddAsync(TaxOrBonus taxOrBonus)
        {
            try
            {
                _db.TaxOrBonuses.Add(taxOrBonus);
                await _db.SaveChangesAsync();
                return new ServiceResponse<TaxOrBonus> { Data = taxOrBonus, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<TaxOrBonus> { Success = false, Message = "Failed to create TaxOrBonus, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<IEnumerable<TaxOrBonus>>> GetAllAsync()
        {
            try
            {
                var taxOrBonuss = await _db.TaxOrBonuses.Where(t=>!t.IsArchived).ToListAsync();
                return new ServiceResponse<IEnumerable<TaxOrBonus>> { Data = taxOrBonuss, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<IEnumerable<TaxOrBonus>> { Success = true, Message = "Failed to retrieve TaxOrBonuss, " + ex.Message };
            }
        }
        public async Task<ServiceResponse<IEnumerable<TaxOrBonus>>> GetAllWithArchivedAsync()
        {
            try
            {
                var taxOrBonuss = await _db.TaxOrBonuses.ToListAsync();
                return new ServiceResponse<IEnumerable<TaxOrBonus>> { Data = taxOrBonuss, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<IEnumerable<TaxOrBonus>> { Success = true, Message = "Failed to retrieve TaxOrBonuss, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<TaxOrBonus>> GetByIdAsync(int id)
        {
            try
            {
                var taxOrBonus = await _db.TaxOrBonuses.FindAsync(id);
                if (taxOrBonus == null) return new ServiceResponse<TaxOrBonus> { Success = false, Message = "No matching taxOrBonus for id " + id };
                return new ServiceResponse<TaxOrBonus> { Data = taxOrBonus, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<TaxOrBonus> { Success = false, Message = "Failed to retrieve taxOrBonus, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<TaxOrBonus>> RemoveAsync(int id)
        {
            try
            {
                var taxOrBonus = await _db.TaxOrBonuses.Include(t => t.AppliedValues).FirstOrDefaultAsync(t => t.Id == id);
                if (taxOrBonus.AppliedValues.Any()) { taxOrBonus.IsEnabled = false; taxOrBonus.IsArchived = true; }
                else
                {
                    _db.TaxOrBonuses.Remove(taxOrBonus);
                }
                await _db.SaveChangesAsync();
                return new ServiceResponse<TaxOrBonus> { Data = taxOrBonus, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<TaxOrBonus> { Success = false, Message = "Failed to retrieve taxOrBonus, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<TaxOrBonus>> UpdateAsync(TaxOrBonus taxOrBonus)
        {
            //Disables previous one and creates a new one to keep track of all
            try
            {
                var oldTaxOrBonus = await _db.TaxOrBonuses.Include(t => t.AppliedValues).FirstOrDefaultAsync(t => t.Id == taxOrBonus.Id);
                if (oldTaxOrBonus.AppliedValues.Any())
                {//because entity had already been assigned to quotes, we disable it instead of deleting it.
                    oldTaxOrBonus.IsArchived = true;
                    oldTaxOrBonus.IsEnabled=false;
                    _db.TaxOrBonuses.Add(taxOrBonus);
                }
                else
                {//If entry hadn't been used yet, we just modify it
                    oldTaxOrBonus.IsBonus = taxOrBonus.IsBonus;
                    oldTaxOrBonus.IsEnabled = taxOrBonus.IsEnabled;
                    oldTaxOrBonus.ExclusionList = taxOrBonus.ExclusionList;
                    oldTaxOrBonus.Amount = taxOrBonus.Amount;
                    oldTaxOrBonus.Description = taxOrBonus.Description;
                    oldTaxOrBonus.IsPercentage = taxOrBonus.IsPercentage;
                    oldTaxOrBonus.Name = taxOrBonus.Name;
                }
                await _db.SaveChangesAsync();
                return new ServiceResponse<TaxOrBonus> { Data = taxOrBonus, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<TaxOrBonus> { Success = false, Message = "Failed to update taxOrBonus, " + ex.Message };
            }
        }
    }
}
