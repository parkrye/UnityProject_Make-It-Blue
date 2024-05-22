using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PointerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public UnityEvent OnClick = new UnityEvent();
    public UnityEvent OnClickEnd = new UnityEvent();
    public UnityEvent<Direction, Direction> OnDrag = new UnityEvent<Direction, Direction>();

    private bool _isClick, _isDrag;
    private bool _isDragging;

    private Vector2 _prevPosition;

    public void InitButton(bool isClick = false, bool isDrag = false)
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
        var dirUD = currentPosition.y - _prevPosition.y > 0f ? Direction.Up : Direction.Dowm;
        var dirLR = currentPosition.x - _prevPosition.x > 0f ? Direction.Right : Direction.Left;

        OnDrag?.Invoke(dirUD, dirLR);
        _isDragging = false;
    }
}
