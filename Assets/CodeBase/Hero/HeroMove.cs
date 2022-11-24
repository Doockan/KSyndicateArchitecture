using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Services;
using CodeBase.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Hero
{
  [RequireComponent(typeof(CharacterController))]
  public class HeroMove : MonoBehaviour, ISavedProgress
  {
    public float MovementSpeed = 10f;

    private CharacterController _characterController;
    private IInputService _input;

    public void Awake()
    {
      _input = AllServices.Container.Single<IInputService>();

      _characterController = GetComponent<CharacterController>();
    }

    public void Update()
    {
      var movementVector = Vector3.zero;

      if (_input.Axis.sqrMagnitude > Constants.Epsilon)
      {
        movementVector = Camera.main.transform.TransformDirection(_input.Axis);
        movementVector.y = 0;
        movementVector.Normalize();
        transform.forward = movementVector;
      }

      movementVector += Physics.gravity;
      _characterController.Move(MovementSpeed * movementVector * Time.deltaTime);
    }

    public void UpdateProgress(PlayerProgress progress)
    {
      progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
    }

    public void LoadProgress(PlayerProgress progress)
    {
      Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
      if (savedPosition != null) 
        Warp(to: savedPosition);
    }

    private void Warp(Vector3Data to)
    {
      _characterController.enabled = false;
      transform.position = to.AsUnityVector().AddY(_characterController.height);
      _characterController.enabled = true;
    }

    private static string CurrentLevel() => 
      SceneManager.GetActiveScene().name;
  }
}