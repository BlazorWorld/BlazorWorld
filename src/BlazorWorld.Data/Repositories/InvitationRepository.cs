using BlazorWorld.Core.Entities.Organization;
using BlazorWorld.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWorld.Data.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        public ApplicationDbContext _dbContext { get; set; }

        public InvitationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Add(Invitation invite)
        {
            _dbContext.Invitations.Add(invite);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<string> GetInvitationAsync(string email, string code)
        {
            var invites = from i in _dbContext.Invitations
                where (i.Email == email) && (i.InvitationCode == code)
                select i;

            var invite = (await invites.FirstOrDefaultAsync());

            if (invite != null)
            {
                return invite.CreatedBy;
            }

            return string.Empty;
        }

        public async Task<Invitation[]> GetInvitationsAsync(string createdBy)
        {
            var invites = from i in _dbContext.Invitations
                          where i.CreatedBy == createdBy
                          select i;

            return await invites.ToArrayAsync();
        }
    }
}