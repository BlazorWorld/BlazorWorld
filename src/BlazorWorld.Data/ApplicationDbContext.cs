using BlazorWorld.Core.Entities.Common;
using BlazorWorld.Core.Entities.Configuration;
using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Core.Entities.Organization;
using Microsoft.EntityFrameworkCore;

namespace BlazorWorld.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // common entities
        public DbSet<EntityCustomFields> EntityCustomFields { get; set; }
        public DbSet<EntityTag> EntityTags { get; set; }

        // configuration entities
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        // content entities
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<NodeVersion> NodeVersions { get; set; }
        public DbSet<NodeVote> NodeVotes { get; set; }
        public DbSet<Reaction> Reactions { get; set; }

        // organization entities
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Invitation> Invitations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.EntityId);
            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.IndexedCustomField1);
            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.IndexedCustomField2);
            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.IndexedCustomField3);
            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.IndexedCustomField4);
            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.IndexedCustomField5);
            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.IndexedCustomField6);
            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.IndexedCustomField7);
            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.IndexedCustomField8);
            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.IndexedCustomField9);
            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.IndexedCustomField10);
            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.IndexedCustomField11);
            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.IndexedCustomField12);
            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.IndexedCustomField13);
            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.IndexedCustomField14);
            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.IndexedCustomField15);
            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.IndexedCustomField16);
            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.IndexedCustomField17);
            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.IndexedCustomField18);
            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.IndexedCustomField19);
            modelBuilder.Entity<EntityCustomFields>()
                .HasIndex(x => x.IndexedCustomField20);
            modelBuilder.Entity<Permission>()
                .HasIndex(p => new { p.Module, p.Type, p.Action });
            modelBuilder.Entity<EntityTag>()
                .HasIndex(x => x.Tag);
            modelBuilder.Entity<EntityTag>()
                .HasIndex(x => x.EntityId);

            modelBuilder.Entity<Activity>()
                .HasIndex(x => x.NodeId);
            modelBuilder.Entity<Activity>()
                .HasIndex(x => x.CreatedBy);
            modelBuilder.Entity<Category>()
                .HasIndex(x => x.ParentCategoryId);
            modelBuilder.Entity<Category>()
                .HasIndex(x => x.Path);
            modelBuilder.Entity<Message>()
                .HasIndex(x => x.GroupId);
            modelBuilder.Entity<Node>()
                .HasIndex(x => x.Slug);
            modelBuilder.Entity<Node>()
                .HasIndex(x => x.CategoryId);
            modelBuilder.Entity<Node>()
                .HasIndex(x => x.Path);
            modelBuilder.Entity<Node>()
                .HasIndex(x => x.GroupId);
            modelBuilder.Entity<Node>()
                .HasIndex(x => x.ParentId);
            modelBuilder.Entity<Reaction>()
                .HasIndex(x => x.ContentId);
            modelBuilder.Entity<Reaction>()
                .HasIndex(x => new { x.ContentId, x.UserId });
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
