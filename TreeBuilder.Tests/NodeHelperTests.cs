using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;

namespace TreeBuilder.Tests
{
    [TestClass]
    public class NodeHelperTests
    {
        public static INodeHelper NodeHelper { get; set; }
        public const string NodeName = "NodeName";

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            var kernel = new StandardKernel(new NinjectConfiguration());
            NodeHelper = kernel.Get<INodeHelper>();
        }
        
        // GetOrCreateNode

        [TestMethod]
        public void GetOrCreateNode_Returns_Null()
        {
            var dictionary = new Dictionary<string, Node>();

            var node = NodeHelper.GetOrCreateNode(dictionary, "#");

            Assert.IsNull(node);
        }

        [TestMethod]
        public void GetOrCreateNode_Returns_Right_Node()
        {
            var dictionary = new Dictionary<string, Node>();

            var node = NodeHelper.GetOrCreateNode(dictionary, NodeName);

            Assert.IsNotNull(node);
            Assert.AreEqual(node.Value, NodeName);
        }

        [TestMethod]
        public void GetOrCreateNode_Adds_To_Dictionary()
        {
            var dictionary = new Dictionary<string, Node>();

            var node = NodeHelper.GetOrCreateNode(dictionary, NodeName);

            Assert.IsTrue(dictionary.ContainsKey(NodeName));
            Assert.IsNotNull(dictionary[NodeName]);
            Assert.AreEqual(dictionary[NodeName].Value, NodeName);
        }

        // ValidateNodeNamesList

        [TestMethod]
        [ExpectedException(typeof(TreeException))]
        public void ValidateNodeNamesList_Throws_When_Empty()
        {
            var list = new List<string>();

            NodeHelper.ValidateNodeNamesList(list);
        }

        [TestMethod]
        [ExpectedException(typeof(TreeException))]
        public void ValidateNodeNamesList_Throws_When_Less_Than_3()
        {
            var list = new List<string> { "a", "b" };

            NodeHelper.ValidateNodeNamesList(list);
        }

        [TestMethod]
        [ExpectedException(typeof(TreeException))]
        public void ValidateNodeNamesList_Throws_When_Mode_Than_3()
        {
            var list = new List<string> { "a", "b", "c", "d" };

            NodeHelper.ValidateNodeNamesList(list);
        }

        [TestMethod]
        [ExpectedException(typeof(TreeException))]
        public void ValidateNodeNamesList_Throws_When_Whitespace()
        {
            var list = new List<string> { "a", " " };

            NodeHelper.ValidateNodeNamesList(list);
        }

        // ValidateTree

        [TestMethod]
        public void ValidateTree_Validates_Correct_Tree()
        {
            var node1 = new Node("1", null, null);
            var node2 = new Node("2", null, null);
            var node3 = new Node("3", node2, node1);
            var node4 = new Node("4", node3, null);
            var node5 = new Node("5", null, node4);

            var rootNodes = new List<Node> { node5 };

            NodeHelper.ValidateTree(rootNodes, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(TreeException))]
        public void ValidateTree_Throws_When_List_Is_Empty()
        {
            var rootNodes = new List<Node> { };

            NodeHelper.ValidateTree(rootNodes, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(TreeException))]
        public void ValidateTree_Throws_When_Graph_Has_Several_Root_Nodes()
        {
            var node1 = new Node("1", null, null);
            var node2 = new Node("2", null, null);
            var rootNodes = new List<Node> { node1, node2 };

            NodeHelper.ValidateTree(rootNodes, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(TreeException))]
        public void ValidateTree_Throws_When_Graph_Has_Loops()
        {
            var node1 = new Node("1", null, null);
            var node2 = new Node("2", node1, null);
            node1.LeftNode = node2;

            var rootNodes = new List<Node> { node1 };

            NodeHelper.ValidateTree(rootNodes, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(TreeException))]
        public void ValidateTree_Throws_When_Graph_Has_Several_Parts()
        {
            var node1 = new Node("1", null, null);
            var node2 = new Node("2", null, null);
            var node3 = new Node("3", node2, null);
            node2.LeftNode = node3;

            var rootNodes = new List<Node> { node1 };

            NodeHelper.ValidateTree(rootNodes, 3);
        }
    }
}
