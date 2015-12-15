namespace ITeamSoftware.Tools.Twitter
{
    public class TwitterTweet
    {
        public string text { get; set; }
        public string id_str { get; set; }
        public string created_at { get; set; }
        public TwitterUser user { get; set; }
        public bool retweeted { get; set; }
        public TwitterTweet retweeted_status { get; set; }
    }
}
