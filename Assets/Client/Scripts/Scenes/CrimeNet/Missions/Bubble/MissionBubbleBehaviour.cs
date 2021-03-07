using System;
using DG.Tweening;
using Kadoy.CrimeNet.Models.Missions;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Kadoy.CrimeNet.Missions.Bubble {
  public class MissionBubbleBehaviour : MonoBehaviour {
    public class MissionArgs {
      public InnerMissionInfo MissionInfo { get; }
      public Vector2 Position { get; }
      public Action OnComplete { get; }

      public MissionArgs(InnerMissionInfo missionInfo, Vector2 position, Action onComplete) {
        MissionInfo = missionInfo;
        Position = position;
        OnComplete = onComplete;
      }
    }
    
    [SerializeField]
    private Collider2D interactableZone;

    [Space]
    [SerializeField]
    private MissionBubbleInformationBehaviour informationBehaviour;

    [SerializeField]
    private MissionBubbleTimerBehaviour timerBehaviour;

    private Action onComplete;
    private Transform selfTransform;
    private readonly CompositeDisposable disposables = new CompositeDisposable();
    
    public event Action<MissionBubbleBehaviour> PointEnter;
    public event Action<MissionBubbleBehaviour> PointExit;
    public event Action<MissionBubbleBehaviour> PointDown;
    public event Action<MissionBubbleBehaviour> Complete;

    public Transform Root => selfTransform ?? (selfTransform = transform); 
    public IMissionTimer Timer => timerBehaviour; 

    private void OnEnable() {
      timerBehaviour.OnComplete += OnTimerComplete;
    }

    private void OnDisable() {
      disposables.Clear();

      timerBehaviour.OnComplete -= OnTimerComplete;
    }

    public void Initialize(MissionArgs args) {
      onComplete = args.OnComplete;
      
      Root.position = args.Position;
      Root.localScale = Vector3.one;
      Root.name = args.MissionInfo.Id;

      informationBehaviour.Initialize(args.MissionInfo);
      timerBehaviour.Reset(args.MissionInfo.Duration);

      disposables.Clear();
      
      interactableZone.OnMouseExitAsObservable().Subscribe(OnPointExit).AddTo(disposables);
      interactableZone.OnMouseEnterAsObservable().Subscribe(OnPointEnter).AddTo(disposables);
      interactableZone.OnMouseDownAsObservable().Subscribe(OnPointDown).AddTo(disposables);
    }

    private void OnComplete() {
      Root
        .DOScaleX(0, 0.5f)
        .OnComplete(() => {
          Complete?.Invoke(this);
          
          onComplete?.Invoke();
        });
    }

    private void OnTimerComplete() {
      disposables.Clear();

      informationBehaviour.Close().OnComplete(OnComplete);
    }

    private void OnPointEnter(Unit unit) {
      informationBehaviour.ShowDetailedInfo();
      
      PointEnter?.Invoke(this);
    }

    private void OnPointExit(Unit unit) {
      informationBehaviour.HideDetailedInfo();
      
      PointExit?.Invoke(this);
    }

    private void OnPointDown(Unit unit) {
      PointDown?.Invoke(this);
    }
  }
}