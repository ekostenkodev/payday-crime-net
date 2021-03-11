using System;
using DG.Tweening;
using Kadoy.CrimeNet.Models.Missions;
using Kadoy.CrimeNet.Utils;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

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
    private PointHandlerBehaviour interactableZone;

    [Space]
    [SerializeField]
    private MissionBubbleInformationBehaviour informationBehaviour;

    [SerializeField]
    private MissionBubbleTimerBehaviour timerBehaviour;

    private Action onComplete;
    private Transform selfTransform;

    public event Action<MissionBubbleBehaviour> PointEnter;
    public event Action<MissionBubbleBehaviour> PointExit;
    public event Action<MissionBubbleBehaviour> PointDown;
    public event Action<MissionBubbleBehaviour> Complete;

    public Transform Root => selfTransform ?? (selfTransform = transform);
    public IMissionTimer Timer => timerBehaviour;

    private void OnEnable() {
      timerBehaviour.OnComplete += OnTimerComplete;

      interactableZone.Click += OnPointClick;
      interactableZone.Enter += OnPointEnter;
      interactableZone.Exit += OnPointExit;
    }

    private void OnDisable() {
      timerBehaviour.OnComplete -= OnTimerComplete;
      
      interactableZone.Click -= OnPointClick;
      interactableZone.Enter -= OnPointEnter;
      interactableZone.Exit -= OnPointExit;
    }

    public void Initialize(MissionArgs args) {
      onComplete = args.OnComplete;

      Root.position = args.Position;
      Root.localScale = Vector3.one;
      Root.name = args.MissionInfo.Id;

      informationBehaviour.Initialize(args.MissionInfo);
      timerBehaviour.Reset(args.MissionInfo.Duration);
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
      informationBehaviour.Close().OnComplete(OnComplete);
    }

    private void OnPointEnter(PointerEventData eventData) {
      informationBehaviour.ShowDetailedInfo();

      PointEnter?.Invoke(this);
    }

    private void OnPointExit(PointerEventData eventData) {
      informationBehaviour.HideDetailedInfo();

      PointExit?.Invoke(this);
    }

    private void OnPointClick(PointerEventData eventData) {
      PointDown?.Invoke(this);
    }
  }
}