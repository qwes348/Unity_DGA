using System;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Polygone mypoly = new Polygone();
    mypoly.sides = 4;
  }

  class Polygone
  {
    public int sides;
    

    // public int[] FaceInput(int facecnt)
    // {
    //   int[] sidesArr = new int[facecnt];

    //   for(int i = 0; i<=facecnt-1; i++)
    //   {
    //     string input;
    //     Console.Write("input face's length: ");
    //     input = Console.ReadLine();
    //     sidesArr[i] = Int32.Parse(input);
    //     Console.WriteLine();

    //   }
    //   return sidesArr;
    }
  }

  class Rectangle : Polygone
  {
    int length;
    int width;

    public int area{int length, int width}
    {
      return length * width;
    }
  }

  class Square : Polygone
  {
    int length;

    public int area{int length}
    {
      return length*length;
    }
  }

  class Etriangle : Polygone
  {
    int side;
    double height = Math.Sqrt(3) / 2 * side;

    public double area{int side, double height}
    {
      return side * double * 0.5;
    }
  }

  class Circle : Polygone
  {
    
  }

}


// 숙제 다각형의 넓이를 구하는 코드를 상속개념으로 짜오기
// 결과는 array에 담기
// 직사각형 정사각형 원 