using System;
using System.Collections.Generic;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    int[] list = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
    print(BinarySerach(list, 0, 0, list.Length-1) == 0);
    print(BinarySerach(list, 7, 0, list.Length-1) == 7);
    print(BinarySerach(list, 100, 0, list.Length-1) == -1);

    print(BinarySerach2(list, 0) == 0);
    print(BinarySerach2(list, 7) == 7);
    print(BinarySerach2(list, 100) == -1);

    list = new int[] { 8 };
    print(BinarySerach(list, 8, 0, list.Length-1) == 0);
    print(BinarySerach(list, 100, 0, list.Length-1) == -1);

    print(BinarySerach2(list, 8) == 0);
    print(BinarySerach2(list, 100) == -1);
  }

  public static int BinarySerach(int[] list, int value, int left, int right)
  {
    if(left > right)
      return -1;
    int mid = (left + right) / 2;
    if(list[mid] == value)
      return mid;
    if(list[mid] < value)
      return BinarySerach(list, value, mid+1, right);
    else
      return BinarySerach(list, value, left, mid-1);
  }

  public static int BinarySerach2(int[] list, int value)
  {
    int left = 0;
    int right = list.Length-1;
    int mid = (left + right) / 2;

    if(list[mid] == value)
      return mid;
    else
    {
      while(list[mid] < value)
      {
        left = mid+1;
        mid = (left + right) / 2;
        if(left > right)
          return -1;
      }
      while(list[mid] > value)
      {
        right = mid-1;
        mid = (left + right) / 2;
        if(left > right)
          return -1;
      }
      if(list[mid] == value)
        return mid;
    }
    return mid;
  }

  //HomeWork 재귀호출을 이용하지않고 만들어오기
}