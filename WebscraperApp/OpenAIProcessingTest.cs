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
        /* This just checks if the openAI_API interaction works. 
         * It sends a string to the API and checks if it returns a string.
         * Be careful as this test uses openAI API tokens.
         */
        [Theory]
        [InlineData("What is artificial intelligence")]
        public async Task OpenAIProcessingTester(string chatInput)
        {
            bool expected = true;
            bool actual;

            string result = await OpenAIProcessing.OpenAIProcesser(chatInput);


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
