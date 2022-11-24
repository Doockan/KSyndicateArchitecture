using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Services.Randomizer;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssets _asset;
    private readonly IStaticDataService _staticData;
    private readonly IRandomService _randomService;
    private readonly IPersistentProgressService _progressService;
    private readonly IWindowService _windowService;

    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

    private GameObject HeroGameObject { get; set; }

    public GameFactory(IAssets asset, IStaticDataService staticData, IRandomService randomService, 
      IPersistentProgressService progressService, IWindowService windowService)
    {
      _asset = asset;
      _staticData = staticData;
      _randomService = randomService;
      _progressService = progressService;
      _windowService = windowService;
    }

    public void Cleanup()
    {
      ProgressReaders.Clear();
      ProgressWriters.Clear();
    }

    public GameObject CreateMonster(MonsterTypeID typeID, Transform parent)
    {
      var monsterData = _staticData.ForMonster(typeID);
      var monster = Object.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity, parent);
      var health = monster.GetComponent<IHealth>();
      health.Current = monsterData.Hp;
      health.Max = monsterData.Hp;

      monster.GetComponent<ActorUI>().Construct(health);
      monster.GetComponent<AgentMoveToPlayer>().Construct(HeroGameObject.transform);
      monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

      var lootSpawner = monster.GetComponentInChildren<LootSpawner>();
      lootSpawner.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);
      lootSpawner.Construct(this, _randomService);

      var attack = monster.GetComponent<Attack>();
      attack.Construct(HeroGameObject.transform);
      attack.Damage = monsterData.Damage;
      attack.Cleavage = monsterData.Cleavage;
      attack.EffectiveDistance = monsterData.EffectiveDistance;
      
      monster.GetComponent<RotateToHero>()?.Construct(HeroGameObject.transform);

      return monster;
    }

    public LootPiece CreateLoot()
    {
      var lootPiece = InstantiateRegistered(AssetPath.Loot).GetComponent<LootPiece>();
      lootPiece.Construct(_progressService.Progress.WorldData);
      return lootPiece;
    }

    public GameObject CreateHero(Vector3 at) => 
      HeroGameObject = InstantiateRegistered(AssetPath.HeroPath, at);

    public GameObject CreateHud()
    {
      var hud = InstantiateRegistered(AssetPath.HudPath);
      hud.GetComponentInChildren<LootCounter>().Construct(_progressService.Progress.WorldData);

      foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
        openWindowButton.Construct(_windowService);

      return hud;
    }

    public void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeID monsterTypeId)
    {
      SpawnPoint spawner = InstantiateRegistered(AssetPath.Spawner, at)
        .GetComponent<SpawnPoint>();

      spawner.Construct(this);
      spawner.Id = spawnerId;
      spawner.MonsterTypeID = monsterTypeId;
    }

    public void Register(ISavedProgressReader progressReader)
    {
      if (progressReader is ISavedProgress progressWriter) 
        ProgressWriters.Add(progressWriter);

      ProgressReaders.Add(progressReader);
    }

    private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
    {
      GameObject gameObject = _asset.Instantiate(prefabPath, at);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }

    private GameObject InstantiateRegistered(string prefabPath)
    {
      GameObject gameObject = _asset.Instantiate(prefabPath);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
      foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
        Register(progressReader);
    }
  }
}