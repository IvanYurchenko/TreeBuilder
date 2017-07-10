using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.IO;

namespace TreeBuilder.Tests
{
    [TestClass]
    public class TreeBuilderIntegrationTests
    {
        public static ITreeBuilder TreeBuilder { get; set; }
        public static TestsHelper TestsHelper { get; set; }

        public static Dictionary<string, string> PositiveInputs { get; set; }
        public static Dictionary<string, string> IncorrectInputs { get; set; }
        public static Dictionary<string, string> IncorrectTrees { get; set; }

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            var kernel = new StandardKernel(new NinjectConfiguration());
            TreeBuilder = kernel.Get<ITreeBuilder>();
            TestsHelper = new TestsHelper();

            PositiveInputs = new Dictionary<string, string>
            {
                { "Usual", "1, 2, 3 \r\n 2, 4, 5 \r\n 3, 6, 7" }
            };

            IncorrectInputs = new Dictionary<string, string>
            {
                { "4nodes", "1, 2, 3 \r\n 2, 5, 6, 7" },
                { "2nodes", "1, 2 \r\n 2, 3, 4" },
                { "Empty", "" },
            };

            IncorrectTrees = new Dictionary<string, string>
            {
                { "Loop", "1, 2, 3 \r\n 2, 1, 4" },
                { "TwoParents", "1, 2, 3, \r\n 4, 2, 5" },
                { "SeveralChildren", "1, 2, 3 \r\n 1, 4, 5" },
                { "SeveralParts", "1, 2, 3 \r\n 4, 5, 6" }
            }; 
        }

        [TestMethod]
        public void Generates_Correct_Tree()
        {
            Tree tree;

            using (var stream = TestsHelper.CreateStream(PositiveInputs["Usual"]))
            {
                using(var reader = new StreamReader(stream))
                {
                    tree = TreeBuilder.BuildTree(reader);
                }
            }

            Assert.IsNotNull(tree);

            var rootNode = tree.RootNode;
            Assert.IsNotNull(rootNode);
            Assert.AreEqual(rootNode.Value, "1");

            var leftNode = rootNode.LeftNode;
            Assert.IsNotNull(leftNode);
            Assert.AreEqual(leftNode.Value, "2");

            var rightNode = rootNode.RightNode;
            Assert.IsNotNull(rightNode);
            Assert.AreEqual(rightNode.Value, "3");

            Assert.IsNotNull(leftNode.LeftNode);
            Assert.AreEqual(leftNode.LeftNode.Value, "4");
            Assert.IsNotNull(leftNode.RightNode);
            Assert.AreEqual(leftNode.RightNode.Value, "5");

            Assert.IsNotNull(rightNode.LeftNode);
            Assert.AreEqual(rightNode.LeftNode.Value, "6");
            Assert.IsNotNull(rightNode.RightNode);
            Assert.AreEqual(rightNode.RightNode.Value, "7");
        }

        [TestMethod]
        [ExpectedException(typeof(TreeException))]
        public void Throws_Error_When_4_nodes()
        {
            Tree tree;

            using (var stream = TestsHelper.CreateStream(IncorrectInputs["4nodes"]))
            {
                using (var reader = new StreamReader(stream))
                {
                    tree = TreeBuilder.BuildTree(reader);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TreeException))]
        public void Throws_Error_When_2_nodes()
        {
            Tree tree;

            using (var stream = TestsHelper.CreateStream(IncorrectInputs["2nodes"]))
            {
                using (var reader = new StreamReader(stream))
                {
                    tree = TreeBuilder.BuildTree(reader);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TreeException))]
        public void Throws_Error_When_Empty()
        {
            Tree tree;

            using (var stream = TestsHelper.CreateStream(IncorrectInputs["Empty"]))
            {
                using (var reader = new StreamReader(stream))
                {
                    tree = TreeBuilder.BuildTree(reader);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TreeException))]
        public void Throws_Error_When_Loop()
        {
            Tree tree;

            using (var stream = TestsHelper.CreateStream(IncorrectTrees["Loop"]))
            {
                using (var reader = new StreamReader(stream))
                {
                    tree = TreeBuilder.BuildTree(reader);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TreeException))]
        public void Throws_Error_When_Two_Parents()
        {
            Tree tree;

            using (var stream = TestsHelper.CreateStream(IncorrectTrees["TwoParents"]))
            {
                using (var reader = new StreamReader(stream))
                {
                    tree = TreeBuilder.BuildTree(reader);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TreeException))]
        public void Throws_Error_When_Several_Children()
        {
            Tree tree;

            using (var stream = TestsHelper.CreateStream(IncorrectTrees["SeveralChildren"]))
            {
                using (var reader = new StreamReader(stream))
                {
                    tree = TreeBuilder.BuildTree(reader);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TreeException))]
        public void Throws_Error_When_Several_Parts()
        {
            Tree tree;

            using (var stream = TestsHelper.CreateStream(IncorrectTrees["SeveralParts"]))
            {
                using (var reader = new StreamReader(stream))
                {
                    tree = TreeBuilder.BuildTree(reader);
                }
            }
        }
    }
}
