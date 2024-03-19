using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Application
{
    public interface ISettingsRepository
    {
        public Task<ServiceResponse<string>> GetSettingsSlug();
        public Task<ServiceResponse<string>> SetSettingsSlug(string value);
    }
}
