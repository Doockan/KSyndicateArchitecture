using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    
    GameObject CreateHud();
    GameObject CreateHero(Vector3 at);
    GameObject CreateMonster(MonsterTypeID typeID, Transform parent);
    LootPiece CreateLoot();
    void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeID monsterTypeId);

    void Cleanup();
  }
}