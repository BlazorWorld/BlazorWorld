using BlazorWorld.Core.Entities.Organization;

namespace BlazorWorld.Core.Repositories
{
    public interface IEmailRepository : IRepository
    {
        void Add(Email email);
    }
}
