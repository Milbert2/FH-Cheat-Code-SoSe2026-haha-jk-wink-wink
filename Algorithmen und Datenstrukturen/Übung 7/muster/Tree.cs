// AlgoDat Implementation
// Copyright (C) 2024  Fachhochschule Salzburg / Department Creative Technologies / Andreas Bilke

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace FHS.CT.AlgoDat.DataStructures
{
    public class Tree<T> where T : IComparable<T>
    {
        public class Node
        {
            public T Key { get; }

            public Node? Left { get; set; }
            public Node? Right { get; set; }

            public Node(T key)
            {
                Key = key;
            }
        }

        public Node? Root { get; set; }

        public bool CheckHeapProperties(Node n)
        {
            return IsHeap(n) && IsComplete(n);
        }

        public bool IsHeap(Node? n)
        {
            if (n == null)
            {
                return true;
            }

            if (n.Left != null && n.Left.Key.CompareTo(n.Key) < 0)
            {
                return false;
            }

            if (n.Right != null && n.Right.Key.CompareTo(n.Key) < 0)
            {
                return false;
            }

            return IsHeap(n.Left) && IsHeap(n.Right);
        }

        public bool IsComplete(Node n)
        {
            var indices = new List<int>();
            CreateIndex(n, 0, indices);
            indices.Sort();

            for (int i = 1; i < indices.Count; i++)
            {
                if (indices[i - 1] + 1 != indices[i])
                {
                    return false;
                }
            }

            return true;
        }

        private void CreateIndex(Node? n, int index, List<int> indices)
        {
            if (n == null)
            {
                return;
            }

            indices.Add(index);

            int indexLeft = 2 * index + 1;
            CreateIndex(n.Left, indexLeft, indices);

            int indexRight = 2 * index + 2;
            CreateIndex(n.Right, indexRight, indices);
        }
    }
}
