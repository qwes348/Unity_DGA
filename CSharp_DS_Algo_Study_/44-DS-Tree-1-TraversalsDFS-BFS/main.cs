using System;
using System.Collections;
using System.Collections.Generic;


class TreeNode
{
  public List<TreeNode> children; // 가변가능한 List 
  public TreeNode parent;

  public string Name{get; set;} 
  public TreeNode(string name)
  {
    Name = name;
    children = new List<TreeNode>();
    parent = null;
  }

  public void AddChild(TreeNode c)
  {
    children.Add(c);  // List의 Add함수
    c.parent = this;
  }

  public override string ToString()
  {
    return Name;
  }
}

// Tree 클래스-----------------------------------------------------------
class Tree : IEnumerable
{
  public TreeNode Root { get; set; }
  
  // GetEnumerator 메소드 ---------------------------------------------------
  public IEnumerator GetEnumerator()
  {  //using List<> Enumerator     // 1번째 방법
    var list = new List<TreeNode>();
    IterativeDFS(Root, node => list.Add(node));
    return list.GetEnumerator();
    // return new TreeNodeEnumerator(this); // 2번째 방법
  }

  // 3번째 방법 : using yield return  
  public IEnumerable<TreeNode> EnumDFS(TreeNode node)
  {
    var stack = new Stack<TreeNode>();
    stack.Push(node);
    while(stack.Count > 0)
    {
      TreeNode n = stack.Pop();
      yield return n;
      for(int i = n.children.Count-1; i>=0; i--)
      {
        stack.Push(n.children[i]);
      }
    }
  }

  class TreeNodeEnumerator : IEnumerator
  {
    Tree tree;
    List<TreeNode> list;

    int position = -1;
    public TreeNodeEnumerator(Tree _tree)
    {
      tree = _tree;
      list = new List<TreeNode>();
      tree.IterativeDFS(tree.Root, node=>list.Add(node));
      //tree.IterativeBFS(tree.Root, node=>list.Add(node));
    }

    public bool MoveNext()
    {
      if(position < list.Count-1)
      {  
        position++;
        return true;
      }
      else
      {
        return false;
      }

    }

    public object Current
    {
      get
      {
        return list[position];
      }
    }

    public void Reset()
    {
      position = -1;
    }

  }

  public string IterativeDFS(TreeNode node, Action<TreeNode> callback)  
  // 자주쓰는 패턴 스택을이용한 DFS
  {
    string s = "";
    Stack<TreeNode> stack = new Stack<TreeNode>();
    stack.Push(node);
    callback(node);

    while(stack.Count > 0)
    {
      TreeNode n = stack.Pop();
      s += n.Name + " ";

      for(int i = n.children.Count-1; i >= 0; i--)
        stack.Push(n.children[i]);
    }
    return s;
  }

  public string RecursiveDFS(TreeNode node) // 재귀호출을 이용한 DFS방식출력
  {
    string s = node.Name + " ";
    foreach(var n in node.children)
      s += RecursiveDFS(n);
    return s;
  }

  public string IterativeBFS(TreeNode node, Action<TreeNode> callback) 
  // 큐를 이용한 BFS (재귀호출은 불가능) 작성해오기
  {
    string s = "";
    Queue<TreeNode> queue = new Queue<TreeNode>();  // var queue = new Queue<TreeNode>();
    queue.Enqueue(node);
    callback(node);
    
    while(queue.Count > 0)
    {
      TreeNode n = queue.Dequeue();
      s += n.Name + " ";

      for(int i = 0; i <= n.children.Count-1; i++)
        queue.Enqueue(n.children[i]);
    }
    return s;

  }

  public int NodeDepth(TreeNode node)
  {
    int depth = 0;
    if(node.parent == null)
      return depth;
    else
    {
      while(node.parent != null)
      {
        depth++;
        node = node.parent;
      }
    }
    return depth;
  }

  public int TreeHeight()
  {
    Tree tree = this;
    int edge = 0;
    Queue<TreeNode> queue = new Queue<TreeNode>();
    queue.Enqueue(tree.Root);

    while(queue.Count > 0)
    {
      TreeNode n = queue.Dequeue();
      int depthCount = tree.NodeDepth(n);
      if(edge < depthCount)
        edge = depthCount;
      
      for(int i = 0; i <= n.children.Count-1; i++)
        queue.Enqueue(n.children[i]);
    }
    return edge;
  }

  public int TreeDegree()
  {
    Tree tree = this;
    int degree = 0;

    Queue<TreeNode> queue = new Queue<TreeNode>();
    queue.Enqueue(tree.Root);

    while(queue.Count > 0)
    {
      TreeNode n = queue.Dequeue();
      if(degree < n.children.Count)
        degree = n.children.Count;
      
      for(int i = 0; i <= n.children.Count-1; i++)
        queue.Enqueue(n.children[i]);
    }
    return degree;
    
  }
}



class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    Tree tree = new Tree();
    TreeNode root = new TreeNode("root");
    TreeNode a = new TreeNode("a");
    TreeNode b = new TreeNode("b");
    TreeNode c = new TreeNode("c");
    TreeNode d = new TreeNode("d");
    TreeNode e = new TreeNode("e");
    TreeNode f = new TreeNode("f");
    TreeNode g = new TreeNode("g");
    TreeNode h = new TreeNode("h");
    TreeNode i = new TreeNode("i");
    tree.Root = root;
    
    root.AddChild(a); root.AddChild(b); root.AddChild(c);
    a.AddChild(d); a.AddChild(e);
    b.AddChild(f); b.AddChild(g);
    g.AddChild(h); g.AddChild(i); 

    //DFS == Stack방식
    //BFS == Queue방식

    print(tree.IterativeDFS(root, node=>{}) == "root a d e b f g h i c ");
    print(tree.RecursiveDFS(root) == "root a d e b f g h i c ");
    print(tree.IterativeBFS(root, node=>{}) == "root a b c d e f g h i ");

    print(tree.NodeDepth(i) == 3);
    print(tree.NodeDepth(d) == 2);
    print(tree.NodeDepth(a) == 1);
    print(tree.NodeDepth(root) == 0);

    print(tree.TreeDegree() == 3);
    print(tree.TreeHeight() == 3);

    string str = "";
    foreach(var n in tree)
      str += n + " ";
    print(str == "root a d e b f g h i c ");

    str = "";
    foreach(var n in tree.EnumDFS(tree.Root))
      str += n + " ";
    print(str == "root a d e b f g h i c ");

  }
}


// Node depth, Tree degree, Tree height 만들어오기
// doubly linked list, foreachable 가능하게 만들어오기
// Tree, foreachable (DFS방식 이용)