using FHS.CT.AlgoDat.DataStructures;


namespace AlgoDat
{
    public static class BSTChecker<T> where T : IComparable<T>
    {
        public static bool Check(Tree<T> tree)
        {
            if (tree.Root == null)
            {
                return true;
            }

            return CheckNode(tree.Root);
        }

        private static bool CheckNode(Tree<T>.Node? n)
        {
            if (n == null)
            {
                return true; // a non existing node is a bst node
            }

            if (n.Left != null)
            {
                var maxValue = GetMaxValue(n.Left);
                if (maxValue.CompareTo(n.Key) >= 0)
                {
                    // if the largest node in the left subtree is larger equal than key -> violation found
                    return false;
                }
            }

            if (n.Right != null)
            {
                var minValue = GetMinValue(n.Right);
                if (minValue.CompareTo(n.Key) < 0)
                {
                    // if the smallest node in the right subtree is smaller than key -> violation found
                    return false;
                }
            }

            return CheckNode(n.Left) && CheckNode(n.Right);
        }

        private static T GetMinValue(Tree<T>.Node n)
        {
            // we are at a leaf
            if (n.Left == null && n.Right == null)
            {
                return n.Key;
            }

            var minValues = new List<T>
            {
                n.Key
            };

            if (n.Left != null)
            {
                minValues.Add(GetMinValue(n.Left));
            }

            if (n.Right != null)
            {
                minValues.Add(GetMinValue(n.Right));
            }

            return minValues.Min()!; // there is at least one value inside
        }

        private static T GetMaxValue(Tree<T>.Node n)
        {
            // we are at a leaf
            if (n.Left == null && n.Right == null)
            {
                return n.Key;
            }

            var maxValues = new List<T>
            {
                n.Key
            };

            if (n.Left != null)
            {
                maxValues.Add(GetMaxValue(n.Left));
            }

            if (n.Right != null)
            {
                maxValues.Add(GetMaxValue(n.Right));
            }

            return maxValues.Max()!; // there is at least one value inside
        }
    }
}
