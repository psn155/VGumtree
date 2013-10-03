using VGumtree.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace VGumtree.Model.Migrations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration() : base()
        {
            // Many to many relationship between User and Role table. This will automatically create the UsersInRoles table for this relationship
            this.HasMany(a => a.Roles)
                .WithMany(b => b.Users).Map(m =>
                    {
                        m.MapLeftKey("UserId");
                        m.MapRightKey("RoleId");
                        m.ToTable("webpages_UsersInRoles");
                    });            
        }
    }
}
