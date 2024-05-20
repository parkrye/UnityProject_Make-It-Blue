using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Product Data", fileName = "ProductData")]
public class ProductData : ScriptableObject
{
    public ProductEnum Product;
    public string Name;
    public string Description;
    public int Price;
    public int Count;
}
