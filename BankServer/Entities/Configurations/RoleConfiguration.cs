using BankServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankServer.Entities.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public static readonly string AdminRoleId = "C0A0748E-09B6-43F3-BA37-84952E8545E9";
        public static readonly string UserRoleId = "04E3A0CA-C0DB-4D71-A065-9E131B989597";

        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole { Id = AdminRoleId, Name = UserRoles.Admin, NormalizedName = UserRoles.Admin.ToUpper() },
                new IdentityRole { Id = UserRoleId, Name = UserRoles.User, NormalizedName = UserRoles.User.ToUpper() }
                );
        }
    }
}
