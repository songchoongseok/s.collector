using Loader.Domains.ValueObjects;

namespace Loader.Domains.Downloads;

internal sealed class Rule(long id, string name, FtpInfoVo ftpInfoVo)
{
    private List<TargetVo> _targets = [];

    internal long Id { get; } = id;
    internal string Name { get; } = name;
    internal FtpInfoVo FtpInfoVo { get; } = ftpInfoVo;

    internal void AddFile(TargetVo targetVo)
        => _targets.Add(targetVo);

    internal int Count() => _targets.Count;
}
