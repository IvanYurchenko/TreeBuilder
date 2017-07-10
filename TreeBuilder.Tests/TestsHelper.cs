using Moq;
using System.Collections.Generic;
using System.IO;

namespace TreeBuilder.Tests
{
    public class TestsHelper
    {
        public Stream CreateStream(string s)
        {
            var stream = new MemoryStream();

            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }

        public Mock<INodeHelper> GetNodeHelperMock()
        {
            var nodesHelperMock = new Mock<INodeHelper>();
            nodesHelperMock.Setup(x =>
            x.GetOrCreateNode(It.IsAny<Dictionary<string, Node>>(), It.IsAny<string>()))
            .Returns((Dictionary<string, Node> dict, string name) => {
                if (!dict.ContainsKey(name))
                {
                    dict.Add(name, new Node(name, null, null));
                }
                return dict[name];
            });

            nodesHelperMock.Setup(x => x.ValidateNodeNamesList(It.IsAny<List<string>>()));
            nodesHelperMock.Setup(x => x.ValidateTree(It.IsAny<List<Node>>(), It.IsAny<int>()));

            return nodesHelperMock;
        }
    }
}
