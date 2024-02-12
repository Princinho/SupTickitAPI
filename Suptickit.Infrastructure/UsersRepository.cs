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
    public class UsersRepository : IUsersRepository
    {
        private readonly SuptickitContext _db;
        public UsersRepository(SuptickitContext db)
        {
            _db = db;
        }

        public async Task<User> GetByName(string name)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Username == name);
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _db.Users.Include(u => u.RoleAssignments).ToListAsync();
        }


        public async Task<Application.ServiceResponse<User>> RemoveAsync(int id)
        {
            var user = _db.Users.Find(id);
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return new Application.ServiceResponse<User> { Data = user, Success = true, Message = "User successfully deleted" };
        }

        public async Task<Application.ServiceResponse<User>> GetByIdAsync(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user is null) return new Application.ServiceResponse<User> { Message = "User not found", Success = false };
            return new Application.ServiceResponse<User> { Data = user, Success = true };
        }
        public async Task<Application.ServiceResponse<User>> GetByUserNameAsync(string username)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user is null) return new Application.ServiceResponse<User> { Message = "User not found", Success = false };
            return new Application.ServiceResponse<User> { Data = user, Success = true };
        }

    }
}
