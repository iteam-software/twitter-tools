using System;

namespace ITeamSoftware.Tools.Twitter
{
    public class TwitterDateStringBuilder : ITwitterDateStringBuilder
    {
        public static string TwitterDateFormat { get; } = "ddd MMM dd HH:mm:ss +ffff yyyy";

        public string Build(TwitterTweet tweet)
        {
            var date = DateTime.ParseExact(tweet.created_at, TwitterDateFormat, new System.Globalization.CultureInfo("en-US")).ToLocalTime();
            var diff = DateTime.Now - date;

            if (diff > TimeSpan.FromDays(365))
            {
                return date.ToString("d MMM yyyy");
            }
            else if (diff > TimeSpan.FromDays(1))
            {
                return date.ToString("MMM d");
            }
            else if (diff > TimeSpan.FromHours(1))
            {
                return diff.ToString("%h") + 'h';
            }
            else if (diff > TimeSpan.FromMinutes(1))
            {
                return diff.ToString("%m") + 'm';
            }
            else
            {
                return diff.ToString("%s") + 's';
            }
        }
    }
}
