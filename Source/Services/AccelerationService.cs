using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Codenation.Challenge.Services
{
    public class AccelerationService : IAccelerationService
    {
        private CodenationContext _context;

        public AccelerationService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Acceleration> FindByCompanyId(int companyId)
        {
            return _context.Candidates
                .Include(c => c.Acceleration)
                .Where(c => c.CompanyId == companyId)
                .Select(c => c.Acceleration)
                .Distinct()
                .ToList();
        }

        public Acceleration FindById(int id)
        {
            return _context.Accelerations.Find(id);
        }

        public Acceleration Save(Acceleration acceleration)
        {
            if (acceleration.Id == default)
            {
                _context.Accelerations.Add(acceleration);
            }
            else
            {
                _context.Accelerations.Update(acceleration);
            }

            _context.SaveChanges();

            return acceleration;
        }
    }
}
