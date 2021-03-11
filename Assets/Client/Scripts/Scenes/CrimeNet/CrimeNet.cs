using System.Collections.Generic;
using Kadoy.CrimeNet.Controllers;
using Kadoy.CrimeNet.Missions.Bubble;
using Kadoy.CrimeNet.Missions.UI;
using UniRx;
using UnityEngine;

namespace Kadoy.CrimeNet {
  public class CrimeNet : MonoBehaviour {
    [SerializeField]
    private MissionBubbleFabricBehaviour missionBubbleFabric;

    [SerializeField]
    private MissionInformationBehaviour informationBehaviour;
    
    private readonly List<MissionBubbleBehaviour> activeMissions = new List<MissionBubbleBehaviour>();

    private MissionBubbleFadeController fadeController;
    private MissionBubbleInfoController infoController;

    private void OnDisable() {
      RemoveMissionRange(activeMissions.ToArray());
      
      fadeController.Dispose();
      infoController.Dispose();
    }

    private void Awake() {
      fadeController = new MissionBubbleFadeController(activeMissions);
      infoController = new MissionBubbleInfoController(activeMissions, informationBehaviour, missionBubbleFabric);
      
      missionBubbleFabric
        .Execute()
        .Subscribe(AddMission)
        .AddTo(this);
    }

    private void AddMission(MissionBubbleBehaviour mission) {
      activeMissions.Add(mission);
      fadeController.Add(mission);
      infoController.Add(mission);
          
      mission.Complete += OnMissionComplete;
    }
    
    private void RemoveMissionRange(params MissionBubbleBehaviour[] missions) {
      foreach (var mission in missions) {
        fadeController.Remove(mission);
        infoController.Remove(mission);
        activeMissions.Remove(mission);

        mission.Complete -= OnMissionComplete;
      }
    }
    
    private void OnMissionComplete(MissionBubbleBehaviour mission) {
      RemoveMissionRange(mission);
    }
  }
}