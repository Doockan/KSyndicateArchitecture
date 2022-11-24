using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;

namespace CodeBase.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void LoadMonsters();
    MonsterStaticData ForMonster(MonsterTypeID typeID);
    LevelStaticData ForLevel(string sceneKey);
    WindowConfig ForWindow(WindowId windowId);
  }
}