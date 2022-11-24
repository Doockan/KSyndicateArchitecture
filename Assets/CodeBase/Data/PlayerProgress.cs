using System;

namespace CodeBase.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public State HeroState;
    public Stats Stats;
    public WorldData WorldData;
    public KillData KillData;

    public PlayerProgress(string initialLevel)
    {
      WorldData = new WorldData(initialLevel);
      HeroState = new State();
      Stats = new Stats();
      KillData = new KillData();
    }
  }
}