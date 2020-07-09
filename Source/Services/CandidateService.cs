using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class CandidateService : ICandidateService
    {
        private CodenationContext _context;

        public CandidateService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Candidate> FindByAccelerationId(int accelerationId)
        {
            return _context.Candidates
               .Where(c => c.AccelerationId == accelerationId)
               .ToList();

        }

        public IList<Candidate> FindByCompanyId(int companyId)
        {
            return _context.Candidates
                .Where(c => c.CompanyId == companyId)
                .ToList();
        }

        public Candidate FindById(int userId, int accelerationId, int companyId)
        {
            return _context.Candidates
                .FirstOrDefault(
                    c => c.UserId == userId &&
                    c.AccelerationId == accelerationId &&
                    c.CompanyId == companyId);
        }

        public Candidate Save(Candidate candidate)
        {
            if (_context.Candidates.Any(c => c.AccelerationId == candidate.AccelerationId &&
            c.CompanyId == candidate.CompanyId &&
            c.UserId == candidate.UserId))
            {
                _context.Candidates.Update(candidate);
            }
            else
            {
                _context.Candidates.Add(candidate);
            }

            _context.SaveChanges();

            return candidate;
        }
    }
}
