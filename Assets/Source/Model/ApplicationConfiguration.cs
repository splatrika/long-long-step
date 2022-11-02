namespace Splatrika.LongLongStep.Model
{
    public class ApplicationConfiguration
    {
        public string GitHubUrl { get; set; }
        public string Version { get; set; }

        public ApplicationConfiguration(string gitHubUrl, string version)
        {
            GitHubUrl = gitHubUrl;
            Version = version;
        }
    }
}
