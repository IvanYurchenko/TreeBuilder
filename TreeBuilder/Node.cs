namespace TreeBuilder
{
    public class Node
    {
        public Node(string value, Node leftNode, Node rightNode)
        {
            Value = value;
            LeftNode = leftNode;
            RightNode = rightNode;
        }

        public string Value { get; set; }
        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }
    }
}
