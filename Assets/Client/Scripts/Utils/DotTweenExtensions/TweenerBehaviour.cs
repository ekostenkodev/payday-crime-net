using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace Kadoy.CrimeNet.Utils {
  public abstract class TweenerBehaviour<T> : MonoBehaviour {
    [SerializeField]
    protected T startValue;
    
    [SerializeField]
    protected T endValue;
    
    [Space]
    [SerializeField]
    protected float duration;
    
    [SerializeField]
    protected float delay;
    
    [SerializeField]
    protected AnimationCurve animationCurve;

    [Space]
    [SerializeField]
    protected bool onEnable;
    
    [Space]
    [SerializeField]
    protected bool isLoop;

    [SerializeField]
    protected LoopType loopType;
    
    protected Tweener tweener;

    [UsedImplicitly]
    protected void OnEnable() {
      if (onEnable) {
        Animate();
      }
    }

    [UsedImplicitly]
    private void OnDisable() {
      StopAnimation();
    }

    [UsedImplicitly]
    private void OnDestroy() {
      StopAnimation();
    }
    
    public virtual void Animate(){}
    
    public virtual void StopAnimation() {
      if (null == tweener) {
        return;
      }

      tweener.Kill();
      tweener = null;
    }
  }
}
