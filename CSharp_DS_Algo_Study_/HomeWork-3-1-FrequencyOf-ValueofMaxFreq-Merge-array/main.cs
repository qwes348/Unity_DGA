using System;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    print("*FrequencyOf*");
    int[] arrA = new int[] {1, 1, 1, 1, 2, 1, 2, 3, 2, 3, 3};
    print(FrequencyOf(arrA, 1) == 5);
    print(FrequencyOf(arrA, 2) == 3);
    print(FrequencyOf(arrA, 3) == 3);
    print(FrequencyOf(arrA, 100) == 0);
    print("");

    print("*ValueOfMaxFreq*");
    print(ValueOfMaxFreq(arrA) == 1);
    int[] arrB = new int[] {1, 2, 2, 2, 2, 3, 3, 3};
    print(ValueOfMaxFreq(arrB) == 2);
    print("");

    print("*MergeArrays*");
    int [] arrC = new int[] {1, 3, 5, 7, 9};
    int [] arrD = new int[] {2, 4, 6, 8};
    print(Stringify(MergeArrays(arrC, arrD)) == "1 2 3 4 5 6 7 8 9");

    int[] arrE = new int[] {1, 2, 5, 7, 9};
    int[] arrF = new int[] {2, 4, 6, 8, 10, 11, 12};
    print(Stringify(MergeArrays(arrE, arrF)) == "1 2 2 4 5 6 7 8 9 10 11 12");

    print(Stringify(MergeArrays(new int[] {}, new int[] {})) == "");
    print(Stringify(MergeArrays(new int[] {}, new int[] {1, 5, 10})) == "1 5 10");
    print(Stringify(MergeArrays(new int[] {1,1,1}, new int[] {2,2,2,2})) == "1 1 1 2 2 2 2");
  }

  public static int FrequencyOf(int[] arr, int num)
  {
    int count = 0;
    for(int i=0; i<arr.Length; i++)
    {
      if(arr[i] == num)
        count++;
    }
    return count;
  }  

  public static int ValueOfMaxFreq(int[] arr)
  {
    int maxCount = 0;
    int maxValue = 0;

    for(int i=0; i<arr.Length; i++)
    {
      if(FrequencyOf(arr, arr[i]) >= maxCount)
      {
        maxCount = FrequencyOf(arr, arr[i]);
        maxValue = arr[i];
      }
    }
    return maxValue;
  }

  public static int[] MergeArrays(int[] arr1, int[] arr2)
  {
    int len = arr1.Length + arr2.Length;
    int[] mergedArr = new int[len];
    int countArr1 = 0; 
    int countArr2 = 0;

    for(int i=0; i < mergedArr.Length; i++)
    {
      if(countArr1 < arr1.Length && countArr2 < arr2.Length)
      {
        if(arr1[countArr1] < arr2[countArr2])
        {
          mergedArr[i] = arr1[countArr1];
          countArr1++;
        }
        else if(arr2[countArr2] < arr1[countArr1])
        {
          mergedArr[i] = arr2[countArr2];
          countArr2++;
        }
        else
        {
          mergedArr[i] = arr1[countArr1];
          countArr1++;
          i++;
          mergedArr[i] = arr2[countArr2]; // == mergedArr[i] == arr2[countArr2++];
          countArr2++;
        }
      }
      else if(countArr1 >= arr1.Length)
      {
        mergedArr[i] = arr2[countArr2];
        countArr2++;
      }
      else if(countArr2 >= arr2.Length)
      {
        mergedArr[i] = arr1[countArr1];
        countArr1++;
      }
    }

    return mergedArr;
  }

  public static string Stringify(int[] list)
  {
    return String.Join(" ", list);   // int형 배열을 문자열로 변환
  }

}



// FrequencyOf(array, num) array안에서 num의 갯수를 반환

// ValueOfMaxFreq(array) array안에서 빈도가 제일높은 num을 반환

// MergeArrays(array, array) 두 array를 합치는데 합치는과정에서 비교해서 정렬하며 담을것
// arrA = {1, 3, 5, 7, 9};
// arrB = {2, 4, 6, 8};
// arrC = {1, 2, 5, 7, 9};
// arrD = {2, 4, 6, 8, 10, 11, 12};
// 크기는 배열 둘의길이를 합친길이