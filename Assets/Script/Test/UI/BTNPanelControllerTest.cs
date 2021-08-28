using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BTNPanelControllerTest : MonoBehaviour
{
    public Text testText;
    public Button testButton;
    // Start is called before the first frame update
    void Start()
    {
       // testButton.onClick.AddListener(QQText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void QQText()
    {
        Debug.Log("QQ");
        testText.text = "QQ";
        testButton.onClick.RemoveAllListeners();
       // testButton.onClick.AddListener(HaHaText);
    }
    public void HaHaText()
    {
        Debug.Log("HaHA");
        testText.text = "HaHA";
        testButton.onClick.RemoveAllListeners();
       // testButton.onClick.AddListener(QQText);
    }
}
