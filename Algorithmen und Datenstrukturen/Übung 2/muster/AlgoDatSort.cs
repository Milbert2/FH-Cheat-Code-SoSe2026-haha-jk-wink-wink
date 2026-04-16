namespace AlgoDat
{
    public class AlgoDatSort<T> where T : IComparable<T>
    {
        public int Cutoff { get; set; }

        public void Sort(T[] list)
        {
            MergeSort<T>.Sort(list, Cutoff);
        }
    }
}
