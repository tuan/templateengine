using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemplateEngine.Controllers;
using TemplateEngine.Models;

namespace TemplateEngine.Tests
{
    [TestClass]
    public class ControllerTests
    {
        private string baseAddress = "http://localhost:9000";

        [TestMethod]
        public void GetMethodReturnsString()
        {
            var renderController = new RenderController();
            var template = "Hello, @Model.FirstName";
            var firstName = "Tuan";
            var expectedResult = string.Format(CultureInfo.InvariantCulture, "Hello, {0}", firstName);

            var renderingData = new RenderingData()
            {
                Template = template,
                Model = new { FirstName = firstName}
            };

            var response = renderController.Post(renderingData);

            Assert.IsNotNull(response);

            //TODO, verify expected render result
        }
    }
}
