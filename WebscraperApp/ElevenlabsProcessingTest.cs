using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebscraperApp.Tests
{
    public class ElevenlabsProcessingTest
    {
        [Theory]
        [InlineData("What is artificial intelligence")]
        public async Task ElevenlabsProcessingTester(string voiceInput)
        {
            bool expected = true;
            bool actual;

            string result = await ElevenlabsProcessing.ElevenlabsProcesser(voiceInput);


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
