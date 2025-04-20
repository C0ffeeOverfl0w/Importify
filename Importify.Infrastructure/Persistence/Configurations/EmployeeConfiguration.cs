namespace Importify.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configures the database schema for the <see cref="Employee"/> entity.
/// </summary>
public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    /// <summary>
    /// Configures the properties and relationships of the <see cref="Employee"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employee");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.PayrollNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.OwnsOne(e => e.Name, n =>
        {
            n.Property(name => name.FirstName)
                .IsRequired();

            n.Property(name => name.LastName)
                .IsRequired();
        });

        builder.Property(e => e.DateOfBirth)
            .IsRequired();

        builder.Property(e => e.Telephone)
            .HasMaxLength(20);

        builder.Property(e => e.Mobile)
            .HasMaxLength(20);

        builder.OwnsOne(e => e.Address, a =>
        {
            a.Property(x => x.AddressLine1)
                .IsRequired();

            a.Property(x => x.AddressLine2)
                .IsRequired();

            a.Property(x => x.Postcode)
                .IsRequired();
        });

        builder.OwnsOne(e => e.Email, e =>
        {
            e.Property(email => email.Value)
                .IsRequired()
                .HasMaxLength(200);
        });

        builder.Property(e => e.StartDate)
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .IsRequired();
    }
}