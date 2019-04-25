using System;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;
    Book b = new Book();
    b.SetTitle("LOR");
    print(b.GetTitle() == "LOR");

    Book2 b2 = new Book2();
    b2.Title = "Hobbit";
    print(b2.Title == "Hobbit");
    b2.Title = "";
    print(b2.Title == "None");

    Book3 b3 = new Book3();
    b3.Title = "Bible";
    print(b3.Title == "Bible");

    Book4 b4 = new Book4 { // object initializer
      Title = "TLOR", 
      Price = 30000
      };
    print(b4.Price == 30000);
  }

  class Book
  {
    string title;  // 보호수준을 생략하면 디폴트값은 private
    public string GetTitle() {return title;}  // 출력 Getter
    public void SetTitle(string title) {this.title = title;} // 입력 Setter
  }

  class Book2 // property 관례상 property변수의 첫글자는 대문자로쓴다 Title
  {
    string title;  
    public string Title
    {
      get  // 출력
      {
        return title;
      }
      set  // 입력
      {
        if(value == "")
          title = "None";
        else
          title = value;  // value는 예약어
      }
    }
  }

  class Book3
  {
    // 변수선언조차 필요없음
    public string Title { get; set; }
  }

  class Book4
  {
    public string Title { get; set; }
    public int Price {get; set;}
  }
}