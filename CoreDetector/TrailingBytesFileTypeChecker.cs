namespace FileTypeDetect;

internal class TrailingBytesFileTypeChecker : EndBytesFileTypeChecker
{
    public TrailingBytesFileTypeChecker(FileType fileType, int offset, byte?[] trailingBytes)
        :base(fileType, offset, trailingBytes)
    {
    }

    public override bool IsByteArrayOfMyType(byte[] bytes)
    {
        if(this.MagicBytes.Length > bytes.Length || this.Offset > bytes.Length)
        {
            return false;
        }

        for (int i = 0; i < MagicBytes.Length; i++)
        {
            if(this.MagicBytes[i] != null &&
            (bytes[bytes.Length - this.Offset + i] != this.MagicBytes[i]))
            {
                return false;
            }
        }

        return true;
    }
}
