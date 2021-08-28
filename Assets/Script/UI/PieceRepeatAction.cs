using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PieceRepeatAction : MonoBehaviour
{   //RepeatActionPanel Button
    public List<Button> ActionButtons;

    //ConfirmCastPanel Button
    public Button confirmButton;
    //public Button skillBButton;
    public GameObject RepeatActionPanel;
    public GameObject ConfirmCastPanel;

    public Text Skill1NameText;
    public Text Skill2NameText;
    public Text Skill1ManaCostText;
    public Text Skill2ManaCostText;

    private int Skill1ManaCost=0;
    private int Skill2ManaCost=0;

    private void Update()
    {
        SetButtonInteractable();
    }

    #region -- RepeatActionPanel --
    public void SetRepeatActionPanel(bool _Switch)
    {
        SkillManager.instance.ClearAllEvent();
        // Debug.Log("Switch is "+ _Switch);
        RepeatActionPanel.SetActive(_Switch);
        if (_Switch)
        {
            Piece _piece = GameManager.instance.selected.GetComponent<Piece>();
            Skill1NameText.text = _piece.skills[0].SkillName;
            Skill2NameText.text = _piece.skills[1].SkillName;
            Skill1ManaCostText.text = _piece.skills[0].manaCost.ToString();
            Skill2ManaCostText.text = _piece.skills[1].manaCost.ToString();
            Skill1ManaCost = _piece.skills[0].manaCost;
            Skill2ManaCost = _piece.skills[1].manaCost;
           for(int i=0;i < ActionButtons.Count; i++)
            {  
                int _temp = i;
                //Debug.Log("haha is " + i);
                ActionButtons[i].onClick.AddListener(_piece.skills[i].Execute);
              
                if (_piece.skills[i] is OnePieceTargetBaseSkill)
                {
                  
                    OnePieceTargetBaseSkill _currentSkill = _piece.skills[i] as OnePieceTargetBaseSkill;
                    // SkillManager.instance.OnBeingSkillTarget += _currentSkill.BeingSkillEffect;
                    ActionButtons[i].onClick.AddListener(() => OnePieceTargetButtonFuntion(_currentSkill));
                    ActionButtons[i].onClick.AddListener(()=>UIManager.instance.SetSkillCastHintPanel(true,_currentSkill.skillCastHint));
                    // SkillManager.instance.skillMana = _piece.skills[i].manaCost;
                  
                }
             
                if (_piece.skills[i] is DefaultRangeBaseSkill)
                {
                    DefaultRangeBaseSkill _currentSkill = _piece.skills[i] as DefaultRangeBaseSkill;
                    //ActionButtons[i].onClick.AddListener(() => UIManager.instance.SetSkillCastHintPanel(false, ""));
                    ActionButtons[i].onClick.AddListener(() => DefaultAOEPrepare(_temp));
                    
                    // Debug.Log("這裡調用是 " + i);

                }
               
            }
            
           
           
         
        }
        else
        {
            foreach(var _btn in ActionButtons)
            {
                _btn.onClick.RemoveAllListeners();
            }
           // UIManager.instance.SetSkillCastHintPanel(false, "");
            //skillAButton.onClick.RemoveAllListeners();
            SkillManager.instance.ClearAllEvent();
            Skill1ManaCost = 0;
            Skill2ManaCost = 0;
        }
    }
    private void OnePieceTargetButtonFuntion(OnePieceTargetBaseSkill _skill)
    {
        SkillManager.instance.ClearAllEvent();
        SkillManager.instance.OnBeingSkillTarget += _skill.BeingSkillEffect;
        SkillManager.instance.skillMana = _skill.manaCost;
    }
    public void ChoiceToAttack()
    {
        UIManager.instance.SetSkillCastHintPanel(false, "");
        GameManager.instance.ShowAttackRange();
    }
    public void ChoiceToEndTurn()
    {
        UIManager.instance.SetSkillCastHintPanel(false, "");
        GameManager.instance.SwitchToEndState();
    }
    public void SetButtonInteractable()
    {
        if (GameManager.instance.turnOwner == GameManager.TurnOwner.Player1)
        {
            if (Skill1ManaCost > GameManager.instance.player1Mana)
                ActionButtons[0].interactable = false;
            else
                ActionButtons[0].interactable = true;
            if (Skill2ManaCost > GameManager.instance.player1Mana)
                ActionButtons[1].interactable = false;
            else
                ActionButtons[1].interactable = true;
        }
        else if (GameManager.instance.turnOwner == GameManager.TurnOwner.Player2)
        {
            if (Skill1ManaCost > GameManager.instance.player2Mana)
                ActionButtons[0].interactable = false;
            else
                ActionButtons[0].interactable = true;
            if (Skill2ManaCost > GameManager.instance.player2Mana)
                ActionButtons[1].interactable = false;
            else
                ActionButtons[1].interactable = true;
        }
    }
    #endregion
    #region -- ConfirmCastPanel --
    //關閉tConfirmCastPanel，可寫為 SetConfirmCastPanel(false, -1);
    public void SetConfirmCastPanel(bool _Switch, int _skillIndex)
    {
       // Debug.Log("AOE Hint");
        Piece _piece = GameManager.instance.selected.GetComponent<Piece>();

        if (_Switch)
        {
            UIManager.instance.SetSkillCastHintPanel(true, _piece.skills[_skillIndex].skillCastHint);
            if (_piece.skills[_skillIndex]  is DefaultRangeBaseSkill)
            {
                DefaultRangeBaseSkill _currentSkill = _piece.skills[_skillIndex] as DefaultRangeBaseSkill;
                confirmButton.onClick.AddListener(_currentSkill.BeingSkillEffect);
                confirmButton.onClick.AddListener(() => AfterDefaultAOEAction(_skillIndex));
            }
        }
        else
        {
            confirmButton.onClick.RemoveAllListeners();
            UIManager.instance.SetSkillCastHintPanel(false,"");
        }
        ConfirmCastPanel.SetActive(_Switch);
    }
    public void ChoiceToCancelSkill()
    {
        GameManager.instance.CloseAllRange();
        SetRepeatActionPanel(true);
        SetConfirmCastPanel(false, -1);
    }
   public void AfterDefaultAOEAction(int _SkillIndex)
    {
        Piece _piece = GameManager.instance.selected.GetComponent<Piece>();
        GameManager.TurnOwner _pieceOwner=GameManager.TurnOwner.Player1;
        if (_piece.owner == Piece.Owner.Player1)
            _pieceOwner = GameManager.TurnOwner.Player1;
        else if (_piece.owner == Piece.Owner.Player2)
            _pieceOwner = GameManager.TurnOwner.Player2;
        GameManager.instance.ManaSpend(_pieceOwner, _piece.skills[_SkillIndex].manaCost);
        SetConfirmCastPanel(false, -1);
        GameManager.instance.SwitchToEndState();

    }
    #endregion
    #region -- Default AOE --

    public void DefaultAOEPrepare(int _SkillIndex)
    {  
        //Debug.Log("這裡被調用是 "+ _SkillIndex);
        SetConfirmCastPanel(true,_SkillIndex);
        SetRepeatActionPanel(false);
    }
    #endregion

}
