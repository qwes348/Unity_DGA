using System;

class MainClass 
{
  public delegate bool Condition(int index, int value);

  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    int[] scores = {1, 2, 3, 4, 5, 6, 7};
    print(FindFirstIndex(scores, (i,v) => v==7) == 6);
    print(FindFirstIndex(scores, (i,v) => i>5 && v>=7) == 6);
     print(FindFirstIndex(scores, (i,v) => v==0) == -1); // 없는값이면 -1 리턴
  }

  public static int FindFirstIndex(int[] list, Condition condition)
  {
    for(int i = 0; i<list.Length; i++)
    {
      if(condition(i, list[i]))
        return i;
    }
    return -1;
  }

  // 숙제~~~~
  // 스택 자료구조 만들기 int타입 array 활용 push pop 삽입 삭제 
  // 테스트케이스 알아서 만들어오기
}