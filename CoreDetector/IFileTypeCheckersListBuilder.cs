namespace FileTypeDetect;

public interface IFileTypeCheckersListBuilder
{
    IEnumerable<IFileTypeChecker> BuildFileTypeCheckers();
}
