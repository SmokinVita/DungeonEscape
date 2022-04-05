using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class InitializeAdsScript : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField]
    private string _androidGameId, _iOSGameId;
    [SerializeField]
    bool _testMode = true;
    private string _gameID;

    [SerializeField]
    private AdsManager _adsManager;

    void Awake()
    {
        _adsManager = GetComponent<AdsManager>();
        if(_adsManager == null)
        {
            Debug.LogError("Ads Manager is NULL!!");
        }
        InitializeAds();
    }
    
    public void InitializeAds()
    {
        _gameID = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSGameId : _androidGameId;
        Debug.Log($"GameID: {_gameID}");
        Advertisement.Initialize(_gameID, _testMode, this);
        
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Ad Initialization is complete!");
        _adsManager.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Ad Inititialization has failed. Error: {error.ToString()}, Message: {message}");
    }
}
