using System;

class MainClass 
{
  public static int Comp(int x, int y) { return x.CompareTo(y);}

  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    int[] scores = {2, 4, 5, 3, 6, 8, 1, 7};
    print(scores.GetType().ToString() == "System.Int32[]");
    print(scores.GetType().BaseType.ToString() == "System.Array");

    print(Stringify(new int[] {2}) == "2");
    print(Stringify(new int[] {}) == String.Empty);    
    print(Stringify(scores) == "2 4 5 3 6 8 1 7");     // Stringify함수의 테스트 

    Array.ForEach( scores, v => Console.Write(v*2 + " ") );  
    // 익명함수 표현법(lamda)용도=콜백
    // 한줄에 정의와 사용을전부함 
    print("\n");

    // Sorting: 오름차순(Ascending order)
    print("\t\tSorting");
    Array.Sort(scores);         // in-place algorithm 제자리 정렬(추가메모리 없이 정렬)
    print(Stringify(scores) == "1 2 3 4 5 6 7 8");   // 정렬됨
    // public static int Comp(int x, int y) { return x.CompareTo(y);} 위에있는것
    Array.Sort( scores, Comp);    // 두번째 인자로 정렬법을 넘겨줌
    print(Stringify(scores) == "1 2 3 4 5 6 7 8");
    Array.Sort( scores, delegate (int x, int y) {return x.CompareTo(y);}); // 간단히
    Array.Sort( scores, (x, y) => x.CompareTo(y) );     // 더간단히
    print(Stringify(scores) == "1 2 3 4 5 6 7 8");
    Array.Sort( scores, (x, y) => x-y);         // 더더간단히 x-y == x.CompareTo(y)

    // 내림차순(Descending order)
    Array.Sort( scores, (x, y) => y.CompareTo(x) );
    print(Stringify(scores) == "8 7 6 5 4 3 2 1");
    Array.Sort( scores, (x, y) => y-x);
    print(Stringify(scores) == "8 7 6 5 4 3 2 1");

    print(1.CompareTo(2) < 0);
    print(2.CompareTo(1) > 0);
    print(1.CompareTo(1) == 0);
    print(2.CompareTo(2) == 0);
    

  }

  public static string Stringify(int[] list)
  {
    return String.Join(" ", list);   // int형 배열을 문자열로 변환
  }
}