using System;
using UnityEngine;
using UnityEngine.Advertisements;
using Application = UnityEngine.Device.Application;

namespace CodeBase.Infrastructure.Services.Asd
{
  public class AdsService : IAdsService, IUnityAdsListener 
  {
    private const string AndroidGameId = "5034068";
    private const string IOSGameId = "5034069";

    private const string RewardedVideoPlacementId = "KSyndicateArchitectureAds";

    private string _gameID;

    public event Action RewardedVideoReady;
    private Action _onVideoFinished;

    public int Reward => 13;

    public void Initialize()
    {
      switch (Application.platform)
      {
        case RuntimePlatform.Android:
          _gameID = AndroidGameId;
          break;
        case RuntimePlatform.IPhonePlayer:
          _gameID = IOSGameId;
          break;
        case RuntimePlatform.WindowsEditor:
          _gameID = AndroidGameId;
          break;
        default:
          Debug.Log("Unsupported platform for ads");
          break;
      }
      
      Advertisement.AddListener(this);
      Advertisement.Initialize(_gameID);
    }

    public void ShowRewardedVideo(Action onVideoFinished)
    {
      Advertisement.Show(RewardedVideoPlacementId);
      
      _onVideoFinished = onVideoFinished;
    }

    public bool IsRewardedVideoReady => 
      Advertisement.IsReady(RewardedVideoPlacementId);

    public void OnUnityAdsReady(string placementId)
    {
      Debug.Log($"OnUnityAdsReady {placementId}");
      
      if(placementId == RewardedVideoPlacementId)
        RewardedVideoReady?.Invoke();
    }

    public void OnUnityAdsDidError(string message) => 
      Debug.Log($"OnUnityAdsDidError {message}");

    public void OnUnityAdsDidStart(string placementId) => 
      Debug.Log($"OnUnityAdsDidStart {placementId}");

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
      switch (showResult)
      {
        case ShowResult.Failed:
          Debug.LogError($"OnUnityAdsDidFinish {showResult}");
          break;
        case ShowResult.Skipped:
          Debug.LogError($"OnUnityAdsDidFinish {showResult}");
          break;
        case ShowResult.Finished:
          _onVideoFinished?.Invoke();
          break;
        default:
          Debug.LogError($"OnUnityAdsDidFinish {showResult}");
          break;
      }

      _onVideoFinished = null;
    }
  }
}