using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
  public class LootCounter : MonoBehaviour
  {
    public TextMeshProUGUI Counter;
    private WorldData _worldData;

    public void Construct(WorldData worldData)
    {
      _worldData = worldData;
      _worldData.LootData.Changed += UpdateCounter;
    }

    private void Start()
    {
      UpdateCounter();
    }

    private void UpdateCounter()
    {
      Counter.text = $"{_worldData.LootData.Collected}";
    }

    private void OnDestroy()
    {
      _worldData.LootData.Changed -= UpdateCounter;
    }
  }
}