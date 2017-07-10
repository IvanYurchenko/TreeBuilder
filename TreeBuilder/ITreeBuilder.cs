using System.IO;

namespace TreeBuilder
{
    public interface ITreeBuilder
    {
        /// <summary>
        /// Parses data from the provided reader and builds a Tree based on this data.
        /// </summary>
        /// <param name="reader">Stream reader to read data from. </param>
        /// <returns>Binary Tree that was built based on data from the reader. </returns>
        Tree BuildTree(StreamReader reader);
    }
}
