using UnityEngine;

namespace Kadoy.CrimeNet.Utils {
  public static class GameObjects {
    public static void NotActive(params GameObject[] objects) {
      foreach (var obj in objects) {
        if (obj) {
          obj.SetActive(false);
        }
      }
    }

    public static void Active(params GameObject[] objects) {
      foreach (var obj in objects) {
        if (obj) {
          obj.SetActive(true);
        }
      }
    }

    public static void NotActive(params Component[] objects) {
      foreach (var obj in objects) {
        obj.gameObject.SetActive(false);
      }
    }

    public static void Active(params Component[] objects) {
      foreach (var obj in objects) {
        obj.gameObject.SetActive(true);
      }
    }
  }
}