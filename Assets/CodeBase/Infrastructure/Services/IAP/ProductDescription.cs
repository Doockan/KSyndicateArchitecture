using UnityEngine.Purchasing;

namespace CodeBase.Infrastructure.Services.IAP
{
  public class ProductDescription
  {
    public string Id;
    public Product Product;
    public ProductConfig Config;
    public int AvailablePurchasesLeft;
  }
}