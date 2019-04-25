using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFSsearch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //RecursiveDFS(transform);
        print("DFS: " + IterativeDFS(transform));
        print("BFS: " + IterativeBFS(transform));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string IterativeDFS(Transform node)
    {
        string s = "";
        var stack = new Stack<Transform>();
        stack.Push(node);

        while(stack.Count > 0)
        {
            Transform n = stack.Pop();
            s += n.name + " ";

            for (int i = n.childCount-1; i >= 0 ; i--)
            {
                stack.Push(n.GetChild(i));
            }   
        }
        return s;
    }

    public string IterativeBFS(Transform node)
    {
        string s = "";
        var queue = new Queue<Transform>();
        queue.Enqueue(node);

        while(queue.Count > 0)
        {
            Transform n = queue.Dequeue();
            s += n.name + " ";

            for (int i = 0; i <= n.childCount-1; i++)
            {
                queue.Enqueue(n.GetChild(i));
            }
        }
        return s;
    }
    
    //public string RecursiveDFS(Transform node)
    //{
    //    string s = node.name + " ";
    //    int childCount = node.childCount;

    //    for (int i = 0; i < childCount; i++)
    //    {
    //        s += RecursiveDFS(node.GetChild(childCount));
    //    }
    //    return s;
    //}
}
