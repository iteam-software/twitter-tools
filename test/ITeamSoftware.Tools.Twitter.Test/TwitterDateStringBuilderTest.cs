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
            var date = new DateTime(2012, 1, 15).ToUniversalTime();
            var tweet = new TwitterTweet { created_at = date.ToString(TwitterDateStringBuilder.TwitterDateFormat) };

            // act
            var converted = TwitterDateService.Build(tweet);

            // assert
            Assert.Equal("15 Jan 2012", converted);
        }

        [Fact]
        public void TwitterDate_LessThanYear_GreaterThanDay()
        {
            // setup
            var date = DateTime.UtcNow.AddDays(-40);
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
            var date = DateTime.UtcNow.AddHours(-2);
            var tweet = new TwitterTweet { created_at = date.ToString(TwitterDateStringBuilder.TwitterDateFormat) };

            // act
            var converted = TwitterDateService.Build(tweet);

            // assert
            Assert.Equal("2h", converted);
        }

        [Fact]
        public void TwitterDate_LessThanHour_GreaterThanMinute()
        {
            // setup
            var date = DateTime.UtcNow.AddMinutes(-2);
            var tweet = new TwitterTweet { created_at = date.ToString(TwitterDateStringBuilder.TwitterDateFormat) };

            // act
            var converted = TwitterDateService.Build(tweet);

            // assert
            Console.WriteLine(converted);
            Assert.Equal("2m", converted);
        }

        [Fact]
        public void TwitterDate_LessThanMinute()
        {
            // setup
            var date = DateTime.UtcNow.AddSeconds(-10);
            var tweet = new TwitterTweet { created_at = date.ToString(TwitterDateStringBuilder.TwitterDateFormat) };

            // act
            var converted = TwitterDateService.Build(tweet);

            // assert
            Assert.Equal("10s", converted);
        }
    }
}
