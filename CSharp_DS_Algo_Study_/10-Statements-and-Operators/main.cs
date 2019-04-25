using System;

class MainClass 
{
  public enum Color {Red, Green, Blue};
  
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    Random rnd = new Random();   //랜덤하게 값을 줌
    Color c = (Color)rnd.Next(0,4);    // 0 ~ 3까지 random value
    
    if(c == Color.Red)
      print(c == Color.Red);
    else if(c == Color.Green)
      print(c == Color.Green);
    else if(c == Color.Blue)
      print(c == Color.Blue);
    else
      print("Unknown Color !");
    
    switch(c)
    {
      case Color.Red:
        print(c == Color.Red);
        break;
      case Color.Green:
        print(c == Color.Green);
        break;
      case Color.Blue:
        print(c == Color.Blue);
        break;
      default:
        print("Unknown Color!");
        break;
    }

    switch(c)
    {
      case Color.Red:
      case Color.Green:
      case Color.Blue:   // Red or Green or Blue => print(c);
        print(c);
        break;
      default:
        print("Unknown Color!");
        break;
    }

    int[] scores = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
    print(SumIf(scores, v => v%2==0) == 30);  // 짝수의 합만구함 람다식 함수
    print(SumIf(scores, v => v%2!=0) == 25);  // 홀수만 합계
    print(SumIf(scores, v => v > 6) == 34);   // 6이상만 합계
    print(SumIf(scores, v => true) == 55);   // 전부합계

    print(SumIf2(scores, v => v%2==0) == 30);   

    int count = 1;
    print(++count == 2);
    count = 1;
    print(count++ == 1);

    int a = 9, b = 8;
    int carry = a+b > 10 ? 1 : 0;  // 올림수 계산 후 carry변수에 담음
    int remainder = a+b > 10? a+b-10 : a+b;  //올림수 빼고 나머지 뒷자리 계산후 remainder에저장
    print(carry == 1 && remainder == 7);     // short-circuit evaluation
  }

  // Func<파라미터, 리턴값>
  // Func<int, bool> == bool Function(int v);
  // condition 은 함수를 가진 변수 == v => v%2==0
  public static int SumIf(int[] list, Func<int, bool> condition)
  {
    int i = 0;
    int sum = 0;
    while (i < list.Length)
    {
      if(!condition(list[i]))
      {
        i++;
        continue;
        // break; == 루프를 종료함 바로리턴으로감
      }
      else
      {
        sum += list[i++];
      }
    }
    return sum;
  }

  public static int SumIf2(int[] list, Func<int, bool> condition)
  {
    int i = 0;
    int sum = 0;

    do  // 한번은 무조건 실행하고 조건을 검사하는 do ~ while문
    {
      if(condition(list[i]))
        sum += list[i];
    } while(++i < list.Length);

    return sum;
  }
}