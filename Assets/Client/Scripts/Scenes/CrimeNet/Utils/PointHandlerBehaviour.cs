using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Kadoy.CrimeNet.Utils {
  public class PointHandlerBehaviour : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler,
    IPointerUpHandler, IPointerDownHandler {
    
    public event Action<PointerEventData> Click;
    public event Action<PointerEventData> Enter;
    public event Action<PointerEventData> Exit;
    public event Action<PointerEventData> Up;
    public event Action<PointerEventData> Down;

    public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(eventData);
    public void OnPointerEnter(PointerEventData eventData) => Enter?.Invoke(eventData);
    public void OnPointerExit(PointerEventData eventData) => Exit?.Invoke(eventData);
    public void OnPointerUp(PointerEventData eventData) => Up?.Invoke(eventData);
    public void OnPointerDown(PointerEventData eventData) => Down?.Invoke(eventData);
  }
}