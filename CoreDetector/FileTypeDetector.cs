namespace FileTypeDetect
{
    public class FileTypeDetector
    {
        private readonly IEnumerable<IFileTypeChecker> checkers;

        public FileTypeDetector()
        {
            this.checkers = this.BuildCheckers();
        }

        public IEnumerable<FileType> GetFileTypesFromByteArray(byte[] bytes)
        {
            foreach(IFileTypeChecker checker in this.checkers)
            {
                if(checker.IsByteArrayOfMyType(bytes))
                {
                    yield return checker.FileType;
                }
            }
        }

        /// <summary>
        /// Returns a single file type instead of a collection. E.g. a docx file is both a docx and a zip file. This will return only docx.
        /// </summary>
        /// <param name="bytes">The byte representation of the file.</param>
        /// <returns>The result file type.</returns>
        public FileType GetSingleFileTypeFromByteArray(byte[] bytes)
        {
            foreach(IFileTypeChecker checker in this.checkers)
            {
                if(checker.IsByteArrayOfMyType(bytes))
                {
                    return checker.FileType;
                }
            }

            return FileType.Empty;
        }

        /// <summary>
        /// Override this if you would like to extend the list of file types that can be checked for.
        /// </summary>
        /// <returns>A list of FileTypeCheckers that will be used to check files for their file type.</returns>
        protected IEnumerable<IFileTypeChecker> BuildCheckers()
        {
            return new List<IFileTypeChecker>(){
                new LeadingBytesFileTypeChecker(new FileType("", "pdf"), new byte?[]{0x25, 0x50, 0x44, 0x46, 0x2D}),
                new LeadingBytesFileTypeChecker(new FileType("", "txt"), new byte?[]{0xEF, 0xBB, 0xBF}),
                new LeadingBytesFileTypeChecker(new FileType("", "txt"), new byte?[]{0xFF, 0xFE}),
                new LeadingBytesFileTypeChecker(new FileType("", "txt"), new byte?[]{0xFE, 0xFF}),
                new LeadingBytesFileTypeChecker(new FileType("", "txt"), new byte?[]{0xFF, 0xFE, 0x00, 0x00}),
                new LeadingBytesFileTypeChecker(new FileType("", "txt"), new byte?[]{0x00, 0x00, 0xFE, 0xFF}),
                new LeadingBytesFileTypeChecker(new FileType("", "txt"), new byte?[]{0x0E, 0xFE, 0xFF}),
                new ZipFileTypeChecker(new FileType("", "docx"), "word/document.xml"),
                new ZipFileTypeChecker(new FileType("", "xlsx"), "xl/workbook.xml"),
                new ZipFileTypeChecker(new FileType("", "pptx"), "ppt/presentation.xml"),
                new LeadingBytesFileTypeChecker(new FileType("", "zip"), new byte?[]{0x50, 0x4B}),
            };
        }
    }
}
