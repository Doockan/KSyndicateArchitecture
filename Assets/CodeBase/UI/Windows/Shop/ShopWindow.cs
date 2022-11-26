using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.Asd;
using CodeBase.Infrastructure.Services.IAP;
using CodeBase.Infrastructure.Services.PersistentProgress;
using TMPro;

namespace CodeBase.UI.Windows.Shop
{
  public class ShopWindow : WindowBase
  {
    public TextMeshProUGUI MoneyText;
    public RewardedAdItem AdItem;
    public ShopItemsContainer ShopItemsContainer;

    public void Construct(IAdsService adsService, IPersistentProgressService progressService, IIAPService iapService,
      IAssets assets)
    {
      base.Construct(progressService);
      AdItem.Construct(adsService, progressService);
      ShopItemsContainer.Construct(iapService, progressService, assets);

    }

    protected override void Initialize()
    {
      AdItem.Initialize();
      ShopItemsContainer.Initialize();
      RefreshMoneyText();
    }

    protected override void SubscribeUpdates()
    {
      AdItem.Subscribe();
      ShopItemsContainer.Subscribe();
      Progress.WorldData.LootData.Changed += RefreshMoneyText;
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      AdItem.Cleanup();
      ShopItemsContainer.Cleanup();
      Progress.WorldData.LootData.Changed -= RefreshMoneyText;
    }


    private void RefreshMoneyText() => 
      MoneyText.text = Progress.WorldData.LootData.Collected.ToString();
  }
}