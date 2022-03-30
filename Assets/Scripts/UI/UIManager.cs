using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("UIManager is Null!");

            return _instance;
        }
    }

    [SerializeField]
    private GameObject _shopCanvas;

    [SerializeField]
    private TMP_Text _shopGemCount;
    [SerializeField]
    private TMP_Text _gemCount;

    [SerializeField]
    private Image[] _healthBars;

    [SerializeField]
    private Image _selectionImg;

    void Awake()
    {
        _instance = this;
    }

    public void OpenShop()
    {
        _shopCanvas.SetActive(true);
    }

    public void CloseShop()
    {
        _shopCanvas.SetActive(false);
    }

    public void UpdateGemCount(int gems)
    {
        _shopGemCount.SetText($"{gems}G");
        _gemCount.SetText($"{gems}");
    }
    
    public void UpdateSelection(float yPos)
    {
        _selectionImg.rectTransform.anchoredPosition = new Vector2(_selectionImg.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateLives(int livesRemaining)
    {

        for (int i = 0; i <= livesRemaining; i++)
        {
            if (i == livesRemaining)
                _healthBars[i].enabled = false;
        }

    }
}
