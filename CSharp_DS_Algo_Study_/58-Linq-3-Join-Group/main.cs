using System;
using System.Collections.Generic;
using System.Linq;

// 숙제
// 모든과목이 60점 이상 평균이 80이상
// 60점미만 과락, 평균80점 미만 과락 SQL로 만들어오기
// 출력은 이름만 출력
// group by into (into뒤에 변수명을 붙여줄수있음 그룹에)
// join into!!
class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;
    List<Student> list = new List<Student>
    {
      new Student() {Id = 0, Name = "ctkim", Height = 175},
      new Student() {Id = 1, Name = "Steve", Height = 167},
      new Student() {Id = 2, Name = "Brown", Height = 180},
      new Student() {Id = 3, Name = "Won", Height = 171},
      new Student() {Id = 4, Name = "JJ", Height = 165}
    };

    List<Score> scores = new List<Score>
    {
      new Score() {StudentId = 0, Subject = "Math", Point = 70},
      new Score() {StudentId = 4, Subject = "Math", Point = 60},
      new Score() {StudentId = 3, Subject = "Math", Point = 90},
      new Score() {StudentId = 2, Subject = "Math", Point = 10},
      new Score() {StudentId = 1, Subject = "Math", Point = 30},
      new Score() {StudentId = 0, Subject = "English", Point = 70},
      new Score() {StudentId = 4, Subject = "English", Point = 60},
      new Score() {StudentId = 3, Subject = "English", Point = 90},
      new Score() {StudentId = 2, Subject = "English", Point = 10},
      new Score() {StudentId = 1, Subject = "English", Point = 30}
    };

    var ss1 = from s in list
              join score in scores on s.Id equals score.StudentId
              select new {Name = s.Name, Subject = score.Subject, Point = score.Point};
    print(ss1.Stringify());
    print(ss1.Count() == 10);
    print(ss1.Average(s => s.Point) == 52);

    var ss2 = 
      from score in scores
      group score by score.Subject; // deferred excution 지연실행이라서 밑에 foreach에서 실행
    
    // var MathPoint = 
    //           from s in list
    //           join score in scores on s.Id equals score.StudentId
    //           where score.Point >= 60 && score.Subject == "Math"
    //           select new {Name = s.Name, Point = score.Point};


    // var EnglishPoint = 
    //           from s in list
    //           join score in scores on s.Id equals score.StudentId
    //           where score.Point >= 60 && score.Subject == "English"
    //           select new {Name = s.Name, Point = score.Point};

    // var grade =
    //     from math in MathPoint
    //     join english in EnglishPoint on math.Name equals english.Name
    //     select new {Name = math.Name, Math = math.Point, English = english.Point};

    var ss3 =
        group(
          from k in list
          join score in scores on s.Id equals score.StudentId
          select new {Name = k.Name, Subject = score.Subject, Point = score.Point};)
          by s.Name into grade;

    

    // foreach(var s in grade)
    // {
    //   int avg = (s.Math+s.English)/2;
    //   if(avg >= 80)
    //     print(s.Name + " is Pass!");
    // }


    // subjectGroup is an IGrouping<string, Score>
    // (key, list<Score>)
    // foreach(var subjectGroup in ss2)
    // {
    //   print(subjectGroup.Key); // 과목명 프린트
    //   foreach(Score score in subjectGroup)
    //     print($"   {score.Point}"); // 점수 프린트
    // }

  }
}

public class Student
{
  public int Id {get; set;}
  public string Name {get; set;}
  public int Height {get; set;}

  public override string ToString() {return Name;}
}

public class Score
{
  public int StudentId {get; set;}
  public string Name {get; set;}
  public string Subject {get; set;}
  public int Point {get; set;}
}

public static class ClassExtension
{
  public static string Stringify<T>(this IEnumerable<T> list) 
  {
    return String.Join("\n", list);
  }
}
