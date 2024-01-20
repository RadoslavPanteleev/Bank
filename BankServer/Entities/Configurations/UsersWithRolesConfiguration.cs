using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankServer.Entities.Configurations
{
    public class UsersWithRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            IdentityUserRole<string> iur = new IdentityUserRole<string>
            {
                RoleId = RoleConfiguration.AdminRoleId,
                UserId = UsersConfiguration.AdminId
            };
            builder.HasData(iur);
        }
    }
}
