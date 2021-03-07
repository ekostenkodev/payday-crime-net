using System.Collections.Generic;
using DG.Tweening;
using Kadoy.CrimeNet.Missions.Bubble;
using Kadoy.CrimeNet.Utils;

namespace Kadoy.CrimeNet.Controllers {
  public class MissionBubbleFadeController {
    private const float OutValue = 0.4f;
    private const float FadeDuration = 0.5f;
    
    private readonly IReadOnlyCollection<MissionBubbleBehaviour> activeMissions;
    private readonly Sequence sequence;
    private MissionBubbleBehaviour enteredMission;
    
    public MissionBubbleFadeController(IReadOnlyCollection<MissionBubbleBehaviour> activeMissions) {
      this.activeMissions = activeMissions;

      sequence = DOTween.Sequence();
    }

    public void Add(MissionBubbleBehaviour missionBubble) {
      missionBubble.PointEnter += OnMissionEnter;
      missionBubble.PointExit += OnMissionExit;
      
      if (enteredMission != null) {
        sequence.Join(Fade.DO(missionBubble.Root, OutValue, 0));
      }
    }
    
    public void Remove(MissionBubbleBehaviour missionBubble) {
      missionBubble.PointEnter -= OnMissionEnter;
      missionBubble.PointExit -= OnMissionExit;

      if (missionBubble == enteredMission) {
        OnMissionExit(null);
      }
    }

    private void OnMissionExit(MissionBubbleBehaviour missionBubble) {
      enteredMission = null;
      sequence?.Kill();

      foreach (var activeMission in activeMissions) {
        if (missionBubble == activeMission) {
          continue;
        }

        sequence.Join(Fade.DO(activeMission.Root, 1f, FadeDuration));
      }
    }

    private void OnMissionEnter(MissionBubbleBehaviour missionBubble) {
      enteredMission = missionBubble;
      sequence?.Kill();
      
      foreach (var activeMission in activeMissions) {
        var endValue = OutValue;
        
        if (missionBubble == activeMission) {
          endValue = 1;
        }
        
        sequence.Join(Fade.DO(activeMission.Root, endValue, FadeDuration));
      }
    }
  }
}