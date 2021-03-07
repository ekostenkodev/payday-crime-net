using System.Collections.Generic;
using Kadoy.CrimeNet.Controllers;
using Kadoy.CrimeNet.Missions.Bubble;
using UniRx;
using UnityEngine;

namespace Kadoy.CrimeNet {
  public class CrimeNet : MonoBehaviour {
    [SerializeField]
    private MissionBubbleFabricBehaviour missionBubbleFabric;
    
    private readonly List<MissionBubbleBehaviour> activeMissions = new List<MissionBubbleBehaviour>();

    private MissionBubbleFadeController fadeController;

    private void OnDisable() {
      RemoveMissionRange(activeMissions.ToArray());
    }

    private void Awake() {
      fadeController = new MissionBubbleFadeController(activeMissions);
      
      missionBubbleFabric
        .Execute()
        .Subscribe(AddMission)
        .AddTo(this);
    }

    private void AddMission(MissionBubbleBehaviour missionBubble) {
      activeMissions.Add(missionBubble);
      fadeController.Add(missionBubble);
          
      missionBubble.Complete += OnMissionComplete;
    }
    
    private void RemoveMissionRange(params MissionBubbleBehaviour[] missions) {
      foreach (var mission in missions) {
        activeMissions.Remove(mission);
        fadeController.Remove(mission);

        mission.Complete -= OnMissionComplete;
      }
    }
    
    private void OnMissionComplete(MissionBubbleBehaviour missionBubble) {
      RemoveMissionRange(missionBubble);
    }
  }
}