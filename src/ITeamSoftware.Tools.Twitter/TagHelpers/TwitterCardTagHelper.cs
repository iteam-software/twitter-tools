using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.TagHelpers;

namespace ITeamSoftware.Tools.Twitter.TagHelpers
{
    [HtmlTargetElement("div", Attributes = TwitterCardAttributeName)]
    public class TwitterCardTagHelper : TagHelper
    {
        private readonly ITwitterDateStringBuilder _dateBuilder;
        private const string TwitterCardAttributeName = "twitter-card";

        [HtmlAttributeName(TwitterCardAttributeName)]
        public TwitterTweet Tweet { get; set; }

        public TwitterCardTagHelper(ITwitterDateStringBuilder dateBuilder)
        {
            _dateBuilder = dateBuilder;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Tweet == null)
            {
                return;
            }

            // resolve screen name
            var screenNameText = Tweet.retweeted_status?.user.screen_name ?? Tweet.user.screen_name;

            var card = new TagBuilder("div");
            card.AddCssClass("twitter-card");

            var logo = new TagBuilder("span");
            logo.AddCssClass("twitter-logo");
            logo.AddCssClass("fa");
            logo.AddCssClass("fa-twitter");

            var profileImage = new TagBuilder("img");
            profileImage.AddCssClass("img-rounded");
            profileImage.Attributes.Add("src", Tweet.retweeted_status?.user.profile_image_url ?? Tweet.user.profile_image_url );

            var screenName = new TagBuilder("span");
            var screenNameLink = new TagBuilder("a");
            screenNameLink.Attributes.Add("href", $"https://twitter.com/{screenNameText}");
            screenNameLink.Attributes.Add("target", "_blank");
            screenNameLink.InnerHtml.Append(screenNameText);
            screenName.InnerHtml.Append(screenNameLink);

            var text = new TagBuilder("p");
            text.InnerHtml.Append(Tweet.text);

            var date = new TagBuilder("span");
            date.AddCssClass("twitter-date");
            var dateLink = new TagBuilder("a");
            dateLink.Attributes.Add("href", $"https://twitter.com/{screenNameText}/status/{Tweet.id_str}");
            dateLink.Attributes.Add("target", "_blank");
            dateLink.InnerHtml.Append(_dateBuilder.Build(Tweet));
            date.InnerHtml.Append(dateLink);

            card.InnerHtml.Append(logo);
            card.InnerHtml.Append(profileImage);
            card.InnerHtml.Append(screenName);
            card.InnerHtml.Append(text);
            card.InnerHtml.Append(date);

            output.Content.Append(card);
        }
    }
}
