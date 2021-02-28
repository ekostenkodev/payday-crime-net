using Kadoy.CrimeNet.Models.Missions;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Kadoy.CrimeNet.Missions {
  public class MissionBehaviour : MonoBehaviour {
    [SerializeField]
    private Transform root;
    
    [SerializeField]
    private Collider2D interactableZone;
    
    [Space]
    [SerializeField]
    private MissionInformationBehaviour informationBehaviour;

    [SerializeField]
    private MissionTimerBehaviour timerBehaviour;

    private void OnEnable() {
      timerBehaviour.OnComplete += OnTimerComplete;
    }

    private void OnDisable() {
      timerBehaviour.OnComplete -= OnTimerComplete;
    }

    public void Initialize(InnerMissionInfo missionInfo, Vector2 position) {
      root.position = position;

      informationBehaviour.Initialize(missionInfo);
      timerBehaviour.Initialize(missionInfo.Duration);
      
      interactableZone.OnMouseExitAsObservable().Subscribe(OnPointExit).AddTo(this);
      interactableZone.OnMouseEnterAsObservable().Subscribe(OnPointEnter).AddTo(this);
      interactableZone.OnMouseDownAsObservable().Subscribe(OnPointDown).AddTo(this);
    }

    private void OnTimerComplete() {
      
    }

    private void OnPointEnter(Unit unit) {
      informationBehaviour.ShowDetailedInfo();
    }

    private void OnPointExit(Unit unit) {
      informationBehaviour.HideDetailedInfo();
    }

    private void OnPointDown(Unit unit) {
      
    }
  }
}