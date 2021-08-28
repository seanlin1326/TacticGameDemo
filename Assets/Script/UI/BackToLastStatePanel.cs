using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToLastStatePanel : MonoBehaviour
{
    public GameObject BackPanel;
    public void SetPanel(bool _Switch)
    {
       BackPanel.SetActive(_Switch);
    }
    public void Back()
    {
        GameManager.instance.SwitchToSelectState();
        SetPanel(false);
    }
}
