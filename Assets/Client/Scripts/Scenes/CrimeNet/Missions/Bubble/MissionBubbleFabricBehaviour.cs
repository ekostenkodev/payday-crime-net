using System;
using System.Collections.Generic;
using System.Linq;
using Kadoy.CrimeNet.Models.Missions;
using Kadoy.CrimeNet.Pools;
using Newtonsoft.Json;
using UniRx;
using UniRx.Toolkit;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Kadoy.CrimeNet.Missions.Bubble {
  public interface IMissionFabric {
    void Continue();
    void Stop();
  }

  public class MissionBubbleFabricBehaviour : MonoBehaviour, IMissionFabric {
    private const string MissionPath = "CrimeNet/Missions";

    private static readonly (float min, float max) DelayRange = (2f, 5f);

    [SerializeField]
    private Transform root;

    [SerializeField]
    private MissionBubbleBehaviour missionBubblePrefab;

    [Space]
    [SerializeField]
    private CrimeNetMapBehaviour mapBehaviour;

    private ObjectPool<MissionBubbleBehaviour> missionPool;
    private ISubject<MissionBubbleBehaviour> missionSubject;
    private CompositeDisposable disposable;
    private List<InnerMissionInfo> missionInfos;

    private void OnDisable() {
      disposable?.Clear();
    }

    public IObservable<MissionBubbleBehaviour> Execute() {
      var missionAsset = Resources.Load<TextAsset>(MissionPath);
      var missionBase = JsonConvert.DeserializeObject<InnerMissionBase>(missionAsset.text);

      missionSubject = new ReplaySubject<MissionBubbleBehaviour>();
      missionPool = new EntityPool<MissionBubbleBehaviour>(missionBubblePrefab, root);
      disposable = new CompositeDisposable();
      missionInfos = missionBase.Missions.ToList();

      StartFabric();

      return missionSubject;
    }

    public void Continue() {
      StartFabric();
    }

    public void Stop() {
      disposable?.Clear();
    }

    private void StartFabric() {
      var delay = Random.Range(DelayRange.min, DelayRange.max);
      var delaySpan = TimeSpan.FromSeconds(delay);

      Observable
        .Timer(delaySpan)
        .Subscribe(_ => {
          var missionInfo = GetRandomInfo();
          var mission = CreateMission(missionInfo);

          missionSubject?.OnNext(mission);
          disposable?.Clear();

          StartFabric();
        }).AddTo(disposable);
    }

    private InnerMissionInfo GetRandomInfo() {
      var index = Random.Range(0, missionInfos.Count);
      var mission = missionInfos[index];

      missionInfos.RemoveAt(index);
      
      return mission;
    }

    private MissionBubbleBehaviour CreateMission(InnerMissionInfo missionInfo) {
      var mission = missionPool.Rent();
      var position = mapBehaviour.GetRandomBubblePosition();
      var completeCallback = new Action(() => {
        missionPool.Return(mission);
        missionInfos.Add(missionInfo);
      });

      var args = new MissionBubbleBehaviour.MissionArgs(missionInfo, position, completeCallback);

      mission.Initialize(args);

      return mission;
    }
  }
}