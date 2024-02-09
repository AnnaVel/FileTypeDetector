namespace FileTypeDetect
{
    public class FileTypeDetector
    {
        private readonly IEnumerable<IFileTypeChecker> checkers;

        public FileTypeDetector()
        : this(new FileTypeCheckersListBuilder())
        {
        }

        public FileTypeDetector(IFileTypeCheckersListBuilder builder)
        {
            this.checkers = builder.BuildFileTypeCheckers();
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
    }
}
