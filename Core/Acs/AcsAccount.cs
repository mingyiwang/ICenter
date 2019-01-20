namespace Core.Acs
{

    public sealed class AcsAccount
    {

        public string AccessAccount { get; }

        public string PrivateKey { get; }

        private AcsAccount(string account, string key)
        {
            AccessAccount = account;
            PrivateKey = key;
        }

        public static AcsAccount Of(string account, string key)
        {
            return new AcsAccount(account, key);
        }

    }

}
