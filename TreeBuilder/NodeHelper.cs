using System.Collections.Generic;
using System.Linq;

namespace TreeBuilder
{
    public class NodeHelper : INodeHelper
    {
        public Node GetOrCreateNode(Dictionary<string, Node> nodesDictionary,
            string nodeName)
        {
            if (nodeName == "#")
            {
                return null;
            }

            if (!nodesDictionary.ContainsKey(nodeName))
            {
                var node = new Node(nodeName, null, null);
                nodesDictionary.Add(nodeName, node);
            };

            return nodesDictionary[nodeName];
        }

        public void ValidateNodeNamesList(List<string> nodeNames)
        {
            if (nodeNames.Count != 3)
            {
                throw new TreeException("Found incorrect number of nodes in one line. ");
            }

            if (nodeNames.Any(name => string.IsNullOrWhiteSpace(name)))
            {
                throw new TreeException("The node name is empty. ");
            }
        }

        public void ValidateTree(List<Node> rootNodes, int nodesCount)
        {
            if(rootNodes.Count == 0)
            {
                throw new TreeException("The provided graph is empty. ");
            }

            if (rootNodes.Count > 1)
            {
                throw new TreeException("The provided graph has several parts. ");
            }

            var rootNode = rootNodes.First();

            // Count of nodes in traversed tree to compare with total nodes count
            // If they are different that means that we have a graph with several parts
            var count = 0;

            var traversedNodes = new HashSet<Node>();

            var queue = new Queue<Node>();
            queue.Enqueue(rootNode);

            while (queue.Any())
            {
                var node = queue.Dequeue();

                if(traversedNodes.Contains(node))
                {
                    throw new TreeException("The provided graph has loops. ");
                }

                traversedNodes.Add(node);
                count++;

                if (node.LeftNode != null)
                {
                    queue.Enqueue(node.LeftNode);
                }

                if (node.RightNode != null)
                {
                    queue.Enqueue(node.RightNode);
                }
            }

            if (count != nodesCount)
            {
                throw new TreeException("The provided graph has several parts. ");
            }
        }
    }
}
