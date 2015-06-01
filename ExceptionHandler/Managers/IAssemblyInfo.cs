namespace ExceptionHandler.Managers
{
    public interface IAssemblyInfo
    {
        string FullName { get; }
        string CodeBase { get; }
        string ProductName { get; }
        string ApplicationPath { get; }
        System.Version Version { get; }
        System.DateTime BuildDate { get; }
    }
}