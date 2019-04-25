using System;
using System.Collections.Generic;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    int[] array = {1, 2, 3};
    print(array.Stringify() == "1 2 3"); //
    print("abc".Stringify() == "a b c");
    print((new char[] {'a', 'b', 'c'}).Stringify() == "a b c");

    List<int> list2 = new List<int>() {8, 3, 2};
    print(list2.Stringify() == "8 3 2");

    print((new int[] { 1 }).Stringify() == "1");
  }

}

public static class ClassExtension
{
  public static string Stringify<T>(this IEnumerable<T> list) 
  // 함수를 this - 오브젝트의 클래스에 끼워넣음
  // 열거형 using System.Collections.Generic; 필수
  // array string list stack 큐 ==> 열거가능한 형
  //
  {
    return String.Join(" ", list);
  }
  // public static string Stringify(this string list) 
  // {
  //   return String.Join(" ", list);
  // }
  // public static string Stringify(this char[] list) 
  // {
  //   return String.Join(" ", list);
  // }

  // sum, max를 Extension으로 만들어오기
}