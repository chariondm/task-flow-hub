namespace TaskFlowHub.Infrastructure.Database.MySqlDb.Configurations.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");

        builder
            .HasKey(u => u.Id)
            .HasName("user_pk");

        builder.Property(u => u.Id)
            .HasColumnName("user_id");

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("username");

        builder.HasIndex(u => u.Username)
            .IsUnique()
            .HasDatabaseName("username_unique");

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("email");

        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasDatabaseName("email_unique");

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("password");

        builder.Property(u => u.IsAdmin)
            .IsRequired()
            .HasColumnName("is_admin");

        builder.Property<DateTime>("created_at")
            .IsRequired();

        builder.Property<DateTime>("updated_at")
            .IsRequired();
    }
}