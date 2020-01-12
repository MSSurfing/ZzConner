namespace Zz.Services.Installers
{
    public partial interface IInstallerService
    {
        void InitializeDatabase();
        void InitializeSampleData();

        void InstallManager(string username, string password);
    }
}
