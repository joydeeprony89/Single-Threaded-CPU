// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Solution s = new Solution();
var tasks = new int[6][] { new int[] { 5, 2 }, new int[] { 7, 2 }, new int[] { 9, 4 }, new int[] { 6, 3 }, new int[] { 5, 10 }, new int[] { 1, 1 } };
var answer = s.GetOrder(tasks);
Console.WriteLine(string.Join(",", answer));


public class Solution
{
  public int[] GetOrder(int[][] tasks)
  {
    int length = tasks.Length;
    var sorted = new int[length][];
    for (int i = 0; i < length; i++)
    {
      sorted[i] = new int[3];
      sorted[i][0] = tasks[i][0];
      sorted[i][1] = tasks[i][1];
      sorted[i][2] = i;
    }

    Array.Sort(sorted, (a, b) => { return a[0] - b[0]; });
    PriorityQueue<int[], int[]> pq = new PriorityQueue<int[], int[]>(new Comparer());
    var result = new List<int>();
    int j = 0;
    int start = 0;
    while (result.Count < length)
    {
      while (j < length && sorted[j][1] <= start)
      {
        pq.Enqueue(sorted[j], sorted[j]);
        j++;
      }
      if (pq.Count > 0)
      {
        var task = pq.Dequeue();
        result.Add(task[2]);
        start += task[1];
      }
      else
      {
        start = sorted[j][1];
      }
    }


    return result.ToArray();
  }

  public class Comparer : IComparer<int[]>
  {
    public int Compare(int[] x, int[] y)
    {
      return x[1] == y[1] ? x[2] - y[2] : x[1] - y[1];
    }
  }
}
