using UnityEngine;

namespace Kadoy.CrimeNet.Utils {
  public static class RichTransform {
    public static void ActivateChildren(this Transform transform, int count) {
      var activeCount = 0;
      foreach (Transform child in transform) {
        child.gameObject.SetActive(count > activeCount);
        
        activeCount++;
      }
    }
  }
}