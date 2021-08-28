using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmSelectPieceUI : MonoBehaviour
{
    public GameObject ConfirmSelectPanel;

    public void SetPanel(bool _Switch)
    {
        ConfirmSelectPanel.SetActive(_Switch);
    }
    public void ChoiceToMovePiece()
    {
        GameManager.instance.SwitchToMoveState();
        SetPanel(false);
        GameManager.instance.MaxMove = GameManager.instance.selected.GetComponent<Piece>().maxMoveTime;
        GameManager.instance.MaxAction = GameManager.instance.selected.GetComponent<Piece>().maxAttackTime;
        UIManager.instance.SetBackPanel(true);
    }
    public void ChoiceToCancelMoveState()
    {
        GameManager.instance.SwitchToActionState();
        SetPanel(false);
    }
}
