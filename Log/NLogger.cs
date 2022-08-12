namespace TechSupportHelpSystem.Log
{
    public static class NLogger
    {
        public static NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
    }
}
