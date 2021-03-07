using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Kadoy.CrimeNet.Utils {
  public class ColorTweenerBehaviour : TweenerBehaviour<Color> {
    private SpriteRenderer spriteRenderer;
    private Image image;
    private RawImage rawImage;

    private void Awake() {
      spriteRenderer = GetComponent<SpriteRenderer>();
      image = GetComponent<Image>();
      rawImage = GetComponent<RawImage>();
    }

    public override void Animate() {
      if (spriteRenderer != null) {
        spriteRenderer.color = startValue;
        tweener = spriteRenderer
          .DOColor(endValue, duration)
          .SetEase(animationCurve)
          .SetLoops(isLoop ? int.MaxValue : 0, loopType)
          .SetDelay(delay);
      } else if (image != null) {
        image.color = startValue;
        tweener = image
          .DOColor(endValue, duration)
          .SetEase(animationCurve)
          .SetLoops(isLoop ? int.MaxValue : 0, loopType)
          .SetDelay(delay);
      }else if (rawImage != null) {
        rawImage.color = startValue;
        tweener = rawImage
          .DOColor(endValue, duration)
          .SetEase(animationCurve)
          .SetLoops(isLoop ? int.MaxValue : 0, loopType)
          .SetDelay(delay);
      }
    }

    public override void StopAnimation() {
      if (spriteRenderer != null) {
        spriteRenderer.color = startValue;
      } else if (image != null) {
        image.color = startValue;
      }else if (rawImage != null) {
        rawImage.color = startValue;
      }
      
      base.StopAnimation();
    }
  }
}