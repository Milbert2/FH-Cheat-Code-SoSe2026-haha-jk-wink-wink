using System.ComponentModel.DataAnnotations;

namespace FHS.CT.AlgoDat.DataStructures
{
    public static class AvlTreeExtensions
    {
        public static (int, int) GetMinMaxDepth<T>(this AvlTree<T> tree) where T : IComparable<T>
        {
            return GetMinMaxDepth(tree.Root);
        }

        private static (int, int) GetMinMaxDepth<T>(AvlTree<T>.Node? n) where T : IComparable<T>
        {
            if (n == null)
            {
                return (0, 0);
            }

            var minMaxL = GetMinMaxDepth(n.Left);
            var minMaxR = GetMinMaxDepth(n.Right);

            var min = Math.Min(Math.Min(minMaxL.Item1, minMaxR.Item1), Math.Min(minMaxL.Item2, minMaxR.Item2)) + 1;
            var max = Math.Max(Math.Max(minMaxL.Item1, minMaxR.Item1), Math.Max(minMaxL.Item2, minMaxR.Item2))+ 1;

            return (min, max);
        }

        private static int GetDepth<T>(AvlTree<T>.Node? n) where T : IComparable<T>
        {
            if (n == null)
            {
                return 0;
            }

            int dL = GetDepth(n.Left);
            int dR = GetDepth(n.Right);

            return Math.Max(dL, dR) + 1;
        }
    }
}