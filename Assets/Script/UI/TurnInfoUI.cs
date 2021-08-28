using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurnInfoUI : MonoBehaviour
{
    public Color player1Color;
    public Color player2Color;
    public Text turnOwnerInfoText;
    public Text TurnStateInfoText;
    public Text ManaText;
    public Image playerColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        turnOwnerInfoText.text = GameManager.instance.turnOwner.ToString();
        TurnStateInfoText.text = GameManager.instance.turnState.ToString();
        UpdateManaText();
        
    }
    void UpdateManaText()
    {
        if (GameManager.instance.turnOwner == GameManager.TurnOwner.Player1)
        {
            ManaText.text = GameManager.instance.player1Mana.ToString();
            playerColor.color = player1Color;

        }
        else if(GameManager.instance.turnOwner == GameManager.TurnOwner.Player2)
        {
            ManaText.text = GameManager.instance.player2Mana.ToString();
            playerColor.color = player2Color;
        }
    }
}
