using System;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    print(Plus(10, 10) == 20);

    // static을 안쓰고 함수를 호출하는 과정
    MainClass mc = new MainClass();
    print(mc.Plus2(10, 10) == 20);

    // 1차원 Array
    int[] scores = new int[] {2, 4, 5, 3, 6, 8, 1, 7};
    print(Sum(scores) == 36);
    print(Avg(scores) == 4.5);
    // print(Min(scores) == 1);   숙~~~~제
    // print(Max(scores) == 8);

    // Array 2D
    int[,] list2d = {
      {10, 20},
      {30, 40},
      {50, 60}
    };
    print(list2d[2,0] == 50);

    // Array 3D
    int[,,] list3d = {
      {
        {10, 20},
        {30, 40},
        {50, 60}
      },
      {
          {70, 80},
          {90, 100},
          {110, 120}
      }
    };

    // int sum = 0;
    // for(int i = 0; i < list3d.GetLength(0); i++)
    // {
    //   for(int j = 0; j < list3d.GetLength(1); j++)
    //   {
    //     for(int k = 0; i < list3d.GetLength(2); k++)
    //     {
    //       sum += list3d[i,j,k];
    //     }
    //   }
    // }
    // print(sum);

    // Jagged Array(aka 가변배열)
    int[][] image = new int[][]
    {
      new int[] {0, 0, 0, 0, 0, 0, 0},
      new int[] {0, 0, 1, 1, 1, 0, 0},
      new int[] {0, 0, 1, 0, 1, 0, 0},
      new int[] {0, 0, 1, 0, 1, 0, 0},
      new int[] {0, 0, 1, 0, 1, 0, 0},
      new int[] {0, 0, 1, 1, 1, 0, 0},
      new int[] {0, 0, 0, 0, 0, 0, 0}

    };
    print(image[1][2] == 1);
  }

  public static double Avg(int[] list)
  {
    return Sum(list) / (double)list.Length;  // int/int로 나누면 소숫점이 없다 둘중하나는
                                            // double로 형변환 해야함!
  }

  public static int Sum(int[] list)
  {
    int s = 0;
    // for(int i = 0; i < list.Length; i++)
    // {
    //   s += list[i];
    // }
    foreach(int n in list)   // 파이썬의 for문
      s += n;
    return s;
  }

  public static int Plus(int a, int b)
  {
    return a+b;
  }

  public int Plus2(int a, int b)
  {
    return a+b;
  }

}