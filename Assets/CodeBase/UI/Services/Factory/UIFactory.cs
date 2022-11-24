using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.Asd;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Shop;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
  public class UIFactory : IUIFactory
  {
    private const string UIRootPath = "UI/UIRoot";
    private readonly IAssets _assets;
    private readonly IStaticDataService _staticDataService;
    private readonly IPersistentProgressService _progressService;
    private readonly IAdsService _adsService;

    private Transform _uiRoot;

    public UIFactory(IAssets assets, IStaticDataService staticDataService, IPersistentProgressService progressService, IAdsService adsService)
    {
      _assets = assets;
      _staticDataService = staticDataService;
      _progressService = progressService;
      _adsService = adsService;
    }

    public void CreateShop()
    {
      WindowConfig config = _staticDataService.ForWindow(WindowId.Shop);
      ShopWindow window = Object.Instantiate(config.Prefab, _uiRoot) as ShopWindow ;
      window.Construct(_adsService, _progressService);
    }

    public void CreateUIRoot() => 
      _uiRoot = _assets.Instantiate(UIRootPath).transform;
  }
}