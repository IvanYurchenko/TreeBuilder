using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Moq;
using System.IO;
using System.Collections.Generic;

namespace TreeBuilder.Tests
{
    [TestClass]
    public class TreeBuilderTests
    {
        public static ITreeBuilder TreeBuilder { get; set; }
        public static TestsHelper TestsHelper { get; set; }
        public const string ValidTreeString = "1, 2, 3 \r\n 2, 4, 5";

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            var kernel = new StandardKernel(new NinjectConfiguration());
            TreeBuilder = kernel.Get<ITreeBuilder>();
            TestsHelper = new TestsHelper();
        }

        // Positive tests
        [TestMethod]
        public void Calls_ValidateTree_Once()
        {
            var nodeHelperMock = TestsHelper.GetNodeHelperMock();
            var builder = new TreeBuilder(nodeHelperMock.Object);
            using (var stream = TestsHelper.CreateStream(ValidTreeString))
            {
                var reader = new StreamReader(stream);
                builder.BuildTree(reader);
            }

            nodeHelperMock.Verify(x =>
                x.ValidateTree(It.IsAny<List<Node>>(), It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void Calls_ValidateNodeNamesList()
        {
            var nodeHelperMock = TestsHelper.GetNodeHelperMock();
            var builder = new TreeBuilder(nodeHelperMock.Object);
            using (var stream = TestsHelper.CreateStream(ValidTreeString))
            {
                var reader = new StreamReader(stream);
                builder.BuildTree(reader);
            }

            nodeHelperMock.Verify(x =>
                x.ValidateNodeNamesList(It.IsAny<List<string>>()), Times.Exactly(2));
        }

        [TestMethod]
        public void Calls_GetOrCreateNode()
        {
            var nodeHelperMock = TestsHelper.GetNodeHelperMock();
            var builder = new TreeBuilder(nodeHelperMock.Object);
            using (var stream = TestsHelper.CreateStream(ValidTreeString))
            {
                var reader = new StreamReader(stream);
                builder.BuildTree(reader);
            }

            nodeHelperMock.Verify(x => x.GetOrCreateNode(
                It.IsAny<Dictionary<string, Node>>(), It.IsAny<string>()), Times.Exactly(6));
        }

        // Negative tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_Exception_On_Reader_Null()
        {
            TreeBuilder.BuildTree(null);
        }

    }
}
