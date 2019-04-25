using System;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    print("*double형 Swap*");
    double doubleA = 10.5;
    double doubleB = 11.5;

    SwapDouble(ref doubleA, ref doubleB);
    print(doubleA == 11.5 && doubleB == 10.5);
    
    
    print("*int형 배열의 Swap*");
    int[] arrA = new int[] {1, 2, 3, 4, 5};
    int[] arrB = new int[]  {5, 4, 3, 2, 1};

    SwapArr(arrA, arrB);
    for(int i=0; i<arrA.Length; i++)
    {
      print(arrB[i] == arrA[(arrA.Length-1)-i]);
    }
    for(int i=0; i<arrA.Length; i++)
    {
      print(arrA[i] == arrB[(arrA.Length-1)-i]);
    }


    print("*Book클래스의 개체 bookA 와 bookB Swap*");
    Book bookA = new Book("HarryPoter", 30000);
    Book bookB = new Book("OnePiece", 5000);

    SwapBook(bookA, bookB);
    print(bookB.title == "HarryPoter");
    print(bookB.price == 30000);


    print("*int형 배열의 min, max Swap*");
    int[] arrC = new int[] {6, 7, 8, 9, 10};
    SwapMinMax(arrC);
    print(arrC[0] == 10);
    print(arrC[4] == 6);
  }

  public static void SwapDouble(ref double doubleA, ref double doubleB)
  {
    double temp = doubleA;
    doubleA = doubleB;
    doubleB = temp;
  }

  public static void SwapArr(int[] arrA, int[] arrB)
  {
    if(arrA.Length == arrB.Length)
    {
      int[] temp = new int[5];

      for(int i=0; i<arrA.Length; i++)
      {
        temp[i] = arrA[i];
      }
      for(int i=0; i<arrB.Length; i++)
      {
        arrA[i] = arrB[i];
      }
      for(int i=0; i<temp.Length; i++)
      {
        arrB[i] = temp[i];
      }
    }
    else
      Console.WriteLine("두 배열의 길이가 다릅니다.");
      return false;

  }

  public static void SwapBook(Book bookA, Book bookB)
  {
    string tempTitle = bookA.title;
    int tempPrice = bookA.price;

    bookA.title = bookB.title;
    bookA.price = bookB.price;

    bookB.title = tempTitle;
    bookB.price = tempPrice;
  }

  public static void SwapMinMax(int[] arrC)
  {
      int min = 0;
      int max = 0;
      int[] temp = new int[arrC.Length];
      for(int i=0; i<arrC.Length; i++)
      {
        if(arrC[i] >= max)
          max = i;
        else if(arrC[i] <= min)
          min = i;
      }
      temp[max] = arrC[max];
      arrC[max] = arrC[min];
      arrC[min] = temp[max];
      
  }

  public class Book
  {
    public string title;
    public int price;

    public Book(string _title, int _price)
    {
      title = _title;
      price = _price;
    } 
  }
}