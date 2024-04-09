namespace TaskFlowHub.Core.Application.UseCases.Tasks.RetrieveTasks.Inbounds;

public class RetrieveTaskInboundValidator : AbstractValidator<RetrieveTaskInbound>
{
    public RetrieveTaskInboundValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Task Id is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id is required.");
    }
}
