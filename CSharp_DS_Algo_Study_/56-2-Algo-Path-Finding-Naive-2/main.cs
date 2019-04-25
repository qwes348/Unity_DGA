using System;
using System.Collections.Generic;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;
    
    int[] list = {1, 12};
    print(list.Stringify() == " 1 12");

    byte[][] grid = new byte[][]
    {
      new byte[] {0, 0, 0, 1, 0, 0, 0},
      new byte[] {0, 0, 0, 1, 0, 1, 0},
      new byte[] {0, 0, 0, 0, 0, 1, 0},
      new byte[] {0, 0, 0, 0, 1, 1, 0},
      new byte[] {0, 0, 1, 1, 0, 0, 0},
      new byte[] {0, 0, 0, 0, 0, 1, 1},
      new byte[] {0, 0, 0, 0, 0, 0, 0}
    };

    NaiveSerach naive = new NaiveSerach(grid);
    naive.FindPath(new Node(0, 0), new Node(6, 6));
    print(naive.pathGrid[0].Stringify());
    print(naive.pathGrid[1].Stringify());
    print(naive.pathGrid[2].Stringify());
    print(naive.pathGrid[3].Stringify());
    print(naive.pathGrid[4].Stringify());
    print(naive.pathGrid[5].Stringify());
    print(naive.pathGrid[6].Stringify());

    print("\n");

    print(naive.costGrid[0].Stringify());
    print(naive.costGrid[1].Stringify());
    print(naive.costGrid[2].Stringify());
    print(naive.costGrid[3].Stringify());
    print(naive.costGrid[4].Stringify());
    print(naive.costGrid[5].Stringify());
    print(naive.costGrid[6].Stringify());
  }
}

public class NaiveSerach
{
  public byte[][] grid;
  public byte[][] pathGrid;
  public byte[][] costGrid;
  
  public NaiveSerach(byte[][] grid)
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
    var openList = new Queue<Node>();
    var closedList = new List<Node>();  // 큐에는 find함수가 없으므로 List로 생성
    int count = 0;

    openList.Enqueue(start);
    while(openList.Count > 0)
    {
      count++;     // loop Count
      Node current = openList.Dequeue();
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
          if(grid[n.X][n.Y] == 1)     // Wall
            continue;
          if(openList.Contains(n))
            continue;

          costGrid[n.X][n.Y] = (byte)(costGrid[current.X][current.Y] +1);
          // cost(c,n) = 1  (1이동햇으니까 1더해준다)
          n.parent = current;
          openList.Enqueue(n);    
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
  }
}

public class Node : IEquatable<Node>
{
  public int X{get; set;}
  public int Y{get; set;}
  // public int G{get; set;}
  public Node parent;

  public Node(int x, int y)
  {
    X = x; Y = y;
    parent = null;
  }
  public bool Equals(Node other)
  {
    return X == other.X && Y == other.Y;
  }
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
  
}