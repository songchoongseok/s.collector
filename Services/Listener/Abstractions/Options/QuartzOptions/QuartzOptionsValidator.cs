using Microsoft.Extensions.Options;

namespace Listener.Abstractions.Options.QuartzOptions;

internal sealed class QuartzOptionsValidator : IValidateOptions<QuartzOptions>
{
    public ValidateOptionsResult Validate(string? name, QuartzOptions options)
    {
        List<string> errors = [];

        if (options.Seconds < 5 || options.Seconds > 3600)
            errors.Add("The option value can't be less than 5 seconds or greater than 3,600 seconds.");

        if (errors.Any())
            return ValidateOptionsResult.Fail(errors);

        return ValidateOptionsResult.Success;
    }
}
