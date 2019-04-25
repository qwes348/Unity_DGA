using System;
using System.Collections.Generic;

// http://www.csharp-examples.net/list/
// 테스트케이스 작성해오기
class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    var list = new List<int>() {8, 3, 2};
    
    int item = list[1];
    print(item == 3);

    list.Add(5);
    print(list.Stringify() == "8 3 2 5");

    var list2 = new List<int>() {7, 9};
    list.AddRange(list2);
    print(list.Stringify() == "8 3 2 5 7 9");

    int index1 = list.BinarySearch(5);
    print(index1 == 3);
    int index2 = list.BinarySearch(5, new MyComparer());
    print(index2 == 3);
    int index3 = list.BinarySearch(0, 3, 5, new MyComparer());
    print(index3 == -4);

    var conv = new Converter<int, decimal>(x => (decimal)(x+1));
    var list3 = list.ConvertAll<decimal>(conv);
    print(list3.Stringify() == "9 4 3 6 8 10");

    list = new List<int>() {8, 3, 2};
    int[] array = new int[] {0, 0, 0, 0, 0};
    list.CopyTo(array);
    print(array.Stringify() == "8 3 2 0 0");

    array = new int[] {0, 0, 0, 0, 0};
    list.CopyTo(array, 2);
    print(array.Stringify() == "0 0 8 3 2");

    array = new int[] {0, 0, 0, 0, 0};
    list.CopyTo(1, array, 3, 1);
    print(array.Stringify() == "0 0 0 3 0");

    print(list.Exists(x => x==3) == true);
    print(list.Exists(x => x==10) == false);

    list2 = list;
    print(list.Equals(list2) == true);

    list2 = new List<int>() {8, 3, 2};
    print(list.Equals(list2) == false);

    print(list.Find(x => x > 2) == 8);
    print(list.Find(x => x > 10) == 0);

    list2 = list.FindAll(x => x > 2);
    print(list2.Stringify() == "8 3");

    list = new List<int>() {8, 3, 6, 4, 2};
    print(list.FindIndex(x => x < 5) == 1);
    print(list.FindIndex(2, x => x < 5) == 3);
    print(list.FindIndex(2, 2, x => x < 5) == 3);
    print(list.FindIndex(2, 2, x => x < 3) == -1);

    list = new List<int>() {8, 3, 2};
    print(list.FindLast(x => x < 5) == 2);
    print(list.FindLast(x => x > 10) == 0);

    list = new List<int>() {2, 4, 6, 3, 8};
    print(list.FindLastIndex(x => x < 5) == 3);
    print(list.FindLastIndex(2, x => x < 5) == 1);
    print(list.FindLastIndex(2, 2, x => x < 5) == 1);
    print(list.FindLastIndex(2, 2, x => x < 3) == -1);

    list = new List<int>() {8, 3, 2};
    list.ForEach(x => {Console.Write(x);});
    print("");

    list = new List<int>() {8, 3, 6, 4, 2};
    list2 = list.GetRange(1, 3);
    print(list2.Stringify() == "3 6 4");

    list = new List<int>() {8, 3, 2, 6, 8};
    print(list.IndexOf(8) == 0);
    print(list.IndexOf(8, 1) == 4);
    print(list.IndexOf(3, 1, 2) == 1);
    print(list.IndexOf(8, 1, 2) == -1);

    list = new List<int>() {8, 3, 2};
    list.Insert(1, 5);
    print(list.Stringify() == "8 5 3 2");

    list = new List<int>() {8, 3, 2};
    list2 = new List<int>() {5, 7};
    list.InsertRange(1, list2);
    print(list.Stringify() == "8 5 7 3 2");

    list = new List<int>() {8, 4, 2, 4};
    list.Remove(4);
    print(list.Stringify() == "8 2 4");

    list = new List<int>() {8, 3, 6, 2};
    list.RemoveAll(x => x < 4);
    print(list.Stringify() == "8 6");

    list = new List<int>() {8, 3, 6, 2};
    list.RemoveAt(2);
    print(list.Stringify() == "8 3 2");

    list = new List<int>() {8, 3, 6, 2, 4, 5};
    list.RemoveRange(2, 3);
    print(list.Stringify() == "8 3 5");

    list = new List<int>() {8, 3, 2};
    list.Reverse();
    print(list.Stringify() == "2 3 8");
    list = new List<int>() {8, 3, 6, 2};
    list.Reverse(1, 2);
    print(list.Stringify() == "8 6 3 2");

    list = new List<int>() {8, 3, 6, 2};
    list.Sort();
    print(list.Stringify() == "2 3 6 8");
    list = new List<int>() {8, 3, 6, 2};
    list.Sort((x, y) => x.CompareTo(y));
    print(list.Stringify() == "2 3 6 8");

    list = new List<int>() {8, 3, 6, 2};
    list.Sort((x, y) => y.CompareTo(x));
    print(list.Stringify() == "8 6 3 2");
    list = new List<int>() {8, 3, 6, 2};
    list.Sort(new MyComparer());
    print(list.Stringify() == "2 3 6 8");

    list = new List<int>() {8, 3, 2};
    print(list.TrueForAll(x => x < 10) == true);
    print(list.TrueForAll(x => x < 5) == false);

    print(list.Count);
  }

  public class MyComparer : IComparer<int>
  {
    public int Compare(int x, int y)
    {
      return x.CompareTo(y);
    }
  }
}

public static class ClassExtension
{
  public static string Stringify<T>(this IEnumerable<T> list)
  {
    return String.Join(" ", list);
  }
}