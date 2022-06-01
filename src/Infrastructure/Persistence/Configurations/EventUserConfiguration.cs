using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class EventUserConfiguration : IEntityTypeConfiguration<EventUser>
{
    public void Configure(EntityTypeBuilder<EventUser> builder)
    {
        builder.ToTable(nameof(EventUser)).HasKey(eu => new {eu.ClientId, eu.EventId});
        builder.ToTable(nameof(EventUser)).Property(eu => eu.StatusId).ValueGeneratedOnAdd();
        builder.ToTable(nameof(EventUser)).Property(eu => eu.EventId).ValueGeneratedOnAdd();
        builder.ToTable(nameof(EventUser)).Property(eu => eu.ClientId).ValueGeneratedOnAdd();
    }
}