using FluentValidation;
using MediatR;
using BuildingBlocks.CQRS;

namespace BuildingBlocks.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var validationsResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failiures = validationsResults
            .Where(vr => vr.Errors.Any())
            .SelectMany(vr => vr.Errors)
            .ToList();

        if (failiures.Any())
            throw new ValidationException(failiures);

        return await next();
    }
}
