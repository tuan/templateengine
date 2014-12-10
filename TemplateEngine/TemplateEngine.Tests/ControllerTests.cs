using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemplateEngine.Controllers;

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
            
            var result = renderController.Get();

            Assert.AreEqual(result, "fake result");
        }
    }
}
