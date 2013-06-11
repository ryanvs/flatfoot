namespace ExceptionHandler.Managers
{
    public interface IAssemblyInfo
    {
        string FullName { get; }
        string CodeBase { get; }
        System.Version Version { get; }
    }
}