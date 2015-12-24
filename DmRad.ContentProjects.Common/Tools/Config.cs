namespace DmRad.ContentProjects.Common.Tools
{
    class Config
    {
        public static bool DatabaseLogging => Utils.GetConfigValueByKey<bool>(Consts.DatabaseLogging);
    }
}
