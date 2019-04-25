using System;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    int a = 10;
    int b = 20;
    Swap(ref a, ref b);   // 주소값을 전달 ref
    print(a == 20 && b == 10);

    Point pointA = new Point(10, 20);
    Point pointB = new Point(30, 40);
    Swap(pointA, pointB);
    print(pointA.x == 30);

    int sum;
    Sum(10, 20, out sum);  // out 매개변수 => sum의 주소를 받아옴 ref와 기능의 거의 같지만
                          //out은 output용도로만 씀 즉 함수안에서 sum의 값을 채워서 내보내줌
    print(sum == 30);

  }


  public static void Sum(int a, int b, out int sum)
  {
    sum = a + b;
  }

  public static void Swap(ref int a,ref  int b)
  // 매개변수에 주소값 전달을 하는의미로 ref를 붙임. ref == &
  {
    int temp = a;
    a = b;
    b = temp;
  }

  public static void Swap(Point a, Point b)  // point 변수는 그자체로 주소를 가르키는 변수이기때문에(포인터이기 때문에) 매개변수에 ref를 안붙여도 swap할수 있다.
  {
    int x = a.x;
    int y = a.y;

    a.x = b.x;
    a.y = b.y;

    b.x = x;
    b.y = y;
  }

  public class Point   // 전수업에서 복사해온 클래스 point
  {
    public int x, y;
    public Point(int _x, int _y)
    {
      x = _x;
      y = _y;
    }

    public override bool Equals(object obj)    // override 이미 있는 함수를 무시하고 새로씀
    {
      Console.WriteLine("Equals()");
      if(obj.GetType() != this.GetType())
        return false;
      Point other = (Point) obj;
      return (this.x == other.x) && (this.y == other.y);
    }
    public override int GetHashCode()
    {
      return x ^ y;
    }
  }
}

/* Swap
1. double형 Swap(double a, double b)를 작성하세요.
2. int[]형 Swap(int[] arrayA, int[] arrayB)를 작성하세요. 배열사이즈가 서로 다른경우
호출자에게 알려주세요.
3. Book(멤버가 Title, Price)형(클래스) Swap(Book a, Book b)를 작성하세요.
4. 하나의 int[]를 주어졌을 때 min, max 값을 서로 swap 시키는
SwapMinMax(int[] array)를 작성하세요.
5. 근의공식을 함수형태로 바꿔오기
6. 테스트 케이스를 통해 출력값은 true만 뜨게 만들어올 것. */