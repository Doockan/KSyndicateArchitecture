using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
  public class MonsterStaticData : ScriptableObject
  {
    public MonsterTypeID MonsterTypeID;
    
    public int Hp = 100;
    public float Damage = 30;

    public int MaxLoot;
    public int MinLoot;
    
    [Range(1f, 10)]
    public float MoveSpeed = 10;

    [Range(0.5f, 1)]
    public float EffectiveDistance = 1;

    [Range(0.5f, 1)]
    public float Cleavage = 1;

    public AssetReferenceGameObject PrefabReference;
  }
}