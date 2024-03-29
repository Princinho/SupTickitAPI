﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Suptickit.Application;
using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Suptickit.Infrastructure
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SuptickitContext _db;
        private readonly IConfiguration _config;
        public AuthRepository(SuptickitContext db, IConfiguration configuration)
        {
            _db = db;
            _config = configuration;
        }
        public async Task<string> Login(string username, string password)
        {
            var user = await _db.Users.Include(u=>u.RoleAssignments).FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
            if(user is null)
            {
                throw new ArgumentException("User does not exist");
            }else if(!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
            {
                throw new ArgumentException("Wrong password");
            }
            return CreateToken(user);

        }

        public async Task<int> Register(User user, string password)
        {
            if (await UserExists(user.Username))
            {
                throw new ArgumentException("User already exists"); 
            }
            CreatePasswordHash(password,out byte[] passwordHash,out byte[] salt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt= salt;
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user.Id;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await   _db.Users.AnyAsync(user => user.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }
        private void CreatePasswordHash(string password, out byte[]passwordHash,out byte[] passwordSalt)
        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac=new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Username),
                new Claim("RoleAssignments",JsonSerializer.Serialize(user.RoleAssignments))
            };
            var appSettingsToken = _config.GetSection("Appsettings:Token").Value;
            if(appSettingsToken is null)
            {
                throw new Exception("Appsettings token is null");
            }
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = credentials
            };
            JwtSecurityTokenHandler tokenHandler=new JwtSecurityTokenHandler();
            SecurityToken token=tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            
        }
    }
}
