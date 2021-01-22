using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Entities.Organization;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BlazorWorld.Data.DbContexts
{
    public class AppDbContext : DbContext
    {
        private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options)
        {
            _operationalStoreOptions = operationalStoreOptions;
        }

        protected AppDbContext(DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options)
        {
            _operationalStoreOptions = operationalStoreOptions;
        }

        // configuration entities
        public DbSet<Setting> Settings { get; set; }

        // content entities
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<NodeCustomFields> NodeCustomFields { get; set; }
        public DbSet<NodeLink> NodeLinks { get; set; }
        public DbSet<NodeTag> NodeTags { get; set; }
        public DbSet<NodeVersion> NodeVersions { get; set; }
        public DbSet<NodeVote> NodeVotes { get; set; }
        public DbSet<NodeReaction> Reactions { get; set; }

        // organization entities
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Invitation> Invitations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);

            modelBuilder.Entity<Activity>()
                .HasIndex(x => x.NodeId);
            modelBuilder.Entity<Activity>()
                .HasIndex(x => x.CreatedBy);
            modelBuilder.Entity<Message>()
                .HasIndex(x => x.GroupId);
            modelBuilder.Entity<Node>()
                .HasIndex(x => x.Slug);
            modelBuilder.Entity<Node>()
                .HasIndex(x => x.GroupId);
            modelBuilder.Entity<Node>()
                .HasIndex(x => x.ParentId);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.NodeId);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.IndexedCustomField1);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.IndexedCustomField2);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.IndexedCustomField3);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.IndexedCustomField4);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.IndexedCustomField5);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.IndexedCustomField6);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.IndexedCustomField7);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.IndexedCustomField8);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.IndexedCustomField9);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.IndexedCustomField10);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.IndexedCustomField11);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.IndexedCustomField12);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.IndexedCustomField13);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.IndexedCustomField14);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.IndexedCustomField15);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.IndexedCustomField16);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.IndexedCustomField17);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.IndexedCustomField18);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.IndexedCustomField19);
            modelBuilder.Entity<NodeCustomFields>()
                .HasIndex(x => x.IndexedCustomField20);
            modelBuilder.Entity<NodeLink>()
                .HasIndex(x => new { x.FromNodeId, x.Type });
            modelBuilder.Entity<NodeLink>()
                .HasIndex(x => new { x.ToNodeId, x.Type });
            modelBuilder.Entity<NodeReaction>()
                .HasIndex(x => x.NodeId);
            modelBuilder.Entity<NodeReaction>()
                .HasIndex(x => new { x.NodeId, x.UserId });
            modelBuilder.Entity<NodeTag>()
                .HasIndex(x => x.Tag);
            modelBuilder.Entity<NodeTag>()
                .HasIndex(x => x.NodeId);
            modelBuilder.Entity<NodeVersion>()
                .HasIndex(x => x.NodeId);
            modelBuilder.Entity<NodeVote>()
                .HasIndex(x => x.NodeId);
            modelBuilder.Entity<NodeVote>()
                .HasIndex(x => new { x.NodeId, x.UserId });

            modelBuilder.Entity<Badge>()
                .HasIndex(x => x.UserId);
            modelBuilder.Entity<GroupMember>()
                .HasIndex(x => x.GroupId);
            modelBuilder.Entity<GroupMember>()
                .HasIndex(x => new { x.GroupId, x.UserId });
            modelBuilder.Entity<Invitation>()
                .HasIndex(x => x.Email);
            modelBuilder.Entity<Site>()
                .HasIndex(x => x.TenantId);
        }
    }
}
