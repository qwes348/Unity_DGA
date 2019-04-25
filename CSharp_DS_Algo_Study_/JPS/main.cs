using System;
using System.Collections;
using System.Collections.Generic;


// https://zerowidth.com/2013/05/05/jump-point-search-explained.html
// https://joonleestudio.tistory.com/28
// https://gamedevelopment.tutsplus.com/tutorials/how-to-speed-up-a-pathfinding-with-the-jump-point-search-algorithm--gamedev-5818
// https://blog.naver.com/chan00365/220855146262

// 전방향 검색중 forced neighbor를 만나면 중단
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

    JumpSearch jump = new JumpSearch(grid);
    jump.FindPath(new Node(3, 3), new Node(6, 6));
    print(jump.pathGrid[0].Stringify());
    print(jump.pathGrid[1].Stringify());
    print(jump.pathGrid[2].Stringify());
    print(jump.pathGrid[3].Stringify());
    print(jump.pathGrid[4].Stringify());
    print(jump.pathGrid[5].Stringify());
    print(jump.pathGrid[6].Stringify());

    print("\n");

    print(jump.costGrid[0].Stringify());
    print(jump.costGrid[1].Stringify());
    print(jump.costGrid[2].Stringify());
    print(jump.costGrid[3].Stringify());
    print(jump.costGrid[4].Stringify());
    print(jump.costGrid[5].Stringify());
    print(jump.costGrid[6].Stringify());    

    byte[][] grid2 = new byte[][]
    {
      new byte[] {0, 0, 1, 0, 1, 0, 0},
      new byte[] {0, 0, 0, 0, 1, 0, 0},
      new byte[] {0, 0, 0, 0, 1, 0, 0},
      new byte[] {0, 0, 0, 0, 0, 1, 0},
      new byte[] {0, 1, 0, 0, 0, 0, 0},
      new byte[] {0, 0, 1, 0, 0, 0, 0},
      new byte[] {0, 0, 1, 0, 1, 0, 0}
    };    

    jump = new JumpSearch(grid2);
    jump.FindPath(new Node(6, 0), new Node(0, 6));
    print(jump.pathGrid[0].Stringify());
    print(jump.pathGrid[1].Stringify());
    print(jump.pathGrid[2].Stringify());
    print(jump.pathGrid[3].Stringify());
    print(jump.pathGrid[4].Stringify());
    print(jump.pathGrid[5].Stringify());
    print(jump.pathGrid[6].Stringify());

    print("\n");

    print(jump.costGrid[0].Stringify());
    print(jump.costGrid[1].Stringify());
    print(jump.costGrid[2].Stringify());
    print(jump.costGrid[3].Stringify());
    print(jump.costGrid[4].Stringify());
    print(jump.costGrid[5].Stringify());
    print(jump.costGrid[6].Stringify());       
  }
}

public class JumpSearch
{
  public byte[][] grid;
  public byte[][] pathGrid;  // 최종 길
  public byte[][] costGrid; 
  
  public JumpSearch(byte[][] grid)
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
          if(IsWall(current.X+1, current.Y) && IsWall(n.X-1, n.Y))
                continue;
          if(IsWall(current.X-1, current.Y) && IsWall(n.X+1, n.Y))
                continue;              
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

  public bool FindPathJPS(Node start, Node goal)
  {

  }
  public Node FindSuccessor(Node current, Node start, Node goal)
  {
    var openList = new Queue<Node>();
    var closedList = new List<Node>();
    Node successor = null;
    Node neighbors = Neighbors(current);
    Node jumpPoint = null;

    foreach(Node neighbor in neighbors)
    {
      int dx = Math.Clamp(neighbor.X - current.X, -1, 1);
      int dy = Math.Clamp(neighbor.Y - current.Y, -1, 1);

      jumpPoint = Jump(current.X, current.Y, dx, dy, start, goal);

      if(jumpPoint)
        successor = jumpPoint;
    }
    return successor;    
  }

  public Node Jump(int cx, int cy, int dx, int dy, Node start, Ndoe goal)
  {
    int nextX = cx+dx;
    int nextY = cy+dy;

    if(IsWall(nextX, nextY))
      return null;
    if(nextX == goal.X && nextY == goal.Y)
      return new Node(nextX, nextY);
    // 대각선 케이스
    if(dx != 0 && dy != 0)
    {
      if(/* 대각선 이웃집 확인 */ )
        return Node.pooleNode(nextX, nextY);
      if(Jump(nextX, nextY, dx, 0, start, goal) != null || 
       Jump(nextX, nextY, 0, dy, start, goal) != null)
        return Node.pooleNode(nextX, nextY);
    }
    else
    {
      // Hori case
      if(dx != 0)
      {
        if(/* Hori 이웃집 확인 */)
          return Node.pooleNode(nextX, nextY);
      }      
      // Verti case
      else
      {
        if(/* Verti 이웃집 확인 */)
          return Node.pooleNode(nextX, nextY);
      }
    }

    // 이웃집을 못찾으면 다음점프포인트 시도
    return Jump(nextX, nextY, dx, dy, start, goal);
  }

  public bool IsWall(int X, int Y)
  {
    if(X >= grid.Length || Y >= grid[0].Length)
      return true;
    else if(X < 0 || Y < 0)
        return true;
    else if(grid[X][Y] == 1)
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


