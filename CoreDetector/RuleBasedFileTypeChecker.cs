namespace FileTypeDetect;

public class RuleBasedFileTypeChecker : IFileTypeChecker
{
    private readonly Func<byte[], bool> rule;

    public RuleBasedFileTypeChecker(FileType fileType, Func<byte[], bool> rule )
    {
        Utils.ThrowExceptionIfNulll(fileType, nameof(fileType));
        Utils.ThrowExceptionIfNulll(rule, nameof(rule));

        this.rule = rule;
        this.FileType = fileType;
    }

    public FileType FileType { get; }

    public bool IsByteArrayOfMyType(byte[] bytes)
    {
        return this.rule(bytes);
    }
}
