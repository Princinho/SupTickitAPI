using Microsoft.EntityFrameworkCore;
using Suptickit.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Infrastructure
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly SuptickitContext _db;
        public SettingsRepository(SuptickitContext db)
        {
            _db = db;
        }
        public async Task<ServiceResponse<string>> GetSettingsSlug()
        {
            var settings = await _db.Settings.FirstOrDefaultAsync();
            if (settings == null)
            {
                return new ServiceResponse<string> { Success = false, Message = "No settings defined" };
            }
            return new ServiceResponse<string> { Success = true, Data = settings.SettingsSlug };
        }



        public async Task<ServiceResponse<string>> SetSettingsSlug(string value)
        {
            try
            {
                var settings = await _db.Settings.FirstOrDefaultAsync();
                if (settings == null)
                {
                    _db.Settings.Add(new Domain.Models.Settings { SettingsSlug = value });
                    await _db.SaveChangesAsync();
                    return new ServiceResponse<string> { Success = true, Message = "Settings saved" };
                }
                else
                {
                    settings.SettingsSlug = value;
                    await _db.SaveChangesAsync();
                    return new ServiceResponse<string> { Success = true, Message = "Settings saved" };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse<string> { Success = false, Message = "Failed to save, " + ex.Message };

            }

        }
    }
}
