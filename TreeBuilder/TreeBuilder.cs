using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TreeBuilder
{
    public class TreeBuilder : ITreeBuilder
    {
        public TreeBuilder(INodeHelper nodeHelper)
        {
            NodeHelper = nodeHelper;
        }

        private INodeHelper NodeHelper { get; set; }

        public Tree BuildTree(StreamReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("Reader isn't provided.");
            }

            var nodesDictionary = new Dictionary<string, Node>();

            // Children set is used to determine root nodes
            // (root nodes won't be in this set)
            var children = new HashSet<Node>();

            var line = reader.ReadLine();
            while (line != null)
            {
                var nodeNames = line.Split(',').Select(word => word.Trim()).ToList();

                NodeHelper.ValidateNodeNamesList(nodeNames);

                var rootNode = NodeHelper.GetOrCreateNode(nodesDictionary, nodeNames[0]);
                rootNode.LeftNode = NodeHelper.GetOrCreateNode(nodesDictionary, nodeNames[1]);
                rootNode.RightNode = NodeHelper.GetOrCreateNode(nodesDictionary, nodeNames[2]);
                
                if(!children.Contains(rootNode.LeftNode))
                {
                    children.Add(rootNode.LeftNode);
                }

                if(!children.Contains(rootNode.RightNode))
                {
                    children.Add(rootNode.RightNode);
                }

                line = reader.ReadLine();
            }

            var rootNodes = nodesDictionary.Values.Except(children).ToList();

            NodeHelper.ValidateTree(rootNodes, nodesDictionary.Count);

            var tree = new Tree(rootNodes.First());
            return tree;
        }
    }
}
