using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyVector3 : MonoBehaviour {

    //Static Properties
    public static MyVector3 back = new MyVector3(0, 0, -1);
    public static MyVector3 forward = new MyVector3(0, 0, 1);
    public static MyVector3 down = new MyVector3(0, -1, 0);
    public static MyVector3 up = new MyVector3(0, 1, 0);
    public static MyVector3 left = new MyVector3(-1, 0, 0);
    public static MyVector3 right = new MyVector3(1, 0, 0);
    public static MyVector3 zero = new MyVector3(0, 0, 0);
    public static MyVector3 one = new MyVector3(1, 1, 1);

    //Properties
    public float x, y, z;
    //public MyVector3 normalized;
    public float magnitude;
    public float sqrMagnitude;

    //Constructors
    public MyVector3() {
        x = 1;
        y = 1;
        z = 1;
        magnitude = Mathf.Sqrt(x * x + y * y + z * z);
        sqrMagnitude = x * x + y * y + z * z;
        //normalized = new MyVector3(x / magnitude, y / magnitude, z / magnitude);
    }
    public MyVector3(float X, float Y) {
        x = X;
        y = Y;
        z = 0;
        magnitude = Mathf.Sqrt(x * x + y * y + z * z);
        sqrMagnitude = x * x + y * y + z * z;
        //normalized = new MyVector3(x / magnitude, y / magnitude, z / magnitude);
    }
    public MyVector3(float X, float Y, float Z) {
        x = X;
        y = Y;
        z = Z;
        magnitude = Mathf.Sqrt(x * x + y * y + z * z);
        sqrMagnitude = x * x + y * y + z * z;
        //normalized = new MyVector3(x / magnitude, y / magnitude, z / magnitude);
    }
    public MyVector3(Vector3 vec) {
        x = vec.x;
        y = vec.y;
        z = vec.z;
        magnitude = Mathf.Sqrt(x * x + y * y + z * z);
        sqrMagnitude = x * x + y * y + z * z;
        //normalized = new MyVector3(x / magnitude, y / magnitude, z / magnitude);
    }
    public MyVector3(MyVector3 vec) {
        x = vec.x;
        y = vec.y;
        z = vec.z;
        magnitude = Mathf.Sqrt(x * x + y * y + z * z);
        sqrMagnitude = x * x + y * y + z * z;
        //normalized = new MyVector3(x / magnitude, y / magnitude, z / magnitude);
    }

    //Methods
    public void Set(float X, float Y, float Z) {
        x = X;
        y = Y;
        z = Z;
    }
    public void Normalize() {
        float num = magnitude;
        if (num > 1E-05f){
            x /= num;
            y /= num;
            z /= num;
        }
        else {
            x = 0;
            y = 0;
            z = 0;
        }
    }

    //Static Methods
    public static float Angle(MyVector3 from, MyVector3 to) {
        //return Mathf.Acos(Dot(from,to) / (from.magnitude * to.magnitude));
        return Mathf.Acos(Mathf.Clamp(Dot(from, to) / (from.magnitude * to.magnitude), -1f, 1f)) * 57.29578f;
    }
    public static MyVector3 Cross(MyVector3 lhs, MyVector3 rhs) {
        return new MyVector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
    }
    public static float Distance(MyVector3 lhs, MyVector3 rhs) {
        return (lhs - rhs).magnitude;
    }
    public static float Distance(Vector3 lhs, Vector3 rhs) {
        return (lhs - rhs).magnitude;
    }
    public static float Dot(MyVector3 lhs, MyVector3 rhs) {
        return lhs.x * rhs.x + lhs.y* rhs.y + lhs.z* rhs.z;
    }
    public static MyVector3 Normalize(MyVector3 value) {
        float mag = Mathf.Sqrt((value.x * value.x) + (value.y * value.y) + (value.z * value.z));
        //return new MyVector3(value.x / mag, value.y / mag, value.z / mag);
        if (mag > 1E-05f) return new MyVector3(value.x / mag, value.y / mag, value.z / mag);
        else return zero;
    }
    public static MyVector3 Scale(MyVector3 lhs, MyVector3 rhs) {
        return new MyVector3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
    }

    //Operators
    public static bool operator ==(MyVector3 lhs, MyVector3 rhs) {
        return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
    }
    public static bool operator !=(MyVector3 lhs, MyVector3 rhs) {
        return !(lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z);
    }
    public static MyVector3 operator -(MyVector3 lhs, MyVector3 rhs) {
        return new MyVector3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
    }
    public static MyVector3 operator +(MyVector3 lhs, MyVector3 rhs) {
        return new MyVector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
    }
    public static MyVector3 operator *(MyVector3 lhs, float rhs) {
        return new MyVector3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
    }
    public static MyVector3 operator /(MyVector3 lhs, float rhs) {
        return new MyVector3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
    }
}