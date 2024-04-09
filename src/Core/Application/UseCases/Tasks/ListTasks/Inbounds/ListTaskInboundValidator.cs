namespace TaskFlowHub.Core.Application.UseCases.Tasks.ListTasks.Inbounds;

public class ListTaskInboundValidator : AbstractValidator<ListTaskInbound>
{
    public ListTaskInboundValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id is required.");
    }
}
