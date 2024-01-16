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
        public Project GetById(int id);
        public Project Add(Project project);
        public Project GetByCompanyId(int id);
        public Task UpdateProjectAsync(Project project, int id);
        public Task AssignToCompanyAsync(int projectId,int  companyId);
        public Task DeleteByIdAsync(int id);
        
    }
}
