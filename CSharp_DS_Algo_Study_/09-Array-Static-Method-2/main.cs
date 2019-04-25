using System;

class MainClass 
{
  public static int Comp(int x, int y) { return x.CompareTo(y);}

  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    string[] names = new string[] {"c", "b", "a", "d"};
    print(names.GetType().ToString() == "System.String[]");
    print(names.GetType().BaseType.ToString() == "System.Array");

    print(Stringify(new string[] {"a"}) == "a");
    print(Stringify(new string[] {}) == String.Empty);    
    print(Stringify(names) == "c b a d"); 

    Array.Sort(names);
    print(Stringify(names) == "a b c d"); 
    Array.Sort(names, (string x, string y) => x.CompareTo(y));
    print(Stringify(names) == "a b c d"); 
    Array.Sort(names, ( x,  y) => y.CompareTo(x));
    print('a'.CompareTo('b') < 0);    // a = 97, b = 98 아스키코드
    print('b'.CompareTo('a') > 0);
    print('a'.CompareTo('a') == 0);
    print(Stringify(names) == "d c b a");
    
    User[] users = new User[3]
    {
      new User("Betty", 23),
      new User("Susan", 20),
      new User("Lisa", 25)
    };
    // Array.Sort(users);  // No IComperable 에러 정렬할 수 있는 기준이 없음
    Array.Sort(users, (u1, u2) => u1.age.CompareTo(u2.age));  // 나이순으로 오름차순
    Array.Sort(users, (u1, u2) => u1.age-u2.age);
    foreach(User user in users)
      Console.Write(user.name + user.age + " ");
    Console.WriteLine();

    Array.Sort(users, (u1, u2) => u1.name.CompareTo(u2.name));  
    // 이름순으로 오름차순 내림차순은 u1.name 과 u2.name의 위치를 바꾼다
    foreach(User user in users)
      Console.Write(user.name + user.age + " ");    

      
  }

  class User
  {
    public string name;
    public int age;
    public User(string _name, int _age)
    {
      name = _name;
      age = _age;
    }
  }

  public static string Stringify(string[] list)
  {
    return String.Join(" ", list);   // int형 배열을 문자열로 변환
  }
}