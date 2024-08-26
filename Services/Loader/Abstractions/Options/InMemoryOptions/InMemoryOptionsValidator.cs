using Microsoft.Extensions.Options;

namespace Loader.Abstractions.Options.InMemoryOptions;

internal sealed class InMemoryOptionsValidator : IValidateOptions<InMemoryOptions>
{
    public ValidateOptionsResult Validate(string? name, InMemoryOptions options)
    {
        return ValidateOptionsResult.Success;
    }
}
