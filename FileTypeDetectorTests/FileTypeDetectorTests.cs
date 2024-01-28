using System.Reflection;
using FileTypeDetect;
namespace FileTypeDetectorTests;

[TestClass]
public class FileTypeDetectorTests
{
    const string TestDocumentsFolderName = "FileTypeDetectorTests.TestDocuments";

    [TestMethod]
    public void TestDocxFileIsRecognized()
    {
        this.AssertFileIsOfType("Test.docx", "docx", "zip");
        this.AssertFileIsOfType("Test.xlsx", "xlsx", "zip");
        this.AssertFileIsOfType("Test.pptx", "pptx", "zip");
        this.AssertFileIsOfType("Test.pdf", "pdf");
        this.AssertFileIsOfType("Test.txt", "txt");
        this.AssertFileIsOfType("Test.zip", "zip");
    }

    public void AssertFileIsOfType(string fileName, params string[] expectedExtensions)
    {
        fileName = string.Format("{0}.{1}", TestDocumentsFolderName, fileName);  

        var assembly = Assembly.GetExecutingAssembly();

        Stream? stream = assembly.GetManifestResourceStream(fileName);
        byte[] bytes = new byte[0];
        if(stream != null)
        {
            bytes = GetBytesFromStream(stream);
        }

        FileTypeDetector detector = new FileTypeDetector();
        IEnumerable<FileType> results = detector.GetFileTypesFromByteArray(bytes);

        string[] resultExtensions = (from result in results
        from extension in result.Extensions
        select extension).ToArray();

        Assert.AreEqual(expectedExtensions.Count(), resultExtensions.Count());

        for (int i = 0; i < expectedExtensions.Length; i++)
        {
            string? expectedExtension = expectedExtensions[i];
            string resultExtension = resultExtensions[i];
            Assert.AreEqual(expectedExtension, resultExtension);
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