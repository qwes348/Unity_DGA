using System;

class MainClass 
{
  static int top = -1;

  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;
    print("**String Length**");
    string str1 = "ABCDEFG";
    string str2 = "1111222233334444555566667";
    print(StringLength(str1)==7);
    print(StringLength(str2)==25);

    print("\n**FindMax**");
    int[] arr1 = new int[] {1, 2, 3, 4, 5};
    print(FindMax(arr1, arr1.Length-1)==5);
    int[] arr2 = new int[] {5, 6, 7, 8, 9};
    print(FindMax(arr2, arr2.Length-1)==9);
    int[] arr3 = new int[] {1};
    print(FindMax(arr3, arr3.Length-1)==1);
    
    int[] stack = new int[3];
    print("\n**Stack Push**");
    Push(stack, 1);
    print(stack[0] == 1);
    
    Push(stack, 2);
    print(stack[1] == 2);
    
    Push(stack, 3);
    print(stack[2] == 3);
    Push(stack, 4); 

    print("**Stack Pop**");
    print(Pop(stack) == 3);
    print(Pop(stack) == 2);
    print(Pop(stack) == 1);
    Pop(stack);

    print("\n**Class Extension**");
    print(arr1.Sum() == 15);
    print((new int[] {5, 6, 7, 8, 9}).Sum() == 35);

    print((new int[] {10, 10, 5}).Avg() == 12.5);
    print((arr1.Avg()) == 3.75);

    print(arr1.Max() == 5);
    print(arr1.Min() == 1);
  }

  public static int StringLength(string str)
  {
    if(str == "")
      return 0;

    return 1+StringLength(str.Substring(1));
  }  

    public static int FindMax(int[] arr, int len)
  { 
    if(len==0)
      return arr[len];
    int max = FindMax(arr,len-1);
    max = max > arr[len]? max : arr[len];
    return max;
  }

  public static void Push(int[] stack, int num)
  {
    if(top == stack.Length-1)
    {
      Console.WriteLine("Stack is Overflow!! \n");
      return;
    }
    else
    {
      top++;
      stack[top] = num;
    }
  }

  public static int Pop(int[] stack)
  {
    int pnum = 0;

    if(top == -1)
    {  
      Console.WriteLine("Stack is Empty!! \n");
      return 0;
    }
    else
    {
      pnum = stack[top];
      stack[top] = 0;
      top--;
      return pnum;
    }
  }
}

public static class ClassExtension
{
  public static int Sum(this int[] arr)
  {
    int sum = 0;
    int len = arr.Length;

    for(int i=0; i<=len-1; i++)
    {
      sum+=arr[i];
    }
    return sum;
  }

  public static double Avg(this int[] arr)
  {
    int sum = arr.Sum();
    int len = arr.Length;

    return (double)sum/(len-1);
  }

  public static int Max(this int[] arr)
  {
    int len = arr.Length;
    int max = 0;

    for(int i=0; i<len; i++)
    {
      if(arr[i]>max)
        max = arr[i];
    }
    return max;
  }

  public static int Min(this int[] arr)
  {
    int len = arr.Length;
    int min = arr[0];

    for(int i=1; i<len; i++)
    {
      if(arr[i]<min)
        min = arr[i];
    }
    return min;
  }  
}

  // 숙제~~~~
  // 테스트케이스 알아서 만들어오기
  // sum, max를 Extension으로 만들어오기
  // 재귀호출을 이용한 문자열길이 구하기
  // 재귀호출을 이용해 array의 최댓값 구하기
  // 스택 자료구조 만들기 int타입 array 활용 push pop 삽입 삭제 