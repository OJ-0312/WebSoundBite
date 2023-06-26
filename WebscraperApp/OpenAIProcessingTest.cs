using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebscraperApp.Tests
{
    public class OpenAIProcessingTest
    {
        [Theory]
        [InlineData("What is artificial intelligence")]
        public async Task OpenAIProcessingTester(string ChatInput)
        {
            bool expected = true;
            bool actual;

            string result = await OpenAIProcessing.OpenAIProcesser(ChatInput);


            if (result == null)
            {
                actual = false;
            }
            else
            {
                actual = true;
            }

            Assert.Equal(expected, actual);
        }


    }
}
