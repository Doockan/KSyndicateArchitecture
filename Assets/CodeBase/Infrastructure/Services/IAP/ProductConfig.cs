using System;
using UnityEngine.AddressableAssets;
using UnityEngine.Purchasing;

namespace CodeBase.Infrastructure.Services.IAP
{
  [Serializable]
  public class ProductConfig
  {
    public string Id;
    public ProductType ProductType;

    public int MaxPurchaseCount;
    public ItemType ItemType;
    public int Quantity;
    public string Price;
    public string Icon;
  }
}