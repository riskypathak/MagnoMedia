namespace MagnoMedia.Data.Models
{
    public enum AppInstallState
    {
        NotInstalledDueToOverLimit,
        AlreadyExist,
        DownloadStart,
        InstallStart,
        Success,
        Failure,
    }
}
