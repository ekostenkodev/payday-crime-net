using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace Kadoy.CrimeNet.Utils {
  public class PositionTweenerBehaviour : TweenerBehaviour<Vector3> {
    [UsedImplicitly]
    private void OnEnable() {
      if (transform is RectTransform rectTransform) {
        rectTransform.anchoredPosition = startValue;
      } else {
        transform.localPosition = startValue;
      }

      base.OnEnable();
    }

    public override void Animate() {
      if (transform is RectTransform rectTransform) {
        rectTransform.anchoredPosition = startValue;
        tweener = rectTransform.DOAnchorPos(endValue, duration);
      } else {
        transform.localPosition = startValue;
        tweener = transform.DOLocalMove(endValue, duration);
      }

      tweener
        .SetEase(animationCurve)
        .SetLoops(isLoop ? -1 : 0, loopType)
        .SetDelay(delay);
    }

    public override void StopAnimation() {
      if (transform is RectTransform rectTransform) {
        rectTransform.anchoredPosition = startValue;
      } else {
        transform.localPosition = startValue;
      }
      
      base.StopAnimation();
    }
  }
}