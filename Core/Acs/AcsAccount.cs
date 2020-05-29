namespace Core.Acs
{

    public sealed class AcsAccount
    {

        public string AccessKeyId { get; }

        public string AccessKey   { get; }

        public static AcsAccount Of(string accessKeyId, string accessKey)
        {
            return new AcsAccount(accessKeyId, accessKey);
        }

        private AcsAccount(string keyId, string key)
        {
            AccessKeyId = keyId;
            AccessKey   = key;
        }
    }

}
