using DG.Tweening;
using UnityEngine;

public class Door : BaseInteractableActor, IHitable
{
    [SerializeField] private bool _isSingle;
    private bool _isOpen;
    private Transform _doorRight, _doorLeft;
    private float _doorRightOriginY, _doorLeftOriginY;

    public override void InitForWorld()
    {
        base.InitForWorld();

        _doorRight = transform.GetChild(1);
        _doorRightOriginY = _doorRight.localEulerAngles.y;
        if (_isSingle == false)
        {
            _doorLeft = transform.GetChild(0);
            _doorLeftOriginY = _doorLeft.localEulerAngles.y;
        }

        _isOpen = false;
    }

    public override void InitForBattle()
    {
        base.InitForBattle();
    }

    public override bool Interact()
    {
        if (_isOpen)
        {
            _doorRight.localEulerAngles = (Vector3.up * _doorRightOriginY);
            if (_isSingle == false)
            {
                _doorLeft.localEulerAngles = (Vector3.up * _doorLeftOriginY);
            }
        }
        else
        {
            _doorRight.localEulerAngles = (Vector3.up * (_doorRightOriginY - 90f));
            if (_isSingle == false)
            {
                _doorLeft.localEulerAngles = (Vector3.up * (_doorLeftOriginY + 90f));
            }
        }

        _isOpen = _isOpen == false;

        return base.Interact();
    }

    public void Hit(int damage)
    {
        if (damage >= 0)
            Destroy(gameObject);
    }

    public float GetStatus(StatusEnum status)
    {
        return 0f;
    }

    public int GetCondition()
    {
        return 0;
    }
}
