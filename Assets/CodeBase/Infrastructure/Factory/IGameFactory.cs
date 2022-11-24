using System.Collections.Generic;
using System.Threading.Tasks;
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
    
    Task<GameObject> CreateHud();
    Task<GameObject> CreateHero(Vector3 at);
    Task<GameObject> CreateMonster(MonsterTypeID typeID, Transform parent);
    Task<LootPiece> CreateLoot();
    Task CreateSpawner(Vector3 at, string spawnerId, MonsterTypeID monsterTypeId);

    void Cleanup();
    Task WarmUp();
  }
}