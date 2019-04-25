using System;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    print(Vector3.forward.magnitude == 1f);
    // v.magnitude = 0; // error!
    print((-Vector3.one).ToString() == "-1, -1, -1");

    print((Vector3.right + Vector3.up).ToString() == "1, 1, 0");

    Vector3 a = new Vector3(2f, 2f, 0f);
    Vector3 b = new Vector3(2f, 0f, 0f);
    print(Vector3.Distance(a,b) == 2f);

    print((a + b).ToString() == "4, 2, 0"); // print(operator(a, b).ToString());
    print((2.0f * b).ToString() == "4, 0, 0");
    print((2.0f * a).ToString() == "4, 4, 0");
    print((b * 2.0f).ToString() == "4, 0, 0");
    print((a * 2.0f).ToString() == "4, 4, 0");

    print(b.normalised.ToString() == "1, 0, 0");
    print((3f*b).normalised.ToString() == "1, 0, 0");
    
    print(Vector3.Angle(a,b).ToString() == "45");
    print(Vector3.Angle(new Vector3(5f, 0f, 0f), new Vector3(10f, 2f, 5f)).ToString() == "28.3031961003882");
    print(Vector3.Angle(new Vector3(0f, 0f, 1f), new Vector3(1f, 1f, 0f)).ToString() == "90");
    print(Vector3.Angle(new Vector3(0f, 0f, 1f), new Vector3(0f, 1f, 0f)).ToString() == "90");
    print(Vector3.Angle(Vector3.right, Vector3.left).ToString() == "180");

    print(Vector3.Dot(Vector3.right, Vector3.right) == 1);
    print(Vector3.Dot(Vector3.right, Vector3.up) == 0);
    print(Vector3.Dot(Vector3.right, Vector3.left) == -1);
    
    //http://james-ramsden.com/angle-between-two-vectors/
    //return acos(Vector.dot(vec1.normalised(), vec2.normalised()));
    //https://egohim.blog.me/70000495252
    
  }

  public class Mathf
  {
    public static double Rad2Deg = 360 / (2*Math.PI);
    public static double Deg2Rad = (2*Math.PI*d) / 360;
  }

  public class Vector3
  {
    // 상수를 재귀적으로 생성(읽기전용이라서)
    public static readonly Vector3 zero = new Vector3(0f, 0f, 0f);
    public static readonly Vector3 one = new Vector3(1f, 1f, 1f); 
    public static readonly Vector3 forward = new Vector3(0f, 0f, 1f); 
    public static readonly Vector3 back = -forward;
    public static readonly Vector3 right = new Vector3(1f, 0f, 0f); 
    public static readonly Vector3 left = -right;
    public static readonly Vector3 up = new Vector3(0f, 1f, 0f); 
    public static readonly Vector3 down = -up;

    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    
    public override string ToString() 
    {
      return x + ", " + y + ", " + z; 
      // return $"{x},{y},{z}";
    }

    public Vector3(float _x, float _y, float _z)
    {
      x = _x; y = _y; z = _z;
    }

    public float magnitude
    {
      get { return Math.Abs((float)Math.Sqrt(x*x + y*y + z*z ));} // Math.Abs(절댓값)
    }

    public Vector3 normalised // 정규화, 노멀라이즈
    {
      get { return this * (1 / this.magnitude); }
    }

    public static float Distance(Vector3 a, Vector3 b)
    {
      return (a-b).magnitude;  //magnitude 길이구하기
    }

    public static double Angle(Vector3 a, Vector3 b)
    {
      double[] doubleA = new double[] {a.x, a.y, a.z};
      double[] doubleB = new double[] {b.x, b.y, b.z};
      double lengthA;
      double lengthB;
      double dot;
      double angle;

      // normalise method
      lengthA = Math.Sqrt((doubleA[0] * doubleA[0]) + (doubleA[1] * doubleA[1]) + (doubleA[2] * doubleA[2]));
      lengthB = Math.Sqrt((doubleB[0] * doubleB[0]) + (doubleB[1] * doubleB[1]) + (doubleB[2] * doubleB[2]));

      doubleA[0] /= lengthA; doubleA[1] /= lengthA; doubleA[2] /= lengthA;
      doubleB[0] /= lengthB; doubleB[1] /= lengthB; doubleB[2] /= lengthB;

      // dot method
      dot = doubleA[0]*doubleB[0] + doubleA[1]*doubleB[1] + doubleA[2]*doubleB[2];
      
      // Radian to degree
      angle = Math.Acos(dot);
      angle *= 360.0 / (2 * Math.PI);

      return angle;

    }

    public static Vector3 operator-(Vector3 a, Vector3 b)
    {
      return(a + -b);
    }

    public static Vector3 operator+(Vector3 a, Vector3 b)
    {
      return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z); // 각원소끼리 더해줌
    }

    public static Vector3 operator-(Vector3 a)
    {
      return new Vector3(-a.x, -a.y, -a.z);
    }

    public static Vector3 operator*(float d, Vector3 a)
    {
      return new Vector3(a.x*d, a.y*d, a.z*d);
    }

    public static Vector3 operator*(Vector3 a, float d)
    {
      return d*a;
    }

    public static float Dot(Vector3 a, Vector3 b)
    {
      return a.x*b.x + a.y*b.y + a.z+b.z;
    }

    // operator* 구현해오기 public static Vector3 operator*(float d, Vector3 a);
    // 할수있으면 Angle까지
  }
}