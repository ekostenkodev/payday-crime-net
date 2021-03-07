using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kadoy.CrimeNet.Models.Missions;
using Kadoy.CrimeNet.Pools;
using Newtonsoft.Json;
using UniRx;
using UniRx.Toolkit;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Kadoy.CrimeNet.Missions.Bubble {
  public class MissionBubbleFabricBehaviour : MonoBehaviour {
    private const string MissionPath = "CrimeNet/Missions";
    private const float NeighborRadius = 0.5f;

    private static readonly (float min, float max) DelayRange = (2f, 5f);

    [SerializeField]
    private Transform root;

    [SerializeField]
    private MissionBubbleBehaviour missionBubblePrefab;

    [Header("BOUNDS")]
    [SerializeField]
    private Collider2D screenBounds;

    [SerializeField]
    private Collider2D mapBounds;

    private ObjectPool<MissionBubbleBehaviour> missionPool;
    private ISubject<MissionBubbleBehaviour> missionSubject;
    private CancellationTokenSource cancellationTokenSource;

    private void OnDisable() {
      cancellationTokenSource?.Cancel();
    }

    public IObservable<MissionBubbleBehaviour> Execute() {
      var missionAsset = Resources.Load<TextAsset>(MissionPath);
      var missionBase = JsonConvert.DeserializeObject<InnerMissionBase>(missionAsset.text);
      
      missionSubject = new ReplaySubject<MissionBubbleBehaviour>();
      missionPool = new EntityPool<MissionBubbleBehaviour>(missionBubblePrefab, root);

      StartFabric(missionBase.Missions);

      return missionSubject;
    }

    private async void StartFabric(InnerMissionInfo[] missions) {
      cancellationTokenSource = new CancellationTokenSource();

      foreach (var missionInfo in missions) {
        var mission = CreateMission(missionInfo);
        var delay = Random.Range(DelayRange.min, DelayRange.max);
        var delaySpan = TimeSpan.FromSeconds(delay);

        missionSubject?.OnNext(mission);

        await Task.Delay(delaySpan, cancellationTokenSource.Token);
      }
    }

    private MissionBubbleBehaviour CreateMission(InnerMissionInfo missionInfo) {
      var mission = missionPool.Rent();
      var position = GetRandomMapPosition();
      var completeCallback = new Action(() => missionPool.Return(mission));

      var args = new MissionBubbleBehaviour.MissionArgs(missionInfo, position, completeCallback);

      mission.Initialize(args);

      return mission;
    }

    private Vector2 GetRandomMapPosition() {
      var bounds = screenBounds.bounds;
      var width = bounds.size.x;
      var height = bounds.size.y;
      var screenOffset = new Vector2(width, height) * 0.5f;
      var isComplete = false;
      
      Vector2 mapPosition;

      do {
        var randomPosition = new Vector2(Random.Range(0f, width), Random.Range(0f, height));
        
        mapPosition = randomPosition - screenOffset;

        var isOverlap = mapBounds.OverlapPoint(mapPosition);
        var isNeighborFree = Physics2D
          .OverlapCircleAll(mapPosition, NeighborRadius)
          .Select(x => x.GetComponent<MissionBubbleBehaviour>())
          .All(x => x == null);

        if (isOverlap && isNeighborFree) {
          isComplete = true;
        }
      } while (!isComplete);

      return mapPosition;
    }
  }
}