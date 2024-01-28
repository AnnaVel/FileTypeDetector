namespace FileTypeDetect;

public class FileType
{
    public static readonly FileType Empty = new FileType("Empty filetype", new string[0]); 

    internal FileType(string description, params string[] extensions)
    {
        Utils.ThrowExceptionIfNulll(extensions, nameof(extensions));
        Utils.ThrowExceptionIfNulll(description, nameof(description));

        this.Extensions = extensions;
        this.Description = description;
    }

    public IEnumerable<string> Extensions { get; }

    public string Description { get; }
}