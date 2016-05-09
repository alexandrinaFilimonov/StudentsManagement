namespace StudentsManagement.Helpers
{
    public interface IPathProvider<TModel>
    {
        string GetPathToDownloadFrom();
    }
}
