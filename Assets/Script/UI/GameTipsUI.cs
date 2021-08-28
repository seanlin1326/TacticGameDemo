using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameTipsUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public GameObject GameTipsPanel; 

    public void Update()
    {
        UpdateTipPanel();
    }
    public void UpdateTipPanel()
    {
        if (GameTipsPanel.activeSelf)
        {   if (canvasGroup.alpha <= 0)
                GameTipsPanel.SetActive(false);
            canvasGroup.alpha -= Time.deltaTime * 0.5f;
        }
    }
    public void OpenTipPanel()
    {
        if (GameTipsPanel.activeSelf)
        {
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.alpha = 1;
            GameTipsPanel.SetActive(true);
        }
    }
}
