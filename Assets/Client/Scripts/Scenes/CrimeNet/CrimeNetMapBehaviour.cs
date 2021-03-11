using System.Linq;
using Kadoy.CrimeNet.Missions.Bubble;
using UnityEngine;

namespace Kadoy.CrimeNet {
  public class CrimeNetMapBehaviour : MonoBehaviour {
    private const float NeighborRadius = 0.5f;

    [SerializeField]
    private Collider2D screenBounds;

    [SerializeField]
    private Collider2D mapBounds;
    
    public Vector2 GetRandomBubblePosition() {
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