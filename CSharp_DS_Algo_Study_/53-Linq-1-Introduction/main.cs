using System;
using System.Collections.Generic;
using System.Linq;

// Linq: Language INtegrated Query
// 일종의 쿼리 표현식
// Query: 다양한 형태의 Data source로부터 일관된 방식으로 데이터를 추출하는 표현식
// Data Source: DB, ADO.NET Data sets, .NET Collections
// IEnumerable<T> type은 모두 Linq 적용이 가능하다 == Queryable type

// Deferred execution : foreach, ToArray(), ToList(), Count(), Max(), Average()...즉시 실행
// 컴파일러가 알아서 함수형식으로 변경 시킴
// IEnumerable Extention Method로 정의
// using System.Linq
// ClassExtension으로 구현

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    // 1. Data Source
    int[] numbers = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
    // 2. Query creation: Query syntex
    IEnumerable<int> evens =   // Enumerable한 변수는 모두 사용할수 있음
      from n in numbers       // data source
      where n%2 == 0          // filtering
      select n;               // result type
      // SQL

    // 3. Query excution: Deferred execution (evens가 실행되는 구간 전에는 실행안됨)
    foreach(int n in evens)
      print(n);
    
    print(evens);
    
    // 즉시 실행: evens가 foreach를 만나지않아도 즉시 실행됨
    print(evens.ToArray().Stringify() == "2 4 6 8 10");
    print(evens.ToList().Stringify() == "2 4 6 8 10");
    print(evens.Stringify() == "2 4 6 8 10");

    // Method syntex
    IEnumerable<int> evens2 =
      numbers.Where(n =>n%2==0).Select(n => n);
    print(evens2.Stringify() == "2 4 6 8 10");
    print(evens2);

    List<Student> list = new List<Student>
    {
      new Student(){Name="ctkim", Height=175, Scores=new List<int>() {100, 70, 90, 77, 88}},
      new Student(){Name="Steve", Height=167, Scores=new List<int>() {77, 60, 90, 77, 55}},
      new Student(){Name="Brown", Height=180, Scores=new List<int>() {30, 61, 91, 100, 57}},
      new Student(){Name="Won", Height=171, Scores=new List<int>() {100, 100, 91, 100, 100}},
      new Student(){Name="JJ", Height=165, Scores=new List<int>() {10, 100, 9, 100, 100}}
    };
    // List version
    print(list.Stringify() == "ctkim Steve Brown Won JJ");
    List<Student> ss1 = list.FindAll( s => s.Height < 175);  // 람다도 사용가능!!
    print(ss1.Stringify() == "Steve Won JJ");

    // Linq version
    IEnumerable<Student> ss2 = 
              from student in list  // 타입명을 var로써도 가능
              where student.Height < 175  // if
              select student;       // return
    print(ss2.Stringify() == "Steve Won JJ");              
    // print(ss2);

    IEnumerable<Student> ss3 = 
              from student in list  // 타입명을 var로써도 가능
              where student.Height < 175  
              orderby student.Height  // 오름차순 정렬
              select student;      
    print(ss3.Stringify() == "JJ Steve Won");      

    IEnumerable<Student> ss4 = 
              from student in list  // 타입명을 var로써도 가능
              where student.Height < 175  
              orderby student.Height descending // 내림차순 정렬
              select student;       
    print(ss4.Stringify() == "Won Steve JJ");         

    IEnumerable<string> ss5 = 
              from student in list  
              where student.Height < 175  
              orderby student.Height descending // 내림차순 정렬
              select student.Name;       // String 타입
    print(ss5.Stringify() == "Won Steve JJ");          

    var ss6 = 
              from student in list  // 타입명을 var로써도 가능
              where student.Height < 175 
              orderby student.Height 
              select new {Name=student.Name, InchHeight=student.Height*0393}; // 익명객체
    print(ss6.Stringify());    

    // ss6 Method version
    var ss6_ = list
              .Where(student => student.Height < 175)
              .OrderBy(student => student.Height)
              .Select(student => new {Name=student.Name, InchHeight=student.Height*0393});
    print(ss6_.Stringify());              

    print("\n");
    var ct = new {Name = "ctkim", Height = 178}; // 익명객체(임시클래스에 임시 객체에 들어감)
    print(ct.Name);
    print(ct.GetType().ToString());

    var ss7 =
      from s in list
      let totalScore = s.Scores[0] + s.Scores[1] + s.Scores[2] + s.Scores[3] + s.Scores[4] //변수선언
      select totalScore;
    print(ss7.Stringify() == "425 359 339 491 319");
    double averageScore = ss7.Average();
    print(averageScore.ToString() == "386.6");
  }
}

public class Student
{
  public string Name { get; set;}
  public int Height { get; set;}
  public List<int> Scores {get; set;}
  public override string ToString()
  {
    return Name;
  }
}

public static class ClassExtension
{
  public static string Stringify<T>(this IEnumerable<T> list) 
  {
    return String.Join(" ", list);
  }
}