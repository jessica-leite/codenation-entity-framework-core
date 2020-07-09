using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Codenation.Challenge.Services
{
    public class UserService : IUserService
    {
        private CodenationContext _context;

        public UserService(CodenationContext context)
        {
            _context = context;
        }

        public IList<User> FindByAccelerationName(string name)
        {
            return _context.Candidates
                .Include(c => c.User)
                .Where(c => c.Acceleration.Name == name)
                .Select(c => c.User)
                .ToList();
        }

        public IList<User> FindByCompanyId(int companyId)
        {
            return _context.Candidates
                .Include(c => c.User)
                .Where(c => c.CompanyId == companyId)
                .Select(c => c.User)
                .Distinct()
                .ToList();
        }

        public User FindById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Save(User user)
        {
            if (user.Id == default)
            {
                _context.Users.Add(user);
            }
            else
            {
                _context.Users.Update(user);
            }

            _context.SaveChanges();

            return user;
        }
    }
}
