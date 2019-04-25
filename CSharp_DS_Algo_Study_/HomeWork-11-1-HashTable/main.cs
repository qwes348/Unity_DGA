using System;
// using System.Collections;
using System.Collections.Generic;

// https://bcho.tistory.com/1072
// https://en.wikipedia.org/wiki/Hash_table
// https://d2.naver.com/helloworld/831311
// https://docs.microsoft.com/ko-kr/dotnet/api/system.collections.generic.linkedlist-1?view=netframework-4.7.2

 
class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    Hashtable<string> ht = new Hashtable<string>();

    print(ht.Add("txt", "notepad.exe") == true);  // 삽입
    print(ht.Add("bmp", "paint.exe") == true);
    print(ht.Add("dib", "paint.exe") == true);
    print(ht.Add("rtf", "wordpad.exe") == true);

    print(ht.Count() == 4);             // 카운트
    print(ht.Add("txt", "winword.exe") == false);   // 중복값삽입
    print(ht.Count() == 4);

    print(ht.Remove("txt") == true);          // 삭제
    print(ht.ContainsKey("txt") == false);    // 값의 유무확인
    print(ht.ContainsKey("bmp") == true);
    print(ht.Count() == 3);

    print(ht.Stringify() == "rtf wordpad.exe dib paint.exe bmp paint.exe");
    
    ht["max"] = "3dsmax.exe";               // 인덱서
    print(ht.Count() == 4);

    print((string)ht["max"] == "3dsmax.exe");

    // Hashtable<User> users = new Hashtable<User>();
    // users[100] = new User(100, "Hwang", 20);
    // users[101] = new User(101, "brown", 30);
    // users[102] = new User(102, "hash", 10);
    // print(users.Count() == 3);
  }
}
 
public class Node<T>  // 노드 클래스
{
  public T data;
  public T key;
  public Node<T> next;

  public Node(T key, T data) 
  {
    this.data = data;
    this.key = key;
    next = null;
  }
} 

public class Hashtable<T>   // 해시테이블 클래스
{

  // LinkedList<Node<T>>[] entries = new LinkedList<Node<T>>[50];
  Node<T>[] buckets = new Node<T>[50];
  

  public int hashcode(T key)  // 해시코드
  {
    int index = key.ToString().Length % buckets.Length;

    return index;
  }

  public bool Add(T key, T data)  // Add함수
  {
    Node<T> node = new Node<T>(key, data);
    int index = hashcode(key);  // 해시코드 구함

    // 충돌이 일어났을때
    if(buckets[index] != null && index == hashcode(buckets[index].key))
    {
      // 충돌된 값이 같은 키일때
      if(buckets[index] != null && buckets[index].key.Equals(node.key))
      {  
        Console.WriteLine("already exists!");
        return false;
      }

      // 충돌된값이 다른키일때 Linkedlist처럼 연결
      node.next = buckets[index].next;
      buckets[index].next = node;
      return true;
    }
    else    // 인덱스가 비었을때 바로 삽입
    {
      buckets[index] = node;
      return true;
    }
  }

  public T this[T key]  // Indexer
  {
    get 
    {
      return FindData(key);
    }
    set 
    {
      T data = value;
      Add(key, data);
    }
  }

  public int Count()  // Count함수
  {
    int count = 0;
    Node<T> current = null;
    for(int i = 0; i<buckets.Length-1; i++)
    {
      current = buckets[i];
      while(current != null)
      {
        // Console.WriteLine(current.data);
        count++;
        current = current.next;  // 연결리스트에 끝까지 확인후 빠져나가 인덱스 증가
      }
    }
    return count;
  }

  public bool Remove(T key) // Remove 함수
  {
    int index = hashcode(key);
    Node<T> current = buckets[index];

    while(current != null)
    {
      if(current.key.Equals(key))  // 삭제할 키값을 찾았을때
      {
        buckets[index] = current.next;
        return true;
      }
      else if(current.next != null)  // LinkedList 확인
      {
        if(current.next.key.Equals(key))   
        {
          Node<T> newNext = current.next.next;
          current.next = newNext;
          return true;
        }
        else
        current = current.next;  // LinkedList next
      }
    }

    return false;
  }

  public bool ContainsKey(T key) // Contains함수 Count함수 재활용
  {
    Node<T> current = null;  
    
    for(int i = 0; i<buckets.Length-1; i++)
    {
      current = buckets[i];
      while(current != null)
      {
        if(key.Equals(current.key))
          return true;
        current = current.next;
      }
    }
    return false;
  }

  public T FindData(T key)  // Indexer용 데이터찾기함수 Count함수 활용
  {
    Node<T> current = null;

    for(int i = hashcode(key); i<buckets.Length-1; i++) // 해시코드 인덱스 부터 끝까지 확인
    {
      current = buckets[i];
      while(current != null)
      {
        if(key.Equals(current.key))
          return current.data;
        current = current.next;
      }
    }
    return default(T);
  }

  public string Stringify()
  {
    Node<T> current = null;
    var list = new List<string>();

    for(int i = 0; i<buckets.Length-1; i++)
    {
      current = buckets[i];
      while(current != null)
      {
        list.Add(current.key.ToString());
        list.Add(current.data.ToString());
        current = current.next;
      }
    }
    return list.Stringify();
  }

}

public class User
{
  public int Id { get; set; }
  public string Name { get; set; }
  public int Age { get; set; }
  public User(int id, string name, int age)
  {
    Id = id;
    Name = name;
    Age = age;
  }
}

public static class ClassExtension
{
  public static string Stringify<T>(this IEnumerable<T> list)
  {
    return String.Join(" ", list);
  }
}



    //1: 싱글리스트 2:트리

    //구현해야할 메소드들
    //Stringify  ;;
    //Remove(bool) ;;
    //Containskey ;;
    //Add(bool) 이미있는것은 false 리턴 ;;
    //Count   ;;
    //Generic으로 짤것

    // index값은 임의의 hash함수를 통해 나온 수와 arraysize 의 %연산을 한값으로한다
    // 이번에 hash함수는 string의 Length로 한다
    // index = key.hashcode() % arraysize
    //키값의 충돌이나면 연결리스트를 이용