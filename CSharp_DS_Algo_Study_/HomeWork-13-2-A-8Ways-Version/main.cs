using System;
using System.Collections;
using System.Collections.Generic;

// 숙제 
// A* 알고리즘
// 목표지점을 만나면 종료
// 각 인덱스의 비용은 1
// 움직일때마다 비용을 기억하기
//http://theory.stanford.edu/~amitp/GameProgramming/AStarComparison.html

// http://theory.stanford.edu/~amitp/GameProgramming/Heuristics.html 맨하탄
// 힙에 F=G+H의 값 저장(F의값이 가장 낮은게 top에 있어야함 우선순위1위)
// 힙에 이미 있는지 확인후 있다면 비용 비교후 비용이 더 좋은노드로 체인지

//[0, X]
//[Y, 0]
// F == 총 비용(G+H)h
// G == 시작점으로부터 현재위치까지 이동비용 (이동할때마다 ++)
// H == 현재위치부터 목적지까지 이동비용(장애물 고려X 대각선X) 맨하탄공식 이용

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    byte[][] grid = new byte[][]
    {
      new byte[] {0, 0, 0, 0, 0, 0, 0},
      new byte[] {0, 0, 0, 0, 0, 0, 0},
      new byte[] {0, 0, 0, 0, 0, 0, 0},
      new byte[] {0, 0, 0, 0, 0, 0, 0},
      new byte[] {0, 0, 0, 0, 0, 0, 0},
      new byte[] {0, 0, 0, 0, 0, 0, 0},
      new byte[] {0, 0, 0, 0, 0, 0, 0}
    };    

    AstarSerach astar = new AstarSerach(grid);
    astar.FindPath(new Node(3, 3), new Node(6, 6));
    print(astar.pathGrid[0].Stringify());
    print(astar.pathGrid[1].Stringify());
    print(astar.pathGrid[2].Stringify());
    print(astar.pathGrid[3].Stringify());
    print(astar.pathGrid[4].Stringify());
    print(astar.pathGrid[5].Stringify());
    print(astar.pathGrid[6].Stringify());

    print("\n");

    print(astar.costGrid[0].Stringify());
    print(astar.costGrid[1].Stringify());
    print(astar.costGrid[2].Stringify());
    print(astar.costGrid[3].Stringify());
    print(astar.costGrid[4].Stringify());
    print(astar.costGrid[5].Stringify());
    print(astar.costGrid[6].Stringify());    

    byte[][] grid2 = new byte[][]
    {
      new byte[] {0, 0, 0, 0, 0, 0, 0},
      new byte[] {0, 0, 0, 0, 0, 0, 0},
      new byte[] {0, 0, 0, 0, 0, 0, 0},
      new byte[] {0, 0, 0, 0, 0, 0, 0},
      new byte[] {0, 0, 0, 0, 0, 1, 0},
      new byte[] {0, 0, 0, 0, 1, 0, 0},
      new byte[] {0, 0, 0, 0, 0, 1, 0}
    };    

    astar = new AstarSerach(grid2);
    astar.FindPath(new Node(3, 3), new Node(6, 6));
    print(astar.pathGrid[0].Stringify());
    print(astar.pathGrid[1].Stringify());
    print(astar.pathGrid[2].Stringify());
    print(astar.pathGrid[3].Stringify());
    print(astar.pathGrid[4].Stringify());
    print(astar.pathGrid[5].Stringify());
    print(astar.pathGrid[6].Stringify());

    print("\n");

    print(astar.costGrid[0].Stringify());
    print(astar.costGrid[1].Stringify());
    print(astar.costGrid[2].Stringify());
    print(astar.costGrid[3].Stringify());
    print(astar.costGrid[4].Stringify());
    print(astar.costGrid[5].Stringify());
    print(astar.costGrid[6].Stringify());       
  }
}

public class AstarSerach
{
  public byte[][] grid;
  public byte[][] pathGrid;  // 최종 길
  public byte[][] costGrid; 
  
