using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillCastHint : MonoBehaviour
{
    public GameObject skillCastHintPanel;
    public Text skillCastText;

    public void SetSkillCastPanal(bool _Switch,string _HintTxt)
    {
        if (_Switch)
        {
            skillCastHintPanel.SetActive(true);
            skillCastText.text = _HintTxt;
        }
        else
        {
            if (skillCastHintPanel.activeSelf)
                skillCastHintPanel.SetActive(false);
        }
    }
    
}
