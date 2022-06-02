using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.ToTable(nameof(Status)).HasData(new List<Status>()
        {
            new Status() { Id = 1, Name = "Ученик" },
            new Status() { Id = 2, Name = "Преподаватель" },
            new Status() { Id = 3, Name = "Менеджер" },
            new Status() { Id = 4, Name = "Руководитель" }
        });
    }
}