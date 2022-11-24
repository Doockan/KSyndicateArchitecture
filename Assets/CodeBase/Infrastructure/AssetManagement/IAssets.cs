using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.AssetManagement
{
  public interface IAssets : IService
  {
    Task<GameObject> Instantiate(string path);
    Task<GameObject> Instantiate(string path, Vector3 at);
    Task<T> Load<T>(AssetReference assetReference) where T : class;
    Task<T> Load<T>(string address) where T : class;
    void Cleanup();
    void Initialize();
  }
}