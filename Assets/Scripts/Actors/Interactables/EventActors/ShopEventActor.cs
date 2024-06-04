using UnityEngine;

public class ShopEventActor : BaseEventActor
{
    [SerializeField] ShopData _shopData;

    protected override void OnInteract()
    {
        base.OnInteract();

        GameManager.Data.CurrentShop = _shopData;
    }
}
