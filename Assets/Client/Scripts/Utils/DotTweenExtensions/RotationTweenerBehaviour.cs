using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace Kadoy.CrimeNet.Utils {
  public class RotationTweenerBehaviour : TweenerBehaviour<Vector3> {
    private bool isRotate;

    [SerializeField]
    private bool isRotationAround;

    [UsedImplicitly]
    protected void OnEnable() {
      transform.localRotation = Quaternion.identity;

      base.OnEnable();
    }

    [UsedImplicitly]
    private void Update() {
      if (!isRotate) {
        return;
      }

      var euler = transform.rotation.eulerAngles;

      transform.rotation = Quaternion.Euler(euler + endValue * Time.deltaTime);
    }

    public override void Animate() {
      isRotate = isRotationAround;

      if (!isRotationAround) {
        transform.localRotation = Quaternion.Euler(startValue);
        tweener = transform
          .DORotate(endValue, duration)
          .SetLoops(isLoop ? int.MaxValue : 0, loopType)
          .SetDelay(delay)
          .SetEase(animationCurve);
      }
    }

    public override void StopAnimation() {
      transform.localRotation = Quaternion.identity;

      base.StopAnimation();
    }
  }
}