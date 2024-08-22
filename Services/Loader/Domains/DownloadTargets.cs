using Loader.Domains.ValueObjects;

namespace Loader.Domains.Downloads;

public class DownloadTargets(long id, string name, FtpInfo ftpInfo)
{
    private List<FileVo> _targets = [];

    public long Id { get; } = id;
    public string Name { get; } = name;
}
