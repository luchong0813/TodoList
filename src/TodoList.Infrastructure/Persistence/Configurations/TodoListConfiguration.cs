using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Infrastructure.Persistence.Configurations
{
    public class TodoListConfiguration : IEntityTypeConfiguration<Domain.Entities.TodoList>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.TodoList> builder)
        {
            builder.Ignore(e => e.DomainEvents);
            builder.Property(t=>t.Title).HasMaxLength(200).IsRequired();

            builder.OwnsOne(b => b.Colour);
        }
    }
}
