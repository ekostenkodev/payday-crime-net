using UnityEngine;

namespace Kadoy.CrimeNet.MenuCamera {
  public class CameraMovement : MonoBehaviour {
    [SerializeField]
    private Camera movableCamera;

    [Space]
    [SerializeField]
    private Collider2D boundsRoot;

    [Header("SETTINGS")]
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector2 thickness;

    private Transform root;
    private Vector2 bounds;

    private void Awake() {
      var screenSize = new Vector3(movableCamera.pixelWidth, movableCamera.pixelHeight);
      var worldScreenSize = movableCamera.ScreenToWorldPoint(screenSize);
      
      root = movableCamera.transform;
      bounds = boundsRoot.bounds.size * 0.5f - worldScreenSize;
    }

    private void Update() {
      var cameraPosition = root.position;
      var mousePosition = Input.mousePosition;
      var moveDirection = Vector3.zero;

      var isOverTop = mousePosition.y >= Screen.height - thickness.y;
      var isOverBottom = mousePosition.y <= thickness.y;
      var isOverRight = mousePosition.x >= Screen.width - thickness.x;
      var isOverLeft = mousePosition.x <= thickness.x;

      moveDirection.y = isOverTop ? 1 : isOverBottom ? -1 : 0;
      moveDirection.x = isOverRight ? 1 : isOverLeft ? -1 : 0;

      moveDirection *= speed * Time.deltaTime;
      cameraPosition += moveDirection;

      cameraPosition.x = Mathf.Clamp(cameraPosition.x, -bounds.x, bounds.x);
      cameraPosition.y = Mathf.Clamp(cameraPosition.y, -bounds.y, bounds.y);

      root.position = cameraPosition;
    }
  }
}