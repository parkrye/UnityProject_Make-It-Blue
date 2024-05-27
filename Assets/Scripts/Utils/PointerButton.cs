using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PointerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public UnityEvent OnClick = new UnityEvent();
    public UnityEvent OnClickEnd = new UnityEvent();
    public UnityEvent<DirectionEnum, DirectionEnum> OnDrag = new UnityEvent<DirectionEnum, DirectionEnum>();

    private bool _isClick = true, _isDrag = false;
    private bool _isDragging;

    private Vector2 _prevPosition;

    public void InitButton(bool isClick = true, bool isDrag = false)
    {
        _isClick = isClick;
        _isDrag = isDrag;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isClick == false)
            return;

        OnClick?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isClick == false)
            return;

        if (_isDragging == false)
            OnClickEnd?.Invoke();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isDrag == false)
            return;

        _prevPosition = eventData.position;
        _isDragging = true;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isDrag == false)
            return;

        var currentPosition = eventData.position;
        var dirUD = currentPosition.y - _prevPosition.y > 0f ? DirectionEnum.Up : DirectionEnum.Dowm;
        var dirLR = currentPosition.x - _prevPosition.x > 0f ? DirectionEnum.Right : DirectionEnum.Left;

        OnDrag?.Invoke(dirUD, dirLR);
        _isDragging = false;
    }
}
