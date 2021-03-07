using DG.Tweening;
using UnityEngine;

namespace Kadoy.CrimeNet.Utils {
  public class Fade {
    public static Tweener DO(Component component, float endValue, float duration) {
      if (null == component) {
        return null;
      }

      if (component.TryGetComponent<CanvasGroup>(out var canvasGroup)) {
        return DO(canvasGroup, endValue, duration);
      }

      GameObjects.Active(component);

      return null;
    }
    
    public static Tweener DO(CanvasGroup group, float endValue, float duration) {
      GameObjects.Active(group);

      var tweener = group.DOFade(endValue, duration);

      if (endValue <= 0.01) {
        tweener.OnComplete(() => GameObjects.NotActive(group));
      }
      
      return tweener;
    }
  }
}