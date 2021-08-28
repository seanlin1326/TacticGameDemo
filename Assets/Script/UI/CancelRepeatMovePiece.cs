using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelRepeatMovePiece : MonoBehaviour
{
    public GameObject CancelMovePanel;
    // Start is called before the first frame update
   public void SetPanel(bool _Switch)
    {
        CancelMovePanel.SetActive(_Switch);
    }
    public void ChoiceToMovePiece()
    {
        GameManager.instance.ShowMoveRange();
    }
    public void ChoiceToCancelMovePiece()
    {
        
        GameManager.instance.SwitchToActionState();
        SetPanel(false);
       
    }
}
