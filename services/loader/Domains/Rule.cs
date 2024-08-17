namespace Domains;

public sealed class Rule(int id, string name, FtpInfo ftpInfo)
{
    private List<string> _targets = [];

    public int Id { get; } = id;
    public string Name { get; } = name;
    public FtpInfo FtpInfo { get; } = ftpInfo;

    public void Add(string target)
        => _targets.Add(target);
} 