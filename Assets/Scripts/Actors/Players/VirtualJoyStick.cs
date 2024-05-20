using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private bool _isForMove;

    [SerializeField] private RectTransform _joyStickBG, _joyStick;

    private bool _isDragging = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isDragging)
            return;

        _isDragging = true;
        _joyStickBG.gameObject.SetActive(true);

        var inputPosition = eventData.position;
        _joyStickBG.transform.position = inputPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDragging == false)
            return;

        var inputPosition = eventData.position;
        var dir = eventData.position - (Vector2)_joyStickBG.position;
        var sqrt = dir.sqrMagnitude;
        if (sqrt < 10000f)
        {
            if (sqrt < 400f)
                dir = Vector2.Lerp(Vector2.zero, dir.normalized, 0.2f);
            else if (sqrt < 1600f)
                dir = Vector2.Lerp(Vector2.zero, dir.normalized, 0.4f);
            else if (sqrt < 3600f)
                dir = Vector2.Lerp(Vector2.zero, dir.normalized, 0.6f);
            else if (sqrt < 6400f)
                dir = Vector2.Lerp(Vector2.zero, dir.normalized, 0.8f);
            else
                dir = dir.normalized;
        }
        else
        {
            inputPosition = (Vector2)_joyStickBG.position + dir.normalized * 100f;
            dir = dir.normalized;
        }
        _joyStick.transform.position = inputPosition;
        GameManager.System.PlayerActor.InputControllVector(dir, _isForMove);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isDragging == false)
            return;

        _isDragging = false;
        _joyStickBG.gameObject.SetActive(false);
        GameManager.System.PlayerActor.InputControllVector(Vector2.zero, _isForMove);
    }
}
