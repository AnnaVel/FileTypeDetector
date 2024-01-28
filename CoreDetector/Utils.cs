namespace FileTypeDetect;

internal static class Utils
{
    public static void ThrowExceptionIfNulll(object argument, string argumentName)
    {
        if(argument == null)
        {
            throw new ArgumentException(string.Format("{0} cannot be null", argumentName));
        }
    }

    public static void ThrowExceptionIfNullOrEmpty<T>(IEnumerable<T> argument, string argumentName)
    {
        if(argument == null)
        {
            throw new ArgumentException(string.Format("{0} cannot be null", argumentName));
        }

        if(argument.Count() == 0)
        {
            throw new ArgumentException(string.Format("{0} cannot be empty", argumentName));
        }
    }

    public static byte[] GetBytesFromStream(Stream stream)
    {
        MemoryStream ms = new MemoryStream();
        stream.CopyTo(ms);
        byte[] bytes = ms.ToArray();
        return bytes;
    }
}