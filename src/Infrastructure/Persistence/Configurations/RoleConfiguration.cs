using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.ToTable("AspNetRoles").HasData(new List<IdentityRole>
        {
            new IdentityRole("Ученик"),
            new IdentityRole("Преподаватель"),
            new IdentityRole("Менеджер"),
            new IdentityRole("Руководитель")
        });
    }
}