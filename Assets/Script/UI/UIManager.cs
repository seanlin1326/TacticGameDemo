using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public ConfirmSelectPieceUI confirmSelectPieceUI;
    public CancelRepeatMovePiece cancelRepeatMovePiece;
    public PieceRepeatAction pieceRepeatAction;
    public BackToLastStatePanel backToLastStatePanel;
    public GameTipsUI gameTips;
    public SkillCastHint skillCastHintPanel;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

    }
   public void SetConfirmSelectPanel(bool _switch)
    {
        confirmSelectPieceUI.SetPanel(_switch);
    }
    public void SetCancelRepeatMovePiece(bool _switch)
    {
        cancelRepeatMovePiece.SetPanel(_switch);
    }
    public void SetPieceRepeatAction(bool _switch)
    {
        pieceRepeatAction.SetRepeatActionPanel(_switch);
    }
    public void SetBackPanel(bool _switch)
    {
        backToLastStatePanel.SetPanel(_switch);
    }
    public void OpenGameTips()
    {
        gameTips.OpenTipPanel();
    }
    public void SetSkillCastHintPanel(bool _switch,string _hintInfo)
    {
        skillCastHintPanel.SetSkillCastPanal(_switch, _hintInfo);
    }
    public void SetConfirmCastPanel()
    {
        pieceRepeatAction.SetConfirmCastPanel(false, -1);
    }
}
