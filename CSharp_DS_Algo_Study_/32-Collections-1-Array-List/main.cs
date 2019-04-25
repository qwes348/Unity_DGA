using System;
using System.Collections;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    ArrayList list = new ArrayList();
    list.Add(100);  // 100이 object타입으로 들어감
    print((int)list[0] == 100);  // 따라서 꺼낼때는 캐스팅을 해줘야함
    
    list.Add(200);
    print(list.Count == 2);

    // list.Insert(5, 300);   // ArgumentOutofRangeException 에러
    list.Insert(2, 300);   // 인덱스의 현재까지의 끝이나 요소사이에 넣어야함
    print((int)list[2] == 300);

    list.Remove(100); 
    print((int)list.Count == 2);
    print((int)list[0] == 200);
    print((int)list[1] == 300);
    // Remove로 삭제할경우 뒤 요소들이 앞쪽으로 당겨옴

    foreach(var v in list)
      print(v);

    print(list.Contains(200) == true);   // 괄호안 요소를 포함하는가
    print(list.Contains(700) == false);
  }
}