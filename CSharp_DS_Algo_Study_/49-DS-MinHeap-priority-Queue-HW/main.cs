using System;
using System.Collections.Generic;

// https://gmlwjd9405.github.io/2018/05/10/data-structure-heap.html
class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    MinHeap h = new MinHeap();
    h.Insert(7);
    print(h.Stringify() == "7");
    h.Insert(1);
    print(h.Stringify() == "1 7");
    h.Insert(3);
    h.Insert(2);
    h.Insert(5);
    h.Insert(1);
    h.Insert(8);
    h.Insert(0);
    print(h.Stringify() == "0 1 1 2 5 3 8 7");

    print(h.RemoveTop() == 0);  
    print(h.RemoveTop() == 1);
    print(h.Stringify() == "1 2 3 7 5 8");
    print(h.RemoveTop() == 1);
    print(h.Stringify() == "2 5 3 7 8");

    h.Insert(0); h.Insert(1); h.Insert(1);
    print(h.Stringify() == "0 1 1 5 8 3 2 7");

    h.Remove(5);
    print(h.Stringify() == "0 1 1 7 8 3 2");
    h.Remove(8);
    print(h.Stringify() == "0 1 1 7 2 3");
    h.Remove(0);
    print(h.Stringify() == "1 2 1 7 3");
    
    print(h.Find(o=>o==2) == 2);
    print(h.RemoveTop() == 1);
    print(h.RemoveTop() == 1);
    print(h.RemoveTop() == 2);
    print(h.RemoveTop() == 3);
    print(h.RemoveTop() == 7);
    print(h.Stringify() == "");
  }
}

public class MinHeap
{
  List<int> list;

  public MinHeap()
  {
    list = new List<int>();
  }

  public void Insert(int v)
  {
    list.Add(v);
    HeapifyUp(list.Count-1);
  }

  void HeapifyUp(int i)
  {
    if(i < 1)
      return;
    int p = Parent(i);
    if(list[p] > list[i])
    {  
      list.Swap(p, i);
      HeapifyUp(p);
    }
  }

  void HeapifyDown(int i)
  {
    if(i > list.Count-1)
      return;
    int lc = LChild(i);
    int rc = RChild(i);

    if(isLeaf(i)) // 자식이 둘다X
      return;
    else if(lc < list.Count-1 && rc <= list.Count-1) // 자식이 둘다O
    {
      if(list[i] > list[lc] || list[i] > list[rc])
      {
        if(list[lc] <= list[rc])  // 오른쪽자식이 클때
        {
          list.Swap(i, lc);
          HeapifyDown(lc);
        }
        else  // 왼쪽자식이 클때
        {
          list.Swap(i, rc);
          HeapifyDown(rc);
        }
      }
      else
        return;
    }
    else if(lc <= list.Count-1 && rc > list.Count-1) // 왼쪽자식만 있을때
    {
      if(list[i] > list[lc])
      {
        list.Swap(i, lc);
        HeapifyDown(lc);
      }
      else
        return;    
    }
    
  }

  public int RemoveTop()
  {
    int v = list[0];
    list[0] = list[list.Count-1];
    list.RemoveAt(list.Count-1);
    HeapifyDown(0);
    return v;    // 과제------------------------
  }

  public void Remove(int v)
  {
    int index = 0;
    for(int i = 0; i < list.Count-1; i++)
    {
      if(list[i] == v)
      {
        index = i;
        break;
      }
    }

    list[index] = list[list.Count-1];
    list.RemoveAt(list.Count-1);
    HeapifyDown(index);
    // 주어진 value를 찾아서 삭제 
  }

  // h.Find(o=>o==2) == 2;
  // Func<int, bool> f
  public int Find(Predicate<int> f)
  {
    int i = list.FindIndex(f);
    if(i != -1)
      return list[i];
    else
      return -1;
  }

  bool isLeaf(int i) {return LChild(i)>list.Count-1;}

  

  public string Stringify()
  {
    return list.Stringify();
  }

  int Parent(int i) { return (i-1)/2; }
  int LChild(int i) { return 2*(i+1)-1; }
  int RChild(int i) { return LChild(i)+1; }
}

public static class ClassExtension
{
  public static string Stringify<T>(this IEnumerable<T> list) 
  {
    return String.Join(" ", list);
  }

  public static IList<T> Swap<T>(this IList<T> list, int i, int j)
  {
    T temp = list[i];
    list[i] = list[j];
    list[j] = temp;
    return list;
  }
}