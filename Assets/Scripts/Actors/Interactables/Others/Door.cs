using DG.Tweening;
using UnityEngine;

public class Door : BaseInteractableActor, IHitable
{
    [SerializeField] private bool _isSingle;
    private bool _isOpen;
    private Transform _door, _door2;

    public override void InitForWorld()
    {
        base.InitForWorld();

        _door = transform.GetChild(1);
        if (_isSingle == false)
            _door2 = transform.GetChild(0);

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
            _door.DOKill();
            _door.DORotate(Vector3.zero, 1f);
            if (_isSingle == false)
            {
                _door2.DOKill();
                _door2.DORotate(Vector3.zero, 1f);
            }
            _isOpen = false;
        }
        else
        {
            _door.DOKill();
            _door.DORotate(Vector3.up * -90f, 1f);
            if (_isSingle == false)
            {
                _door2.DOKill();
                _door2.DORotate(Vector3.up * 90f, 1f);
            }
            _isOpen = true;
        }

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
