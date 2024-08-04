namespace Cryptonly
{
    public class LanguageInfo
    {
        public string Code { get; }
        public string LocalizedName { get; }
        public Uri ResourceUri { get; }

        public LanguageInfo(string code, string localizedName, Uri resourceUri)
        {
            Code = code;
            LocalizedName = localizedName;
            ResourceUri = resourceUri;
        }
    }
}