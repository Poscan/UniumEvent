using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable(nameof(Event)).HasData(new List<Event>()
        {
            new Event()
            {
                Id = 1, 
                Name = "Городская Академия Юниум", 
                PageName = "GAU",
                StartDate = new DateTime(2022, 6, 1),
                EndDate = new DateTime(2022, 7, 1)
            },
        });
    }
}