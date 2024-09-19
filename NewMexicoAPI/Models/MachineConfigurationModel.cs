namespace NewMexicoAPI.Models
{
    public class MachineConfigurationModel
    {
        public string MachineName { get; set; }
        public string ClientIpAddress { get; set; }
        public string LocalIpAddress { get; set; }
        public string HostName { get; set; }
        public string ProcessorName { get; set; }
        public string InstalledRam { get; set; }
        public string DeviceId { get; set; }
        public string ProductId { get; set; }
        public string SystemType { get; set; }
        public string Edition { get; set; }
        public string Version { get; set; }
        public string OSBuild { get; set; }
        public string SubnetMask { get; set; }

    }

    public class XForwordForModel
    {
        public string X_Forwarded_For { get; set; }
        public string ProxyIPAddress { get; set; }
        public string? ALL_HTTP { get; set; }
        public string? ALL_RAW { get; set; }
        public string? APPL_MD_PATH { get; set; }
        public string? APPL_PHYSICAL_PATH { get; set; }
        public string? APP_POOL_CONFIG { get; set; }
        public string? APP_POOL_ID { get; set; }
        public string? AUTH_PASSWORD { get; set; }
        public string? AUTH_TYPE { get; set; }
        public string? AUTH_USER { get; set; }
        public string? CACHE_URL { get; set; }
        public string? CERT_COOKIE { get; set; }
        public string? CERT_FLAGS { get; set; }
        public string? CERT_ISSUER { get; set; }
        public string? CERT_KEYSIZE { get; set; }
        public string? CERT_SECRETKEYSIZE { get; set; }
        public string? CERT_SERIALNUMBER { get; set; }
        public string? HTTPS { get; set; }
        public string? HTTPS_KEYSIZE { get; set; }
        public string? HTTPS_SECRETKEYSIZE { get; set; }
        public string? HTTPS_SERVER_ISSUER { get; set; }
        public string? HTTPS_SERVER_SUBJECT { get; set; }
        public string? HTTP_METHOD { get; set; }
        public string? HTTP_URL { get; set; }
        public string? HTTP_VERSION { get; set; }
        public string? INSTANCE_ID { get; set; }
        public string? INSTANCE_META_PATH { get; set; }
        public string? INSTANCE_NAME { get; set; }
        public string? LOCAL_ADDR { get; set; }
        public string? LOGON_USER { get; set; }
        public string? MANAGED_PIPELINE_MODE { get; set; }
        public string? PATH_INFO { get; set; }
        public string? PATH_TRANSLATED { get; set; }
        public string? QUERY_STRING { get; set; }
        public string? REMOTE_ADDR { get; set; }
        public string? REMOTE_HOST { get; set; }
        public string? REMOTE_PORT { get; set; }
        public string? REMOTE_USER { get; set; }
        public string? REQUEST_FILENAME { get; set; }
        public string? REQUEST_FLAGS { get; set; }
        public string? REQUEST_METHOD { get; set; }
        public string? REQUEST_URI { get; set; }
        public string? SCRIPT_FILENAME { get; set; }
        public string? SCRIPT_NAME { get; set; }
        public string? SCRIPT_TRANSLATED { get; set; }
        public string? SERVER_ADDR { get; set; }
        public string? SERVER_NAME { get; set; }
        public string? SERVER_PORT { get; set; }
        public string? SERVER_PROTOCOL { get; set; }
        public string? SERVER_PORT_SECURE { get; set; }
        public string? SERVER_SOFTWARE { get; set; }
        public string? UNENCODED_URL { get; set; }
        public string? UNMAPPED_REMOTE_USER { get; set; }
        public string? URL { get; set; }
        public string? CERT_SERVER_ISSUER { get; set; }
        public string? CERT_SERVER_SUBJECT { get; set; }
        public string? CERT_SUBJECT { get; set; }
        public string? CONTENT_LENGTH { get; set; }
        public string? CONTENT_TYPE { get; set; }
        public string? CRYPT_CIPHER_ALG_ID { get; set; }
        public string? CRYPT_HASH_ALG_ID { get; set; }
        public string? CRYPT_KEYEXCHANGE_ALG_ID { get; set; }
        public string? CRYPT_PROTOCOL { get; set; }
        public string? DOCUMENT_ROOT { get; set; }
        public string? FORWARDED_URL { get; set; }
        public string? GATEWAY_INTERFACE { get; set; }
    }
}
