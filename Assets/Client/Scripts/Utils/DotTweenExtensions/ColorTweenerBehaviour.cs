using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Kadoy.CrimeNet.Utils {
  public class ColorTweenerBehaviour : TweenerBehaviour<Color> {
    private Graphic graphic;
    private Renderer renderer;

    private void Awake() {
      graphic = GetComponent<Graphic>();
      renderer = GetComponent<Renderer>();
    }

    public override void Animate() {
      if (graphic != null) {
        graphic.color = startValue;
        tweener = graphic
          .DOColor(endValue, duration)
          .SetEase(animationCurve)
          .SetLoops(isLoop ? int.MaxValue : 0, loopType)
          .SetDelay(delay);
      } else if (renderer != null) {
        renderer.material.color = startValue;
        tweener = renderer.material
          .DOColor(endValue, duration)
          .SetEase(animationCurve)
          .SetLoops(isLoop ? int.MaxValue : 0, loopType)
          .SetDelay(delay);
      }
    }

    public override void StopAnimation() {
      if (graphic != null) {
        graphic.color = startValue;
      } else if (renderer != null) {
        renderer.material.color = startValue;
      }

      base.StopAnimation();
    }
  }
}