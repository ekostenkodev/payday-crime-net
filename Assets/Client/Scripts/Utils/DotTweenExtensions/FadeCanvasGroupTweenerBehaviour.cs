using DG.Tweening;
using UnityEngine;

namespace Kadoy.CrimeNet.Utils {
  [RequireComponent(typeof(CanvasGroup))]
  public class FadeCanvasGroupTweenerBehaviour : TweenerBehaviour<float> {
    private CanvasGroup canvasGroup;

    private void Awake() {
      canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void Animate() {
      if (canvasGroup == null) {
        return;
      }

      canvasGroup.alpha = startValue;
      tweener = canvasGroup
        .DOFade(endValue, duration)
        .SetEase(animationCurve)
        .SetDelay(delay)
        .SetLoops(isLoop ? -1 : 0, loopType);
    }

    public override void StopAnimation() {
      if (canvasGroup == null) {
        return;
      }

      canvasGroup.alpha = startValue;

      base.StopAnimation();
    }
  }
}