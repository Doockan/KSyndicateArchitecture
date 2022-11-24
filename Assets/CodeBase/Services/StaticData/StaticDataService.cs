using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private const string StaticDataMonstersPath = "StaticData/Monster";
    private const string StaticDataLevelsPath = "StaticData/Levels";
    private const string StaticDataWindowsPath = "StaticData/UI/WindowStaticData";
    private Dictionary<MonsterTypeID, MonsterStaticData> _monsters;
    private Dictionary<string, LevelStaticData> _levels;
    private Dictionary<WindowId,WindowConfig> _windowConfigs;

    public void LoadMonsters()
    {
      _monsters = Resources
        .LoadAll<MonsterStaticData>(StaticDataMonstersPath)
        .ToDictionary(x => x.MonsterTypeID, x => x);
      
      _levels = Resources
        .LoadAll<LevelStaticData>(StaticDataLevelsPath)
        .ToDictionary(x => x.LevelKey, x => x);
      
      _windowConfigs = Resources
        .Load<WindowStaticData>(StaticDataWindowsPath)
        .Configs
        .ToDictionary(x => x.WindowId, x => x);

    }

    public MonsterStaticData ForMonster(MonsterTypeID typeID) =>
      _monsters.TryGetValue(typeID, out MonsterStaticData monsterStaticData)
        ? monsterStaticData
        : null;

    public LevelStaticData ForLevel(string sceneKey) =>
      _levels.TryGetValue(sceneKey, out LevelStaticData levelStaticData)
        ? levelStaticData
        : null;

    public WindowConfig ForWindow(WindowId windowId) =>
      _windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig)
        ? windowConfig
        : null;
  }
}