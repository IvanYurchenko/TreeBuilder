using System.Collections.Generic;

namespace TreeBuilder
{
    public interface INodeHelper
    {
        /// <summary>
        /// Gets a node specified by its name from the nodes dictionary.
        /// If the node doesn't exist, creates it and adds to the dictionary.
        /// </summary>
        /// <param name="nodesDictionary">Nodes dictionary. </param>
        /// <param name="nodeName">Name of the node. "#" means that the node is null. </param>
        /// <returns>Node from the dictionary or null if node's name equals "#". </returns>
        Node GetOrCreateNode(Dictionary<string, Node> nodesDictionary, string nodeName);

        /// <summary>
        /// Validates the list of node names received from one string and throws
        /// an appropriate exception if some of the nodes are invalid.
        /// </summary>
        /// <param name="nodeNames">List of node names. </param>
        void ValidateNodeNamesList(List<string> nodeNames);

        /// <summary>
        /// Validates the tree (checks for loops, several parts etc.)
        /// </summary>
        /// <param name="rootNodes">The list of root nodes of the tree.
        /// If the tree is valid, there should be just 1 node. </param>
        /// <param name="nodesCount">Total count of nodes to check after traversing. 
        /// </param>
        void ValidateTree(List<Node> rootNodes, int nodesCount);
    }
}
