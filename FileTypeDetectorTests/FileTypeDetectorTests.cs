using System.Reflection;
using FileTypeDetect;
namespace FileTypeDetectorTests;

[TestClass]
public class FileTypeDetectorTests
{
    const string TestDocumentsFolderName = "FileTypeDetectTests.TestDocuments";

    [TestMethod]
    public void TestDocxFileIsRecognized()
    {
        this.AssertFileIsOfType("Test.docx", "docx", "zip");
    }

    [TestMethod]
    public void TestXlsxFileIsRecognized()
    {
        this.AssertFileIsOfType("Test.xlsx", "xlsx", "zip");
    }

    [TestMethod]
    public void TestPptxFileIsRecognized()
    {
        this.AssertFileIsOfType("Test.pptx", "pptx", "zip");
    }

    [TestMethod]
    public void TestPdfFileIsRecognized()
    {
        this.AssertFileIsOfType("Test.pdf", "pdf");
    }

    [TestMethod]
    public void TestTxtFileIsRecognized()
    {
        this.AssertFileIsOfType("Test.txt", "txt");
    }

    [TestMethod]
    public void TestZipFileIsRecognized()
    {
        this.AssertFileIsOfType("Test.zip", "zip");
    }

        // TODO: Test file types
        // tar
        // dmg
        // iso
        // xml
        //mxf
        // ts
        // empty file

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
        else
        {
            throw new InvalidOperationException("The stream is not supposed to be null");
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