  public AstarSerach(byte[][] grid)
  {
    this.grid = grid;
    
    this.pathGrid = new byte[grid.Length][];
    for(int i=0; i<grid.Length; i++)
      this.pathGrid[i] = new byte[grid[0].Length];
    
    this.costGrid = new byte[grid.Length][];
    for(int i=0; i<grid.Length; i++)
      this.costGrid[i] = new byte[grid[0].Length];    
  }
  public bool FindPath(Node start, Node goal)
  {
    Action<object> print = Console.WriteLine;
    int width = grid[0].Length;
    int height = grid.Length;
    var openList = new MinHeap<Node>();
    var closedList = new HashSet<Node>();  // 큐에는 find함수가 없으므로 List로 생성
    int count = 0;

    start.Manhattan(start, goal); // start의 FGH값 구함
    // print(start.F); // test

    openList.Insert(start);
    while(openList.Count > 0)
    {
      count++;     // loop Count
      Node current = openList.RemoveTop();
      // openList.RemoveAt(0);  // Queue의 Dequeue작업
      closedList.Add(current);

      if(current.Equals(goal))
      {
        print("Goal!!!");
        Node c = current;
        while(c.parent != null)
        {
            pathGrid[c.X][c.Y] = 7;
            c = c.parent;
        }
        var wall = new Node(0, 0);
        for(int i = 0; i<height; i++)  // 벽도 그려줌
        {
            wall.X = i;
            for(int j = 0; j<width; j++)
            {
            wall.Y = j;
            if(grid[wall.X][wall.Y] == 1)
                pathGrid[wall.X][wall.Y] = 1;      
            }
        }
        return true;
      }
      else
      {
        foreach(var n in Neighbors(current))
        {
          if(n.X < 0 || n.X >= height || n.Y < 0 || n.Y >= width)
            continue;
          if(closedList.Contains(n))
            continue;
          if(IsWall(n.X, n.Y))     // Wall
            continue;
          if(!IsWall(n.X, n.Y))
          {
            try
            {
              if(IsWall(current.X+1, current.Y) && IsWall(n.X-1, n.Y))
                continue;
              else if(IsWall(current.X-1, current.Y) && IsWall(n.X+1, n.Y))
                continue;              
            }
            catch
            {
              continue;
            }
          }
          if(n.X != current.X && n.Y != current.Y)
                n.G = current.G + 1.4;
          else
                n.G = current.G + 1;
          n.Manhattan(start, goal); // 맨하탄 호출
           
          if(n.F > current.F)
            continue;
          if(openList.Contains(n))
          {
            if(n.G > openList.Find(o=>o.X == n.X && o.Y == n.Y).G)
            {
              
              if(n.X != current.X && n.Y != current.Y)
                n.G = current.G + 1.4;
              else
                n.G = current.G + 1;
              n.Manhattan(start, goal);
              // 동일좌표가 openList에 있는데 맨하탄값이 다를때 새로운값으로 대치
              HeapSort(openList);
              // F값 기준 정렬
            }
            else
              continue;
          }
          costGrid[n.X][n.Y] = (byte)(costGrid[current.X][current.Y] +1);
          n.parent = current;
          openList.Insert(n);    
          HeapSort(openList);
          // F값 기준 정렬
          // print("G " + openList[0].G);  // test
          // print("H " + openList[0].H);
        }
      }
      print(count + " loop");
    }
    return false;
  }
  
  public IEnumerable<Node> Neighbors(Node c)
  {
    int x = c.X;
    int y = c.Y;
    
    yield return new Node(x-1, y);  // 중복된 이웃도 새로만드는 문제가 있음
    yield return new Node(x, y+1);  //   
    yield return new Node(x+1, y);
    yield return new Node(x, y-1);
    yield return new Node(x-1, y-1);
    yield return new Node(x+1, y+1);
    yield return new Node(x-1, y+1);
    yield return new Node(x+1, y-1);
  }  

  public bool IsWall(int X, int Y)
  {
    if(grid[X][Y] == 1)
      return true;
    else
      return false;
  }

  public static void HeapSort(MinHeap<Node> list)
  {
    var h = new MinHeap<Node>();
    for(int i=0; i<list.Count; i++)
    {
      h.Insert(list.RemoveTop());
    }
    while(h.Count > 0)
      list.Insert(h.RemoveTop());
  }  
}

public class Node : IEquatable<Node>, IComparable
{
  public int X{get; set;}
  public int Y{get; set;}
  public double G=0;    // 시작점부터 현재위치까지 이동비용
  public double H;   // 현재위치부터 Goal까지 이동비용 (맨하탄공식이용)
  public double F;
  public Node parent;

  public Node(int x, int y)
  {
    X = x; Y = y;
    parent = null;
  }
  public bool Equals(Node other)
  {
    return X == other.X && Y == other.Y; //&& G == other.G && H == other.H;
  }

  public int CompareTo(object other)
  {
    return F.CompareTo(((Node)(other)).F);
  }

  public void Manhattan(Node start, Node goal)  // 맨하탄 GHF구하는 메소드
  {
    // G = Math.Abs(start.X-X) + Math.Abs(start.Y-Y);
    H = Math.Abs(goal.X-X) + Math.Abs(goal.Y-Y);
    F = G + H;
  }
}

                                  // 대소비교   // 같은지 비교
public class MinHeap<T> where T : IComparable, IEquatable<T>  // Heap Class
{
  List<T> list;
  public int Count
  {
    get
    {
      return list.Count;
    }
  }

  public bool Contains(T v)
  {
    if(list.Contains(v))
      return true;
    else
      return false;
  }

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

  int Parent(int i) { return (i-1)/2; }
  int LChild(int i) { return 2*(i+1)-1; }
  int RChild(int i) { return LChild(i)+1; }

}


public static class ClassExtension
{
  public static string Stringify<T>(this IEnumerable<T> list)
  {
    string s = "";
    foreach(var v in list)
    {
      s += string.Format("{0,2}", v) + " ";  // v를 두자리로 만들어줌 ex 10 => "1" "0"
    }
    if(s.Length > 0)
      s = s.Substring(0, s.Length-1);
    return s;
  }
  public static Node FindNode<Node>(this IEnumerable<Node> list, Node n)
  // openList에서 동일좌표 노드를 찾아서 리턴해줌
  {
    foreach(var v in list)
    {
      if(v.Equals(n))
        return v;
    }
    return n;
  }

  public static IList<T> Swap<T>(this IList<T> list, int i, int j)
  {
    T temp = list[i];
    list[i] = list[j];
    list[j] = temp;
    return list;
  }  
  
}


