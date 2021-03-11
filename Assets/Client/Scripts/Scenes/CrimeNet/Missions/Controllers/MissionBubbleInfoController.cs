using System;
using System.Collections.Generic;
using Kadoy.CrimeNet.Missions.Bubble;
using Kadoy.CrimeNet.Missions.UI;
using Kadoy.CrimeNet.Utils;

namespace Kadoy.CrimeNet.Controllers {
  public class MissionBubbleInfoController: IDisposable {
    private readonly IReadOnlyCollection<MissionBubbleBehaviour> missions;
    private readonly MissionInformationBehaviour informationBehaviour;
    private readonly IMissionFabric fabricBehaviour;

    public MissionBubbleInfoController(IReadOnlyCollection<MissionBubbleBehaviour> missions,
      MissionInformationBehaviour informationBehaviour,
      IMissionFabric fabricBehaviour) {
      this.missions = missions;
      this.informationBehaviour = informationBehaviour;
      this.fabricBehaviour = fabricBehaviour;

      informationBehaviour.Close += OnInfoDisable;
    }

    public void Add(MissionBubbleBehaviour missionBubble) {
      missionBubble.PointDown += OnInfoEnable;
    }
    
    public void Remove(MissionBubbleBehaviour missionBubble) {
      missionBubble.PointDown -= OnInfoEnable;
    }

    private void OnInfoDisable() {
      GameObjects.NotActive(informationBehaviour);

      StartTimers();
      fabricBehaviour.Continue();
    }

    private void OnInfoEnable(MissionBubbleBehaviour mission) {
      GameObjects.Active(informationBehaviour);
      fabricBehaviour.Stop();

      StopTimers();
    }

    private void StartTimers() {
      foreach (var mission in missions) {
        mission.Timer.Start();
      }
    }

    private void StopTimers() {
      foreach (var mission in missions) {
        mission.Timer.Stop();
      }
    }
    
    public void Dispose() {
      informationBehaviour.Close -= OnInfoDisable;

      foreach (var mission in missions) {
        Remove(mission);
      }
    }
  }
}