using Loader.Domains.ValueObjects;

namespace Loader.Applications.GetTargetFiles;

internal interface IFtpClient
{
    internal Task<IEnumerable<FtpFileVo>> GetAllFilesFromAsync(TargetVo targetVo, CancellationToken cancellationToken);
}
