using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Application
{
    public interface IProjectsRepository
    {
        public Task<IEnumerable<Project>> GetAll();
        public Task<Project> GetByIdAsync(int id);
        public Task<Project> AddAsync(Project project,int userId);
        public Task<IEnumerable<Project>> GetByCompanyId(int id);
        public Task UpdateProjectAsync(Project project, int id);
        public Task AssignToCompanyAsync(int projectId,int  companyId);
        public Task UnassignCompanyAsync(int projectId,int  companyId);
        public Task DeleteByIdAsync(int id);
        
    }
}
