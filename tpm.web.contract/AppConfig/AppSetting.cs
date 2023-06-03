namespace tpm.web.contract
{
    public class AppSetting
    {
        public static AppSettingLogger Logger;
    }
    public class AppSettingLogger
    {
        public bool SerilogEnable { get; set; }
        public string SeqURI { get; set; }
        public string ClientName { get; set; }
        public bool SerilogDebug { get; set; }
    }
}
