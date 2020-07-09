using Codenation.Challenge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Codenation.Challenge.Services
{
    public class SubmissionService : ISubmissionService
    {
        private CodenationContext _context;

        public SubmissionService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Submission> FindByChallengeIdAndAccelerationId(int challengeId, int accelerationId)
        {
            return _context.Submissions
               .Where(s => s.ChallengeId == challengeId && s.User.Candidates.Any(c => c.AccelerationId == accelerationId))
               .ToList();
        }

        public decimal FindHigherScoreByChallengeId(int challengeId)
        {
            return _context.Submissions
                .Where(s => s.ChallengeId == challengeId)
                .Select(s => s.Score)
                .OrderByDescending(s => s)
                .FirstOrDefault();
        }

        public Submission Save(Submission submission)
        {
            _context.Add(submission);
            _context.SaveChanges();

            return submission;
        }
    }
}
