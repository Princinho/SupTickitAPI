﻿using Microsoft.EntityFrameworkCore;
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


        public async Task<Project> DeleteById(int id)
        {
            var project = _db.Projects.Find(id);
            if (project == null) { throw new ArgumentException("Company with id " + id + " does not exist" + nameof(project)); }
            _db.Projects.Remove(project);
            await  _db.SaveChangesAsync();
            return project;
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _db.Projects.ToListAsync();

        }

        public async Task<IEnumerable<Project>> GetByCompanyId(int id)
        {
            return await _db.Projects.Where(p=>p.Companies.Any(c=>c.Id==id)).ToListAsync();
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

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _db.Projects.FindAsync(id);
        }

        public async Task<Project> AddAsync(Project project)
        {
            _db.Projects.Add(project);
            await _db.SaveChangesAsync();
            return project;
        }
    }
}
