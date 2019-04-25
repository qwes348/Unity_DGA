using System;
using System.Collections.Generic;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    int[] arr1 = new int[] {7, 9, 4, 10, 5, 1, 6, 3, 2, 8};
    Divide(arr1, 0 ,arr1.Length-1);
    print(arr1.Stringify() == "1 2 3 4 5 6 7 8 9 10");

    int[] arr2 = new int[] {4, 4, 4, 1, 1, 1, 3, 3, 3};
    Divide(arr2, 0 ,arr2.Length-1);
    print(arr2.Stringify() == "1 1 1 3 3 3 4 4 4");    

    int[] arr3 = new int[] {10, 9, 1, 5, 4, 6, 8, 1, 3, 3, 5, 4, 3, 6, 2, 7, 0};
    Divide(arr3, 0 ,arr3.Length-1);
    print(arr3.Stringify() == "0 1 1 2 3 3 3 4 4 5 5 6 6 7 8 9 10"); 

    int[] arr4 = new int[] {1};
    Divide(arr4, 0 ,arr4.Length-1);
    print(arr4.Stringify() == "1"); 

    int[] arr5 = new int[] {2, 1};
    Divide(arr5, 0 ,arr5.Length-1);
    print(arr5.Stringify() == "1 2");     
  }

  public static void mergeSort(int[] arr, int left, int mid, int right)
  {
    int Aleft = left; int Aright = mid;
    int Bleft = mid+1; int Bright = right;

    int[] sortedArr = new int[arr.Length];
    
    int j = left;

    while(Aleft<=Aright && Bleft<=Bright)
    {
      if(arr[Aleft] <= arr[Bleft])
        sortedArr[j++] = arr[Aleft++];
      else
        sortedArr[j++] = arr[Bleft++];
    }
    if(Aleft > Aright)
    {
      while(Bleft <= Bright)
      {
        sortedArr[j++] = arr[Bleft++];
      }
    }
    else if(Bleft > Bright)
    {
      while(Aleft <= Aright)
      {
        sortedArr[j++] = arr[Aleft++];      
      }
    }

    for(int i=left; i<=right; i++)
    {
      arr[i] = sortedArr[i];
    }
  }
  
  public static void Divide(int[] arr, int left, int right)
  {
    if(left < right)
    {
      int mid = (left+right)/2;
      Divide(arr, left, mid);     // 왼쪽 리스트
      Divide(arr, mid+1, right);  // 오른쪽 리스트
      mergeSort(arr, left, mid, right); // 정복
    }
  }
}


public static class ClassExtension
{
  public static string Stringify<T>(this IEnumerable<T> list) 
  {
    return String.Join(" ", list);
  }
}

// 숙제 1
// 합병정렬 merge sort
// 분할정복방법 
// https://gmlwjd9405.github.io/2018/05/08/algorithm-merge-sort.html

// 숙제 2
// linkedlist yield retrun 가능하게 만들어오기

// 숙제 3
// A* 알고리즘
// 목표지점을 만나면 종료
// 각 인덱스의 비용은 1
// 움직일때마다 비용을 기억하기
//http://theory.stanford.edu/~amitp/GameProgramming/AStarComparison.html