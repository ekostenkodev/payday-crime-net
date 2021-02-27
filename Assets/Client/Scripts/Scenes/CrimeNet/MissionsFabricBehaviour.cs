using System.IO;
using Kadoy.CrimeNet.Models.Missions;
using Newtonsoft.Json;
using UnityEngine;

namespace Kadoy.CrimeNet.Missions {
  public class MissionsFabricBehaviour : MonoBehaviour {
    private const string MissionPath = "Missions";
    
    [SerializeField]
    private MissionBehaviour missionPrefab;

    [SerializeField]
    private Transform root;

    private void Start() {
      var missionAssets = Resources.LoadAll<TextAsset>(MissionPath);
      
      foreach (var missionAsset in missionAssets) {
        var missionInfo = JsonConvert.DeserializeObject<InnerMissionInfo>(missionAsset.text);
        var mission = Instantiate(missionPrefab, root);

        mission.Initialize(missionInfo);
      }
    }
  }
}