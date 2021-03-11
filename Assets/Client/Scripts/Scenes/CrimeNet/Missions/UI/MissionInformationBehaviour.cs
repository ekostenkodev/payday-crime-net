using System;
using Kadoy.CrimeNet.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Kadoy.CrimeNet.Missions.UI {
  public class MissionInformationBehaviour : MonoBehaviour {
    [SerializeField]
    private Button closeButton;
    
    public event Action Close;

    private void OnEnable() {
      closeButton.onClick.AddListener(OnCloseClick);
    }

    private void OnDisable() {
      closeButton.onClick.RemoveListener(OnCloseClick);
    }

    private void OnCloseClick() {
      Close?.Invoke();
    }
  }
}