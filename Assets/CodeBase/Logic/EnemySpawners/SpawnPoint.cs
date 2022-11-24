using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
  public class SpawnPoint : MonoBehaviour, ISavedProgress
  {
    public MonsterTypeID MonsterTypeID;
    public string Id { get; set; }

    private bool _slain;
    private IGameFactory _gameFactory;

    public void Construct(IGameFactory gameFactory) => 
      _gameFactory = gameFactory;

    public void LoadProgress(PlayerProgress progress)
    {
      if (!progress.KillData.ClearedSpawners.Contains(Id))
        Spawn();
    }

    public void UpdateProgress(PlayerProgress progress)
    {
      if (_slain)
        progress.KillData.ClearedSpawners.Add(Id);
    }

    private void Spawn() =>
      _gameFactory.CreateMonster(MonsterTypeID, transform)
        .GetComponent<EnemyDeath>()
        .Happened += Slain;

    private void Slain() =>
      _slain = true;
  }
}