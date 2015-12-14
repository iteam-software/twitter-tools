using ITeamSoftware.Tools.Twitter.DataTransferObjects;
using System.Threading.Tasks;

namespace ITeamSoftware.Tools.Twitter.Services
{
    public interface ITwitterApplicationOnlyService
    {
        Task<string> GetTokenAsync();
        Task<TwitterTweet[]> GetTimeline(string token, string screenName);
    }
}
