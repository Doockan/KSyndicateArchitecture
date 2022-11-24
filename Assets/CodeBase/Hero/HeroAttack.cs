using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero
{
  [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
  public class HeroAttack : MonoBehaviour, ISavedProgressReader
  {
    public HeroAnimator Animator;
    public CharacterController CharacterController;

    private IInputService _input;
    private static  int _layerMask;
    private Stats _stats;
    private Collider[] _hits = new Collider[3];

    private void Awake()
    {
      _input = AllServices.Container.Single<IInputService>();

      _layerMask = 1 << LayerMask.NameToLayer("Hittable");
    }

    private void Update()
    {
      if (_input.IsAttackButtonUp() && !Animator.IsAttacking) 
        Animator.PlayAttack();
    }

    public void OnAttack()
    {
      for (int i = 0; i < Hit(); i++)
      {
        _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
      }
    }

    public void LoadProgress(PlayerProgress progress) => 
      _stats = progress.Stats;

    private int Hit() => 
      Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.DamageRadius, _hits, _layerMask);

    private Vector3 StartPoint() =>
      new Vector3(transform.position.x, CharacterController.center.y / 2, transform.position.z);
  }
}