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

namespace AlgoDat
{    
    public class InsertionSort<T> where T : IComparable<T>
    {
        public static void Sort(T[] list, int left, int right)
        {
            for (int j = left + 1; j <= right; j++)
            {
                T key = list[j];

                int i = j - 1;
                while (i >= left && list[i].CompareTo(key) > 0)
                {
                    list[i + 1] = list[i];
                    i--;
                }

                list[i + 1] = key;
            }
        }
    }
}