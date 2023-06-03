namespace tpm.business
{
    public class ApiConfig
    {
        public static CommonConfig Common;
        public static ConnectionStrings Connection;
        public static URLConnectionConfig URLConnection;
    }

    public class CommonConfig
    {
        public string ClientName { get; set; }
        public string Environment { get; set; }
        public int PageSizeMaxValue { get; set; }
        public int ExcelRecordMaxValue { get; set; }
        public int SQLInsertMaxRow { get; set; }
        public bool DisableAuthen { get; set; }
    }
    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
        public string ConnectionStringMDM { get; set; }
        public string ConnectionStringSCM { get; set; }
        public string ConnectionStringPCS { get; set; }
    }
    public class URLConnectionConfig
    {
        public string FileHandlerUrl { get; set; }
    }
}

