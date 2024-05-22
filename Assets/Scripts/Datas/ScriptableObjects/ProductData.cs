using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Type Data", fileName = "ProductData")]
public class ProductData : ScriptableObject
{
    public ProductEnum Type;
    public Sprite Image;
    public string Name;
    public string Description;
    public int Price;
    public int Count;
}
