using FluentValidation;
using MediatR;

namespace Domain.Validations;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) =>
        _validators = validators;
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators is null || !_validators.Any())
            return await next();

        await _validators.First().ValidateAndThrowAsync(request, cancellationToken);

        return await next();
    }
}
