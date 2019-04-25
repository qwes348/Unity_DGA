using System;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;
    // void printf(object obj); 변수에 함수포인터를 담는다, 콜백
    print("Hwang");
    // print("Hwang"); 오류
    Console.WriteLine("Hwang", 1);

    // int, short : signed
    // uint, ushort : unsigned
    byte byteA = 255;     // unsigned byte, 1111 1111
    byteA++;               //  1111 1111 +1 = 1 0000 0000
    print(byteA == 0);     // overflow !!
    byteA--;
    print(byteA == 255);

    int ix = 128;
    print(Convert.ToString(ix, 2) == "10000000");  // 이진수 변환 기억하기!
    sbyte sy = (sbyte)ix;     // 캐스팅!(형변환)
    print(sy == -128);        // 큰type에서 작은type 형변환은 완벽하지 않다.
    print(Convert.ToString(sy, 2) == "1111111110000000"); 
    // ToString에 sbyte형이 없다 출력된것은 short타입
    print(Convert.ToString((byte)sy, 2) == "10000000");

    float floatA = 0.9f;
    int intA = (int)floatA;   // 실수를 정수로 형변환할때는 소숫점밑을 버린다.
    print(intA == 0);

    float floatB = 1.1f;
    int intB = (int)floatB;   
    print(intB == 1);       // 소숫점 밑을 버렸으므로 1이된다.

    print(12345.ToString() == "12345");     // int를 str로 변환 
    print(int.Parse("12345") == 12345);     // str을 int로 변환 int.Parse 기억하기!!

    float a = 69.6875f;
    double b = (double)a;
    print(a == b);

    // https://www.h-schmidt.net/FloatConverter/IEEE754.html
    const float x = 4.2f;           // const == 상수, 바꾸지 말아야할 값 저장
    // x = 100f;                   // 변수가 아닌 상수로 선언했기때문에 오류
    double y = (double)x;       
    print(y < 4.2);              // float타입의 오차때문에 같지않다
    string strX = String.Format("{0:G9}", x);  // 소숫점아래 9번째자리
    print(strX == "4.19999981");
    print(String.Format("{0}", y) == "4.19999980926514");
    print(y == 4.2f);

    print(4.19999980926514f == 4.2f);
    print(4.1999998f == 4.2f);
    print(4.199999f == 4.2f);     // false

    print((double)x * 10);
    int n = (int)(x * 10);
    print(n == 41);           // int로 캐스팅하면 소숫점아래는 버린다!!! not 42
    
    double d = 4.2;
    print((int)(d * 10) == 42);    // double도 float보단 안전하지만 오류의 여지는 있다.
    print(String.Format("{0:G9}", d));

    print(4.2 == 4.2000000000000001);    // True
    print(4.2 == 4.20000000001);         // False 
    // double이 float 보다 더 아랫자리까지 비교해주지만 일정범위가 넘으면 같다고 표현한다

    print(DiscountedPrice1(100, 0.1f) == 89);  // 1의 오차발생! 11%가 줄어듦
    print(DiscountedPrice2(100, 0.1) == 90);   // 더블형 오차없음
    print(DiscountedPrice3(100, 0.1) == 90);   // decimal형 오차없음

  }

  public static int DiscountedPrice1(int fullPrice, float discount)
  {
    return (int)(fullPrice * (1-discount));
  }

  public static int DiscountedPrice2(int fullPrice, double discount)
  {
    return (int)(fullPrice * (1-discount));
  }

  public static decimal DiscountedPrice3(int fullPrice, double discount)
  {
    return (decimal)(fullPrice * (1-discount));
  }
}