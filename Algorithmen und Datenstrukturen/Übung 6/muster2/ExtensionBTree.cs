namespace FHS.CT.AlgoDat.DataStructures
{
    public static class BTreeExtensions
    {
        public static int GetDepth<T>(this BTree<T> tree) where T : IComparable<T>
        {
            if (tree.Root.N == 0)
            {
                return 0;
            }

            var currentNode = tree.Root;
            int depth = 0;
            while (currentNode != null)
            {
                currentNode = currentNode.Children[0];
                depth++;
            }

            return depth;
        }
    }
}