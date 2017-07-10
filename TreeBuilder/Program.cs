using Ninject;
using System;
using System.IO;

namespace TreeBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var kernel = new StandardKernel(new NinjectConfiguration());

            var builder = kernel.Get<ITreeBuilder>();

            Tree tree = null;
            using (var stream = File.OpenRead("tree.txt"))
            {
                using (var reader = new StreamReader(stream))
                {
                    try
                    {
                        tree = builder.BuildTree(reader);
                    }
                    catch (TreeException treeException)
                    {
                        Console.WriteLine("Error: " + treeException.Message);
                    }
                }
            }
            
            if(tree != null)
            {
                Console.WriteLine("The tree has been successfully built. ");
            }

            Console.ReadKey();
        }
    }
}