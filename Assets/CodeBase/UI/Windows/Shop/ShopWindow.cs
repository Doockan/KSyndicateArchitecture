using CodeBase.Infrastructure.Services.Asd;
using CodeBase.Infrastructure.Services.PersistentProgress;
using TMPro;

namespace CodeBase.UI.Windows.Shop
{
  public class ShopWindow : WindowBase
  {
    public TextMeshProUGUI MoneyText;
    public RewardedAdItem AdItem;

    public void Construct(IAdsService adsService, IPersistentProgressService progressService)
    {
      base.Construct(progressService);
      AdItem.Construct(adsService, progressService);
    }

    protected override void Initialize()
    {
      AdItem.Initialize();
      RefreshMoneyText();
    }

    protected override void SubscribeUpdates()
    {
      AdItem.Subscribe();
      Progress.WorldData.LootData.Changed += RefreshMoneyText;
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      AdItem.Cleanup();
      Progress.WorldData.LootData.Changed -= RefreshMoneyText;
    }


    private void RefreshMoneyText() => 
      MoneyText.text = Progress.WorldData.LootData.Collected.ToString();
  }
}