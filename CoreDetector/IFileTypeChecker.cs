namespace FileTypeDetect;

public interface IFileTypeChecker
{
    FileType FileType { get; }
    bool IsByteArrayOfMyType(byte[] bytes);
}