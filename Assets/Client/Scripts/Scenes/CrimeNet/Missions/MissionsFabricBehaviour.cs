using System;
using System.Linq;
using System.Threading.Tasks;
using Kadoy.CrimeNet.Models.Missions;
using Newtonsoft.Json;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Kadoy.CrimeNet.Missions {
  public class MissionsFabricBehaviour : MonoBehaviour {
    private const string MissionPath = "CrimeNet/Missions";

    [SerializeField]
    private Transform root;

    [SerializeField]
    private MissionBehaviour missionPrefab;

    [Header("BOUNDS")]
    [SerializeField]
    private Collider2D screenBounds;

    [SerializeField]
    private Collider2D mapBounds;

    private async void Start() {
      var missionAsset = Resources.Load<TextAsset>(MissionPath);
      var missionBase = JsonConvert.DeserializeObject<InnerMissionBase>(missionAsset.text);

      var bounds = screenBounds.bounds;
      var width = bounds.size.x;
      var height = bounds.size.y;

      foreach (var missionInfo in missionBase.Missions) {
        var screenOffset = new Vector2(width, height) * 0.5f;
        var safeCount = 0;
        var mapPosition = Vector2.zero;

        while (safeCount < 100) {
          var randomPosition = new Vector2(Random.Range(0f, width), Random.Range(0f, height));

          mapPosition = randomPosition - screenOffset;

          var isOverlap = mapBounds.OverlapPoint(mapPosition);
          var isNeighborFree = Physics2D.OverlapCircleAll(mapPosition, 0.5f)
            .All(x => x.gameObject.layer != missionPrefab.gameObject.layer);

          if (isOverlap && isNeighborFree) {
            break;
          }

          safeCount++;
        }

        Instantiate(missionPrefab, root).Initialize(missionInfo, mapPosition);
        await Task.Delay(100);
      }
    }
  }
}