using System.Threading.Tasks;

namespace ITeamSoftware.Tools.Twitter
{
    public interface ITwitterApplicationOnlyService
    {
        Task<string> GetTokenAsync();
        Task<TwitterTweet[]> GetTimeline(string token, string screenName);
    }
}
