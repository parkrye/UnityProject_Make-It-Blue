using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Shop Data", fileName = "ShopData")]
public class ShopData : ScriptableObject
{
    public string Name;
    public string Description;
    public ProductData[] Products;
}
