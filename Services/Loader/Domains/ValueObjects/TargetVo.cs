using static Loader.Abstractions.Constants.Constants;

namespace Loader.Domains.ValueObjects;

internal sealed record class TargetVo(string TargetFullName)
{

    internal string TargetPath => Path.GetDirectoryName(TargetFullName)!;
    internal string TargetName => Path.GetFileName(TargetFullName);
    internal IList<string> Directories => TargetPath.Split(Symbol.Slash);
}
