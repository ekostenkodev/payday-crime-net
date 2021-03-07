using DG.Tweening;
using UnityEngine;

namespace Kadoy.CrimeNet.Utils {
  public class ScaleTweenerBehaviour : TweenerBehaviour<Vector3> {
    protected void OnEnable() {
      transform.localScale = startValue;

      base.OnEnable();
    }

    public override void Animate() {
      transform.localScale = startValue;

      tweener?.Kill();
      tweener = transform
        .DOScale(endValue, duration)
        .SetLoops(isLoop ? int.MaxValue : 0, loopType)
        .SetDelay(delay)
        .SetEase(animationCurve);
    }

    public override void StopAnimation() {
      transform.localScale = startValue;

      base.StopAnimation();
    }
  }
}