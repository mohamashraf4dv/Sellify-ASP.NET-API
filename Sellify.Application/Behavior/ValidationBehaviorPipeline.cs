namespace Sellify.Application.Behavior
{
    public class ValidationBehaviorPipeline<TRequest,TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse: GenericResultDTO
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviorPipeline(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // 1)validate request -> 2)if any errors return  validation result -> 3)otherwise  return next

            if (!_validators.Any()) {
                return await next();
            }
            var validationContext = new ValidationContext<TRequest>(request);
            var results = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(validationContext, cancellationToken)));
            var failures = results.SelectMany(e => e.Errors).GroupBy(e => e.PropertyName).ToDictionary(e => GetKeyName(e.Key), x => x.Select(e => e.ErrorMessage).ToHashSet());
            if (failures.Count > 0)
                return (TResponse) new GenericResultDTO(null, 400, failures);
            return await next();
        }

        //used for getting property name as it's nested inside a DTO Object
        private string GetKeyName(string property)
        {
            if (property.Contains('.'))
            {
                string[] namesInArray = property.Split('.');
                return namesInArray.Last();
            }
            return property;

        }
    }
}
