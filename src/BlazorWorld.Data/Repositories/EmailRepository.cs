using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Entities.Organization;
using BlazorWorld.Core.Repositories;
using BlazorWorld.Data.DbContexts;

namespace BlazorWorld.Data.Repositories
{
    public class EmailRepository : Repository, IEmailRepository
    {
        public EmailRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public void Add(Email email)
        {
            _dbContext.Emails.Add(email);
        }
    }
}
