using System;

class MainClass 
{
  enum Animal {MOUSE = -100, CAT, BIRD, DOG = 10, LION}; //열거형 타입 enum 초기화 안하면 0부터 증가
  public static void Main (string[] args) 
  { 
    // Value types: ex)int float double 
    Console.WriteLine("\t\tValue types"); // \t=탭으로 공백
    
    int intA = 100;
    Console.WriteLine(intA == 100); // intA의 값이 100이맞는지 확인
    Console.WriteLine(sizeof(int)==4); 
    
    byte byteA = 100; // byte형 타입 변수 최댓값 255 양수만 표현 언사인드
    Console.WriteLine(byteA == 100);
    Console.WriteLine(sizeof(byte)==1);
    
    sbyte sbyteA = 100; // signed byte타입 부호가있음 -127~127
    Console.WriteLine(sbyteA == 100);
    Console.WriteLine(sizeof(sbyte)==1);
    
    char charA = 'A';
    Console.WriteLine(sizeof(char)==2); 
    Console.WriteLine(charA == 'A' && charA == 65); //A는 아스키 65
    Console.WriteLine('\t'==9); // 탭은 아스키코드 9번
    
    float floatA = 0.1f; // f를빼면 double형 float는 꼭 f를 붙여야한다.
    Console.WriteLine(floatA != 0.1); // 0.1은 더블값, 0.1f는 float값
    Console.WriteLine(floatA == 0.1f);
    Console.WriteLine(sizeof(float)==4);
    
    double doubleA = 0.1; //double형은 f를붙여도 컴파일오류는 뜨진않지만 붙이지 않는다
    Console.WriteLine(doubleA == 0.1);
    Console.WriteLine(0.1f > 0.1); // 오차때문에 0.1f가 더 크다!
    Console.WriteLine((double)0.1f>0.1); // 형변환을 해도 크다
    
    var someA = 0.1; // == double somA = 0.1 컴파일 시점에서 적당한 형으로 변환
    Console.WriteLine(someA.GetType().ToString() == "System.Double"); //double precision
    Console.WriteLine(floatA.GetType().ToString() == "System.Single"); //single precision
    Console.WriteLine(intA.GetType().ToString() == "System.Int32");
    Console.WriteLine(byteA.GetType().ToString() == "System.Byte");
    Console.WriteLine(sbyteA.GetType().ToString() == "System.SByte");

    decimal decimalA = 100000000000000000.123m;
    Console.WriteLine(decimalA == 100000000000000000.123m);
    Console.WriteLine(sizeof(decimal) == 16);
    
    bool boolA = true;  // true false 값을가지는 타입 bool 
    Console.WriteLine(boolA == true);
    Console.WriteLine(boolA != false);
    Console.WriteLine(sizeof(bool) == 1);

    //  enum Animal {MOUSE = -100, CAT, BIRD, DOG = 10, LION};
    Animal enumA = Animal.DOG;  // 가질수있는값이 한정된 enum 특별한 int
    Console.WriteLine(enumA);   // DOG 하지만 "DOG"와는 다르다
    Console.WriteLine(enumA == Animal.DOG);
    Console.WriteLine((int)enumA == 10);
    Console.WriteLine((int)Animal.CAT == -99);
    Console.WriteLine((int)Animal.LION == 11);
    Console.WriteLine(enumA.GetType().ToString() == "MainClass+Animal");
    Console.WriteLine(sizeof(Animal) == 4);
    Console.WriteLine(enumA.GetType().IsValueType == true);


    // Reference types: ex)object, Pointer, 
    //string, class C{...}, interface I{...}
    //, int[] and int [,], delegate int D(...)

    object obj1 = 10;     // boxing 정수10을 변환해서 넣음(?)
    object obj2 = 10;
    int int3 = (int)obj1; // unboxing
    Console.WriteLine(int3 == 10);
    Console.WriteLine((obj1 == obj2) == false);   // 서로다른 10의주소를 가르킨다.
    Console.WriteLine(Object.ReferenceEquals(obj1, obj2) == false); // 주소가 같은지 비교
    Console.WriteLine(Object.Equals(obj1, obj2) == true);     // 값이 같은지 비교
    Console.WriteLine(obj1.Equals(obj2) == true);     // 값이 같은지 비교2 
    Console.WriteLine(obj1.GetType().IsValueType == true);   // 안에들어있는 값의 형으로 확인

    string str1 = "Game";
    string str2 = "Game";
    string str3 = "Academy";
    Console.WriteLine(str1 == str2);    // 주소는달라도 속에 내용까지 비교해줌
    Console.WriteLine(str1.GetType().IsValueType == false); // Reference타입
    object objStr = "Daegu";
    Console.WriteLine(objStr.GetType().IsValueType == false);

    Point pointA = new Point(10, 20);
    Point pointB = new Point(30, 40);
    Console.WriteLine((pointA == pointB) == false);
    Point pointC;
    pointC = pointA;
    Console.WriteLine((pointA == pointC) == true);
    Console.WriteLine(pointA.GetType().IsValueType == false);
    Console.WriteLine(Object.Equals(pointA, pointC) == true); // 같은곳을 가르키니까 이퀄함수가 안에 있는값을 비교안함
    Console.WriteLine(Object.Equals(pointA, pointB) == false);
    Point pointD = new Point(10, 20);
    Console.WriteLine(Object.Equals(pointA, pointD) == true);
    Console.WriteLine(pointA.Equals(pointD)); // 위와같음

    int[] arrayA = new int[2] {1, 2};
    int[] arrayB = new int[2]{
      1,
      2
    };
    Console.WriteLine((arrayA == arrayB) == false); // 값은같지만 주소는 다르다
    Console.WriteLine(arrayA.GetType().IsValueType == false);
    Console.WriteLine(Object.Equals(arrayA, arrayB) == false);

  } // Main

  class Point
  {
    public int x, y;
    public Point(int _x, int _y)
    {
      x = _x;
      y = _y;
    }

    public override bool Equals(object obj)    // override 이미 있는 함수를 무시하고 새로씀
    {
      Console.WriteLine("Equals()");
      if(obj.GetType() != this.GetType())
        return false;
      Point other = (Point) obj;
      return (this.x == other.x) && (this.y == other.y);
    }
    public override int GetHashCode()
    {
      return x ^ y;
    }
  }
}