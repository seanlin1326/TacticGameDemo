using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkillManager : MonoBehaviour
{
    public static TestSkillManager instance;
    public BTNPanelControllerTest bTNPanelController;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

    }
    private void Start()
    {
        bTNPanelController.testButton.onClick.AddListener(TestGameManager.instance.testSkills[1].Execute);
    }
    public void HaHa()
    {
        bTNPanelController.HaHaText();
    }
    public void QQ()
    {
        bTNPanelController.QQText();
    }
}
