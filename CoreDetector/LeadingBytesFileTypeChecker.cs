namespace FileTypeDetect;

internal class LeadingBytesFileTypeChecker : EndBytesFileTypeChecker
{
    public LeadingBytesFileTypeChecker(FileType fileType, int offset, byte?[] leadingBytes)
        :base(fileType, offset, leadingBytes)
    {
    }

    public LeadingBytesFileTypeChecker(FileType fileType, byte?[] leadingBytes)
        : this(fileType, 0, leadingBytes)
    {
    }

    public override bool IsByteArrayOfMyType(byte[] bytes)
    {
        if(this.Offset + this.MagicBytes.Length > bytes.Length)
        {
            return false;
        }

        for (int i = 0; i < MagicBytes.Length; i++)
        {
            if(this.MagicBytes[i] != null &&
            (bytes[i + this.Offset] != this.MagicBytes[i]))
            {
                return false;
            }
        }

        return true;
    }
}