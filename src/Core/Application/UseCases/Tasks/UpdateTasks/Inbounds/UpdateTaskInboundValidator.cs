namespace TaskFlowHub.Core.Application.UseCases.Tasks.UpdateTasks.Inbounds;

public class UpdateTaskInboundValidator : AbstractValidator<UpdateTaskInbound>
{
    public UpdateTaskInboundValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Task Id is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Task Title is required.")
            .MinimumLength(10).WithMessage("Task Title must be at least {MinLength} characters.")
            .MaximumLength(100).WithMessage("Task Title must not exceed {MaxLength} characters.");


        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Task Description is required.")
            .MinimumLength(10).WithMessage("Task Description must be at least {MinLength} characters.")
            .MaximumLength(1000).WithMessage("Task Description must not exceed {MaxLength} characters.");
    }
}
