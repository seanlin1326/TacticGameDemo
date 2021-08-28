using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Piece : MonoBehaviour
{
    public enum Owner
    {
        Player1,
        Player2,
        Other
    }
    public PieceData pieceData;
    public List<Skill> skills;
    public bool ScriptDisable;
    public Owner owner = Owner.Player1;
    public string pieceName;
    public int attackPower;
    public int armor;
    public int MaxHealth = 3;
    public int health;

    public int moveRange;
    public int maxMoveTime = 1;
    public int attackRange;
    public int maxAttackTime = 1;
    //public int playerNumber;

    public bool hasMove;
    public bool hasAttack;
    public bool beingAttackable;
    public bool beingSkillable;
    public Sprite pieceImage;
    //目前站在的Cell  
    public GameObject cellStandOn;
    public GameObject CanBeAttackIcon;
   //SelecteState
    public GameObject CanBeSelectIcon;
    public GameObject currentSelectedIcon;
    public GameObject CanBeSkillTargetIcon;
    public SpriteRenderer sr;
    public PieceUI pieceUI;

   // public event Action OnBeingSkillTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        pieceInitial();
        health = MaxHealth;
        SetStandCell(true);
        sr.sprite = pieceImage;
        pieceUI.SetPieceUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (ScriptDisable)
            return;
        if (
         
           GameManager.instance.turnState == GameManager.TurnState.SelectState
           )
        {
            if (
            (
            (owner == Owner.Player1 && GameManager.instance.turnOwner == GameManager.TurnOwner.Player1) ||
            (owner == Owner.Player2 && GameManager.instance.turnOwner == GameManager.TurnOwner.Player2)
            )
           )
            {
                if ( !CanBeSelectIcon.activeSelf)
                CanBeSelectIcon.SetActive(true);
                if (GameManager.instance.selected != gameObject && currentSelectedIcon.activeSelf)
                    currentSelectedIcon.SetActive(false);
            }
            else if
            (
            (owner == Owner.Player1 && GameManager.instance.turnOwner == GameManager.TurnOwner.Player2) ||
            (owner == Owner.Player2 && GameManager.instance.turnOwner == GameManager.TurnOwner.Player1)
            )
            {
                if (CanBeSelectIcon.activeSelf)
                    CanBeSelectIcon.SetActive(false);
            }
        }
    }
    private void OnMouseDown()
    {
        if (ScriptDisable)
            return;
        Debug.Log("Piece");
        if (
            (
            (owner == Owner.Player1 && GameManager.instance.turnOwner == GameManager.TurnOwner.Player1) ||
            (owner == Owner.Player2 && GameManager.instance.turnOwner == GameManager.TurnOwner.Player2)
            )
            && GameManager.instance.turnState == GameManager.TurnState.SelectState
            )
        {
            GameManager.instance.selected = gameObject;
            currentSelectedIcon.SetActive(true);
            UIManager.instance.SetConfirmSelectPanel(true);
        }
        if (
        (
        (owner == Owner.Player1 && GameManager.instance.turnOwner == GameManager.TurnOwner.Player1) ||
        (owner == Owner.Player2 && GameManager.instance.turnOwner == GameManager.TurnOwner.Player2)
        )

        && GameManager.instance.turnState == GameManager.TurnState.MoveState
        )
        {

            // GameManager.instance.CloseAllRange();
            // GameManager.instance.selected = gameObject;
            // GameManager.instance.ShowMoveRange();


        }
        else if (
            (
            (owner == Owner.Player1 && GameManager.instance.turnOwner == GameManager.TurnOwner.Player1) ||
            (owner == Owner.Player2 && GameManager.instance.turnOwner == GameManager.TurnOwner.Player2)
            )
             && GameManager.instance.turnState == GameManager.TurnState.ActionState
             )
            
           
        {
               // GameManager.instance.ShowAttackRange();
        }
        if (beingAttackable)
        {

            //Debug.Log(GameManager.instance.GetComponent<Piece>().name);
            GetHurt(GameManager.instance.selected.GetComponent<Piece>().attackPower);
            //  GameManager.instance.selected.GetComponent<Piece>().hasAttack=true;
            //GameManager.instance.hasAttack = true;
            GameManager.instance.MaxAction--;
            GameManager.instance.CloseAllRange();
            if (GameManager.instance.MaxAction >= 1)
            {
                UIManager.instance.SetPieceRepeatAction(true);
            }
            else if (GameManager.instance.MaxAction <= 0)
            {
                UIManager.instance.SetPieceRepeatAction(false);
                GameManager.instance.SwitchToEndState();
            }
            AudioManager.instance.audioAttack.Play();
        }
        if (beingSkillable)
        {
            UIManager.instance.SetSkillCastHintPanel(false,"");
            SkillManager.instance.BeingSkillTarget(this);
            SkillManager.instance.CostMana();
            GameManager.instance.MaxAction--;
            GameManager.instance.CloseAllRange();
            if (GameManager.instance.MaxAction >= 1)
            {
                UIManager.instance.SetPieceRepeatAction(true);
            }
            else if (GameManager.instance.MaxAction <= 0)
            {
                UIManager.instance.SetPieceRepeatAction(false);
                GameManager.instance.SwitchToEndState();
            }
            AudioManager.instance.audioMagic1.Play();
        }
        ShowPieceInfoUIController.instance.ShowPieceInfo(this);
    }

    
    public void SetStandCell(bool _stand)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, new Vector3(0, 0, 1));
        foreach (var hit in hits)
        {
            if (hit.collider.tag == "Cell")
            {
                if (_stand)
                {
                    if (owner == Owner.Player1)
                    {
                        hit.collider.GetComponent<Cell>().playerOnCell = Cell.PlayerOnCell.Player1;
                        hit.collider.GetComponent<Cell>().pieceOnCellScript = this;
                        cellStandOn = hit.collider.gameObject;
                    }
                    else if (owner == Owner.Player2)
                    {
                        hit.collider.GetComponent<Cell>().playerOnCell = Cell.PlayerOnCell.Player2;
                        hit.collider.GetComponent<Cell>().pieceOnCellScript = this;
                        cellStandOn = hit.collider.gameObject;
                    }
                }
                else
                {
                    hit.collider.GetComponent<Cell>().playerOnCell = Cell.PlayerOnCell.NoPlayer;
                    hit.collider.GetComponent<Cell>().pieceOnCellScript = null;
                    cellStandOn = null;

                }
            }
        }

    }

    public void Move(float xOffset,float yOffset)
    {
        SetStandCell(false);
        transform.position = new Vector2(xOffset, yOffset);
        GameManager.instance.CloseMoveRange();
        SetStandCell(true);
        //  hasMove = true;
        //  GameManager.instance.hasMove = true;
        //  GameManager.instance.selectPieceLock = true;
       
    }
    public void Reset()
    {
        hasMove = false;
        hasAttack = false;
        beingAttackable = false;
    }
    #region -- Piece Affected --
    public void GetHurt(int _damage)
    {
        health -= _damage - armor;
        pieceUI.SetPieceUI();
       // UIManager.instance.ShowInfo(this);
        if (health <= 0)
        {
            Dead();

        }
    }
    public void GetHeal(int _healAmount)
    {
        if (_healAmount <= 0)
            return;
        if (health + _healAmount >= MaxHealth)
            health = MaxHealth;
        else 
        health += _healAmount;
        pieceUI.SetPieceUI();
        Debug.Log("Piece Get Heal");
    }
    #endregion
    public void Dead()
    {
        SetStandCell(false);
        
           if (GameManager.instance.player1Pieces.Contains(gameObject))
               GameManager.instance.player1Pieces.Remove(gameObject);
           else if(GameManager.instance.player2Pieces.Contains(gameObject))
               GameManager.instance.player2Pieces.Remove(gameObject);

        if (GameManager.instance.allOpenPieceList.Contains(gameObject))
            GameManager.instance.allOpenPieceList.Remove(gameObject);

        if (GameManager.instance.allPieceList.Contains(gameObject))
            GameManager.instance.allPieceList.Remove(gameObject);
        
        Destroy(gameObject);
       
    }
   public void pieceInitial()
    {
        pieceImage = pieceData.pieceSprite;
        pieceName = pieceData.pieceName;
        attackPower = pieceData.attackPower;
        attackRange = pieceData.attackRange;
      
        MaxHealth = pieceData.MaxHealth;
        armor = pieceData.armor;
        moveRange = pieceData.moveRange;
        maxMoveTime = pieceData.maxMoveTime;

        skills.Clear();
        foreach(var _skill in pieceData.skills)
        {
            skills.Add(_skill);
        }
        
    }
    
}
