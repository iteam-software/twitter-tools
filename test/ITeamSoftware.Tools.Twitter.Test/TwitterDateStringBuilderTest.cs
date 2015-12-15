using ITeamSoftware.Tools.Twitter.Services;
using System;
using Xunit;

namespace ITeamSoftware.Tools.Twitter.Test
{
    // see example explanation on xUnit.net website:
    // https://xunit.github.io/docs/getting-started-dnx.html
    public class TwitterDateStringBuilderTest
    {
        public TwitterDateStringBuilder TwitterDateService { get; } = new TwitterDateStringBuilder();

        [Fact]
        public void TwitterDate_GreaterThanYear()
        {
            // setup
            var date = DateTime.Now.AddDays(-370);
            var tweet = new TwitterTweet { created_at = date.ToString(TwitterDateStringBuilder.TwitterDateFormat) };

            // act
            var converted = TwitterDateService.Build(tweet);

            // assert
            Assert.Equal(date.ToString("d MMM yyyy"), converted);
        }

        [Fact]
        public void TwitterDate_LessThanYear_GreaterThanDay()
        {
            // setup
            var date = DateTime.Now.AddDays(-40);
            var tweet = new TwitterTweet { created_at = date.ToString(TwitterDateStringBuilder.TwitterDateFormat) };

            // act
            var converted = TwitterDateService.Build(tweet);

            // assert
            Assert.Equal(date.ToString("MMM d"), converted);
        }

        [Fact]
        public void TwitterDate_LessThanDay_GreaterThanHour()
        {
            // setup
            var date = DateTime.Now.AddHours(-2);
            var now = DateTime.Now;
            var tweet = new TwitterTweet { created_at = date.ToString(TwitterDateStringBuilder.TwitterDateFormat) };

            // act
            var converted = TwitterDateService.Build(tweet);

            // assert
            Assert.Equal((now - date).ToString("%h") + 'h', converted);
        }

        [Fact]
        public void TwitterDate_LessThanHour_GreaterThanMinute()
        {
            // setup
            var date = DateTime.Now.AddMinutes(-2);
            var now = DateTime.Now;
            var tweet = new TwitterTweet { created_at = date.ToString(TwitterDateStringBuilder.TwitterDateFormat) };

            // act
            var converted = TwitterDateService.Build(tweet);

            // assert
            Console.WriteLine(converted);
            Assert.Equal((now - date).ToString("%m") + 'm', converted);
        }

        [Fact]
        public void TwitterDate_LessThanMinute()
        {
            // setup
            var date = DateTime.Now.AddSeconds(-10);
            var now = DateTime.Now;
            var tweet = new TwitterTweet { created_at = date.ToString(TwitterDateStringBuilder.TwitterDateFormat) };

            // act
            var converted = TwitterDateService.Build(tweet);

            // assert
            Assert.Equal((now - date).ToString("%s") + 's', converted);
        }
    }
}
