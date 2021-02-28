using System;
using UnityEngine;
using UnityEngine.UI;

namespace Kadoy.CrimeNet.Missions {
  public class MissionTimerBehaviour : MonoBehaviour {
    [SerializeField]
    private Image timerImage;

    public event Action OnComplete;

    private bool isInitialized;
    private float leftTime;
    private float totalTime;

    private void Update() {
      if (!isInitialized) {
        return;
      }
      
      timerImage.fillAmount = leftTime / totalTime;
      leftTime -= Time.deltaTime;

      if (leftTime <= 0) {
        timerImage.fillAmount = 0;
        isInitialized = false;
          
        OnComplete?.Invoke();
      }
    }

    public void Initialize(float duration) {
      totalTime = leftTime = duration;
      isInitialized = true;
    }
  }
}