using UnityEngine;

namespace Kadoy.CrimeNet.Util {
  public static class RichVectors {
    public static Vector3 Random(float min, float max) {
      var x = UnityEngine.Random.Range(min, max);
      var y = UnityEngine.Random.Range(min, max);
      var z = UnityEngine.Random.Range(min, max);
      
      return new Vector3(x, y, z);
    }

    public static Vector3 WithX(this Vector3 vector, float x) {
      vector.x = x;
      return vector;
    }

    public static Vector3 WithY(this Vector3 vector, float y) {
      vector.y = y;
      return vector;
    }

    public static Vector3 WithZ(this Vector3 vector, float z) {
      vector.z = z;
      return vector;
    }
    
    public static Vector3 PlusX(this Vector3 vector, float x) {
      vector.x += x;
      return vector;
    }

    public static Vector3 PlusY(this Vector3 vector, float y) {
      vector.y += y;
      return vector;
    }

    public static Vector3 PlusZ(this Vector3 vector, float z) {
      vector.z += z;
      return vector;
    }
  }
}