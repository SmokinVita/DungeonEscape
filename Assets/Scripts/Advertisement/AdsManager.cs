using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField]
    private Button _showAdButton;
    [SerializeField]
    private string _androidAdUnitId = "Rewarded_Andriod";
    [SerializeField]
    private string _iOSAdUnitId = "Rewarded_iOS";
    private string _adUnitId = null;

    private Player _player;

    void Awake()
    {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        _showAdButton.interactable = false;
    }

    public void LoadAd()
    {
        Debug.Log($"Loading Ad: {_adUnitId}");
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log($"Ad Loaded: {adUnitId}");

        if (adUnitId.Equals(_adUnitId))
        {
            //_showAdButton.onClick.AddListener(ShowAd);
            _showAdButton.interactable = true;
        }
    }

    public void ShowAd()
    {
        _showAdButton.interactable = false;
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        
        if (placementId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Completed Ad!");
            GameManager.instance.Player.AddDiamonds(100);
            StartCoroutine(AdWaitRoutine());
        }
    }

    IEnumerator AdWaitRoutine()
    {
        yield return new WaitForSeconds(60f);
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowClick(string adUnitId)
    {

    }
    public void OnUnityAdsShowStart(string adUnitId)
    {

    }

    void OnDestroy()
    {
        _showAdButton.onClick.RemoveAllListeners();
    }


}
