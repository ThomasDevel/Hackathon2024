namespace Hackathon.Tests
{
    using HackathonTemplating;
    using System;
    using Xunit;

    public class TestOutcomeParsing
    {
        [Theory]
        [InlineData("500 5.2.2 I don't contain any addresses so change nothing.",
                   "zuppydog@meme.com",
                   "500 5.2.2 I don't contain any addresses so change nothing.")]
        [InlineData("500 5.2.2 <zuppydog@meme.com>: Recipient address rejected: <zuppydog@meme.com> Quota exceeded",
                    "zuppydog@meme.com",
                    "500 5.2.2 <[recipient]>: Recipient address rejected: <[recipient]> Quota exceeded")]
        [InlineData("500 5.2.2 <zuppydog@meme.com>: Recipient address rejected: <zuppydog@meme.com> Quota exceeded, contact postmaster at postmaster@posty.com",
                    "zuppydog@meme.com",
                    "500 5.2.2 <[recipient]>: Recipient address rejected: <[recipient]> Quota exceeded, contact postmaster at postmaster@posty.com")]
        [InlineData("contact pm@selligent.com to continue with user@selligent.com",
                    "user@selligent.com",
                    "contact pm@selligent.com to continue with [recipient]")]
        [InlineData("pm@selligent.com user@selligent.com pm@selligent.com user@selligent.com pm@selligent.com user@selligent.com pm@selligent.com user@selligent.com",
                    "user@selligent.com",
                    "pm@selligent.com [recipient] pm@selligent.com [recipient] pm@selligent.com [recipient] pm@selligent.com [recipient]")]
        public void GivenAValidSmtpOutcome_WhenParsing_RemoveRecipientInformationFromOutcomeIfPresent(String outcomeString, String recipient, String censoredOutcome)
        {
            var result = SmtpCensorer.TryCensorRecipient(outcomeString, recipient);

            Assert.Equal(censoredOutcome, result.ToString());
        }
    }
}
