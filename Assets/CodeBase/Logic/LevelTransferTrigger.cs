using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Logic
{
  public class LevelTransferTrigger : MonoBehaviour
  {
    private const string Player = "Player";
    
    public string TransferTo;
    private IGameStateMachine _stateMacine;

    private bool _triggered;
    
    private void Awake() =>
      _stateMacine = AllServices.Container.Single<IGameStateMachine>();


    private void OnTriggerEnter(Collider other)
    {
      if(_triggered)
        return;
      
      if (other.CompareTag(Player))
        _stateMacine.Enter<LoadLevelState, string>(TransferTo);
      _triggered = true;
    }
  }
}