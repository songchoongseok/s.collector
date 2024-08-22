namespace Loader.Domains.ValueObjects;

public record class FileVo(string FileFullName)
{
    public string FilePath => Path.GetDirectoryName(FileFullName)!;
    public string FileName => Path.GetFileName(FileFullName);
    public IList<string> Directories => FilePath.Split('/');
}
