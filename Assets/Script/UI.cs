using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private CanvasGroup creditCG;
    [SerializeField] private Button quitBtn;
    
    public static UI Instance;

    private void Awake()
    {
        Instance = this;
        creditCG.DOFade(0, 0);
        creditCG.gameObject.SetActive(false);
        quitBtn.onClick.AddListener((() => Application.Quit()));
    }


    public void EnableEndingUI()
    {
        creditCG.gameObject.SetActive(true);
        creditCG.DOFade(1, 1.25f);
    }
    
}
