using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
  public class AgentMoveToPlayer : Follow
  {
    public NavMeshAgent Agent;

    private Transform _heroTransform;

    public void Construct(Transform heroTransform) =>
      _heroTransform = heroTransform;

    private void Update() =>
      SetDestinationForAgent();

    private void SetDestinationForAgent()
    {
      if (_heroTransform)
        Agent.destination = _heroTransform.position;
    }
  }
}