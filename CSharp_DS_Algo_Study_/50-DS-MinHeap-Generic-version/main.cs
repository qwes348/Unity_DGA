using System;
using System.Collections.Generic;

// https://gmlwjd9405.github.io/2018/05/10/data-structure-heap.html
class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    var h = new MinHeap<int>();
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

    var todoList = new MinHeap<Todo>();
    todoList.Insert(new Todo(0, 10, "Game"));  //ID, 우선순위, 할일
    todoList.Insert(new Todo(1, 1, "H/W"));
    todoList.Insert(new Todo(2, 5, "Drinking"));
    todoList.Insert(new Todo(3, 0, "Walking"));
    todoList.Insert(new Todo(4, 7, "Sleep"));

    todoList.Remove(todoList.Find(o=>o.id==3));   //RemoveTop()
    print(todoList.RemoveTop().ToString() == "1,H/W");
    print(todoList.RemoveTop().ToString() == "5,Drinking");
    print(todoList.RemoveTop().ToString() == "7,Sleep");
    print(todoList.RemoveTop().ToString() == "10,Game");
  }

  public class Todo : IComparable, IEquatable<Todo>  // 비교가능, 이퀄가능
  {
    public int id;
    public int priority;
    public string desc;
    
    public Todo(int id, int priority, string desc)
    {
      this.id = id;
      this.priority = priority;
      this.desc = desc;
    }
    public int CompareTo(object other)  // int 타입의 비교를 Todo의 비교로 끌어와 사용함
    {
      return priority.CompareTo(((Todo)other).priority);
    }

    public bool Equals(Todo t) // Todo의 id가 같은지 비교
    {
      return id == t.id;
    }

    public override string ToString()
    {
      return priority.ToString() + "," + desc;
    }
  }

}
                                  // 대소비교   // 같은지 비교
public class MinHeap<T> where T : IComparable, IEquatable<T>
{
  List<T> list;

  public MinHeap()
  {
    list = new List<T>();
  }

  public void Insert(T v)
  {
    list.Add(v);
    HeapifyUp(list.Count-1);
  }

  void HeapifyUp(int i)
  {
    if(i < 1)
      return;
    int p = Parent(i);
    // if(list[p] > list[i])
    if(list[p].CompareTo(list[i]) > 0)
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
      // if(list[i] > list[lc] || list[i] > list[rc])
      if(list[i].CompareTo(list[lc]) > 0 || list[i].CompareTo(list[rc]) > 0)
      {
        // if(list[lc] <= list[rc])  // 오른쪽자식이 클때
        if(list[lc].CompareTo(list[rc]) <= 0)
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
      // if(list[i] > list[lc])
      if(list[i].CompareTo(list[lc]) > 0)
      {
        list.Swap(i, lc);
        HeapifyDown(lc);
      }
      else
        return;    
    }
    
  }

  public T RemoveTop()
  {
    T v = list[0];
    list[0] = list[list.Count-1];
    list.RemoveAt(list.Count-1);
    HeapifyDown(0);
    return v;    // 과제------------------------
  }

  public void Remove(T v)
  {
    int index = 0;
    for(int i = 0; i < list.Count-1; i++)
    {
      // if(list[i] == v)
      if(list[i].Equals(v))
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
  // Func<int, bool> f == Predicate<int> f
  public T Find(Predicate<T> f)
  {
    int i = list.FindIndex(f);
    if(i != -1)
      return list[i];
    else
      return default(T);
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