using Microsoft.EntityFrameworkCore;
using Suptickit.Application;
using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Infrastructure
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly SuptickitContext _db;
        public ProjectsRepository(SuptickitContext context)
        {
            _db = context;
        }

        public Project Add(Project project)
        {
            _db.Add(project);
            _db.SaveChanges();
            return project;
        }

        public async Task AssignToCompanyAsync(int projectId, int companyId)
        {
            var project = await _db.Projects.FindAsync(projectId);
            var company = await _db.Companies.FindAsync(companyId);
            if (project == null)
            {
                throw new ArgumentException("Project does not exist");
            }
            if (company == null)
            {
                throw new ArgumentException("Company does not exist");
            }
            project.Companies.Add(company);
            await _db.SaveChangesAsync();
        }


        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _db.Projects.ToListAsync();

        }

        public Project GetByCompanyId(int id)
        {
            throw new NotImplementedException();
        }

        public Project GetById(int id)
        {
            throw new NotImplementedException();
        }


        public async Task UpdateProjectAsync(Project project, int id)
        {
            if (project.Id != id) throw new ArgumentException("Ids do not match for update");
            _db.Update(project);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var project = _db.Projects.Find(id) ?? throw new ArgumentException("Project does not exist");
            _db.Projects.Remove(project);
            await _db.SaveChangesAsync();
        }
    }
}
