using System.IO.Compression;

namespace FileTypeDetect;

internal class ZipFileTypeChecker : LeadingBytesFileTypeChecker
{
    string entryToLookFor;
    private static readonly byte?[] ZipMagicBytes = new byte?[] {0x50, 0x4B};

    public ZipFileTypeChecker(FileType fileType, string entryToLookFor)
        :base(fileType, ZipMagicBytes)
    {
        this.entryToLookFor = entryToLookFor;
        Utils.ThrowExceptionIfNulll(fileType, nameof(fileType));
    }

    public override bool IsByteArrayOfMyType(byte[] bytes)
    {
        if(!base.IsByteArrayOfMyType(bytes))
        {
            return false;
        }

        MemoryStream memoryStream = new MemoryStream(bytes);
        using (ZipArchive archive = new ZipArchive(memoryStream))
        {
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if(entry.FullName.Contains(this.entryToLookFor))
                {
                    return true;
                }
            }
        }

        return false;
    }
}