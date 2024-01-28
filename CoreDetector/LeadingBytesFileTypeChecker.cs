namespace FileTypeDetect;

internal class LeadingBytesFileTypeChecker : IFileTypeChecker
{
    private readonly byte?[] leadingBytes;

    public LeadingBytesFileTypeChecker(FileType fileType, int offset, byte?[] leadingBytes)
    : this(fileType, leadingBytes)
    {
        this.Offset = offset;
    }

    public LeadingBytesFileTypeChecker(FileType fileType, byte?[] leadingBytes)
    {
        Utils.ThrowExceptionIfNulll(fileType, nameof(fileType));
        Utils.ThrowExceptionIfNulll(leadingBytes, nameof(leadingBytes));

        this.FileType = fileType;
        this.leadingBytes = leadingBytes;
    }

    public FileType FileType { get; }

    public int Offset { get; }

    public virtual bool IsByteArrayOfMyType(byte[] bytes)
    {
        for (int i = 0; i < leadingBytes.Length; i++)
        {
            if(this.leadingBytes[i] != null && (bytes[i + this.Offset] != this.leadingBytes[i]))
            {
                return false;
            }
        }

        return true;
    }
}