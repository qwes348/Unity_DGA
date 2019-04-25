using System;
using System.Collections;
using System.Collections.Generic;

// https://en.wikipedia.org/wiki/Flood_fill
class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;
    int[,] image = new int[,]  
    {
      {0, 0, 0, 0, 0, 0, 0, 0},
      {0, 0, 1, 1, 1, 1, 0, 0},  // [1,2]~[1,5]
      {0, 0, 1, 0, 0, 1, 0, 0},  // [2,2]
      {0, 0, 1, 0, 0, 1, 0, 0},  // [3,2]
      {0, 0, 1, 0, 0, 1, 0, 0},  // [4,2]
      {0, 0, 1, 0, 0, 1, 0, 0},  // [5,2]
      {0, 0, 1, 1, 1, 1, 0, 0},  // [6,2]~[6,5]
      {0, 0, 0, 0, 0, 0, 0, 0}
    };
    print("");
    print("Case: 1");
    paint(image, 1, 2);
    paintPrint(image);
    print("");
    print("");
    paint(image, 2, 3);
    paintPrint(image);
    print("");
    print("");
    paint(image, 0, 0);
    paintPrint(image);
    
    image = new int[,]  
    {
      {0, 0, 0, 0, 0, 0, 0, 0},
      {0, 0, 0, 0, 0, 0, 0, 0},  
      {0, 0, 0, 0, 0, 0, 0, 0}, 
      {1, 1, 1, 1, 1, 1, 1, 1}, 
      {0, 0, 0, 1, 0, 0, 0, 0},  
      {0, 0, 0, 1, 0, 0, 0, 0},  
      {0, 0, 0, 1, 0, 0, 0, 0}, 
      {0, 0, 0, 1, 0, 0, 0, 0}
    };
    print("");
    print("");
    print("Case: 2");    
    paint(image, 0, 0);
    paintPrint(image);
    print("");
    print("");
    paint(image, 4, 0);
    paintPrint(image);
    print("");
    print("");
    paint(image, 4, 4);
    paintPrint(image);
    print("");
    print("");        

  }

  public static void paint(int[,] image, int i, int j)
  {
    Queue<int> painter = new Queue<int>();
    painter.Enqueue(image[i, j]);
    image[i, j] = 7;

    while(painter.Count > 0)
    {
      int n = painter.Dequeue();
      // Console.WriteLine("loop");
      if(j>0 && image[i, j-1] == n)   // west
      {
        painter.Enqueue(image[i, j-1]);
        image[i, j-1] = 7;
        if(j>0)
          j--;
      }
      else if(j<image.GetLength(0)-1 && image[i, j+1] == n) // east
      {
        painter.Enqueue(image[i, j+1]);
        image[i, j+1] = 7;
        if(j<image.GetLength(0)-1)
          j++;
      }
      else if(i>0 && image[i-1, j] == n)  // north
      {        
        painter.Enqueue(image[i-1, j]);
        image[i-1, j] = 7;
        if(i>0)
          i--;
      }
      else if(i<image.GetLength(1)-1 && image[i+1, j] == n) // south
      {        
        painter.Enqueue(image[i+1, j]);
        image[i+1, j] = 7;
        if(i<image.GetLength(1)-1)
          i++;
      }
    }
    return;
  }

  public static void paintPrint(int[,] image)
  {
    for(int i=0; i<image.GetLength(1); i++)
    {
      for(int j=0; j<image.GetLength(0); j++)
      {
        Console.Write(image[i, j]);
      }
      Console.WriteLine("");
    }
    return;
  }
}

// 2차원 array 를 이미지처럼 만들어서
// 그림판의 페인트를 구현
// 찍은위치 숫자의 네방향을 보면서 같은수가 나오면 색칠
// 처리방식은 BFS, 색칠은 큐를 이용