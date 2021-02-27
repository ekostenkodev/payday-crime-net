using Kadoy.CrimeNet.Models.Missions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kadoy.CrimeNet.Missions {
  public class MissionBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    [SerializeField]
    private MissionInformationBehaviour informationBehaviour;

    //[SerializeField]
    //private MissionTimerBehaviour timerBehaviour;

    public void Initialize(InnerMissionInfo missionInfo) {
      informationBehaviour.Initialize(missionInfo);
    }
    
    public void OnPointerEnter(PointerEventData eventData) {
      Debug.LogWarning("ENTER");
      informationBehaviour.ShowDetailedInfo();
    }

    public void OnPointerExit(PointerEventData eventData) {
      informationBehaviour.HideDetailedInfo();
    }

    public void OnPointerClick(PointerEventData eventData) {
      throw new System.NotImplementedException();
    }
  }

  public class MissionTimerBehaviour : MonoBehaviour {
    [SerializeField]
    private Image timerImage;

    private float leftTime;
    
    public void Initialize(float duration) {
    }
  }
}