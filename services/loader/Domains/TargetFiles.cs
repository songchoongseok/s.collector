using System.Collections.Generic;

namespace Domains;

internal sealed class TargetFiles(int id, string name)
{
    private List<string> _files = [];
    
    public int Id { get; } = id;
    public string Name {get;} = name;

    public void Add(string file)
        => _files.Add(file);
}