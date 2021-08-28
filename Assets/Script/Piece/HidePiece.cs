using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePiece : MonoBehaviour
{
    public bool ScriptDisable;
    public bool CanBeOpen;
    public GameObject HideImage;
    public GameObject CanTurnOnIcon;
    public PieceController pieceController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ScriptDisable)
            return;
        if (GameManager.instance.turnState == GameManager.TurnState.SelectState && !GameManager.instance.hasOpen && !CanTurnOnIcon.activeSelf)
        {
            CanTurnOnIcon.SetActive(true);
            CanBeOpen = true;
        }
       if (GameManager.instance.turnState != GameManager.TurnState.SelectState&& CanTurnOnIcon.activeSelf)
        {
            CanTurnOnIcon.SetActive(false);
            CanBeOpen = false;
        }
    }
    private void OnMouseDown()
    {
        if (ScriptDisable)
            return;
        if (GameManager.instance.turnState == GameManager.TurnState.SelectState&& !GameManager.instance.hasOpen&&CanBeOpen)
        {
            CanTurnOnIcon.SetActive(false);
            HideImage.SetActive(false);
            GameManager.instance.hasOpen = true;
            
            pieceController.SwitchToFaceOn();
            GameManager.instance.SwitchToEndState();
        }
    }
}
