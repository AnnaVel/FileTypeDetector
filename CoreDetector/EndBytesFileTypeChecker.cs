namespace FileTypeDetect;

internal abstract class EndBytesFileTypeChecker : IFileTypeChecker
{
    public EndBytesFileTypeChecker(FileType fileType, int offset, byte?[] magicBytes)
    {
        Utils.ThrowExceptionIfNulll(fileType, nameof(fileType));
        Utils.ThrowExceptionIfNulll(magicBytes, nameof(magicBytes));

        this.Offset = offset;
        this.FileType = fileType;
        this.MagicBytes = magicBytes;
    }

    public FileType FileType { get; }

    public int Offset { get; }

    protected byte?[] MagicBytes {get;}

    public abstract bool IsByteArrayOfMyType(byte[] bytes);
}
