using System.Collections.Generic;
using System.Linq;

namespace TreeBuilder
{
    public class Tree
    {
        public Tree(Node rootNode)
        {
            RootNode = rootNode;
        }

        public Node RootNode { get; set; }
    }
}
