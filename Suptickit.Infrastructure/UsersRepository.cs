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

        public async Task<ServiceResponse<User>> UpdateAsync(User user, int id)
        {
            if (user.Id != id) return new ServiceResponse<User> { Success = false, Message = "Invalid arguments for update" };
            try
            {
                var dbUser = _db.Users.FirstOrDefault(u => u.Id == user.Id);
                dbUser.Username = user.Username;
                dbUser.CompanyId = user.CompanyId;
                dbUser.Firstname = user.Firstname;
                dbUser.Lastname = user.Lastname;
                await _db.SaveChangesAsync();
                return new ServiceResponse<User> { Success = true, Data = dbUser };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<User> { Success = false, Message = ex.Message };
            }
        }
    }
}
