namespace TaskFlowHub.Infrastructure.Database.MySqlDb.Configurations.EntityConfigurations;

public class TaskConfiguration : IEntityTypeConfiguration<FlowTask>
{
    public void Configure(EntityTypeBuilder<FlowTask> builder)
    {
        builder.ToTable("task");

        builder
            .HasKey(t => t.Id)
            .HasName("task_pk");

        builder.Property(t => t.Id)
            .HasColumnName("task_id");

        builder.Property(t => t.UserId)
            .IsRequired()
            .HasColumnName("user_id");

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .HasConstraintName("task_user_fk")
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("title");

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(1000)
            .HasColumnName("description");

        builder.Property(t => t.Status)
            .IsRequired()
            .HasColumnName("status")
            .HasConversion<string>();

        builder.Property(t => t.CreationDate)
            .IsRequired()
            .HasColumnName("created_at");

        builder
            .Property<DateTime>("updated_at")
            .IsRequired();
    }
}
