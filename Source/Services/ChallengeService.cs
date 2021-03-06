using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class ChallengeService : IChallengeService
    {
        private CodenationContext _context;

        public ChallengeService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Models.Challenge> FindByAccelerationIdAndUserId(int accelerationId, int userId)
        {
            return _context.Candidates
                .Include(c => c.Acceleration)
                    .ThenInclude(a => a.Challenge)
                .Where(c => c.AccelerationId == accelerationId && c.UserId == userId)
                .Select(c => c.Acceleration.Challenge)
                .Distinct()
                .ToList();
        }

        public Models.Challenge Save(Models.Challenge challenge)
        {
            if (challenge.Id == default)
            {
                _context.Challenges.Add(challenge);
            }
            else
            {
                _context.Challenges.Update(challenge);
            }

            _context.SaveChanges();

            return challenge;
        }
    }
}