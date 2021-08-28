using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum TurnOwner
    {
        Player1,
        Player2,
        Other
    }
    public enum TurnState
    {
        SelectState,
        MoveState,
        ActionState,
        EndState
    }

    public static GameManager instance;
    public TurnOwner turnOwner=TurnOwner.Player1;
    public TurnState turnState = TurnState.SelectState;
    public int player1Mana=0;
    public int player2Mana=0;
   [SerializeField] private GameObject[] cells;

    public GameObject[] allPieces;

    public List<GameObject> allPieceList;
    public List<GameObject> allOpenPieceList;
    public List<GameObject> allHidePieceList;

    public List<GameObject> player1Pieces;
    public List<GameObject> player2Pieces;
    //cell List
    public List<GameObject> moveList;
    private List<GameObject> attackList;
    public List<GameObject> skillRangeList;
    //piece List
    public List<Piece> PiecesInSkillArea;
    //當前被選擇使用的棋子
    public GameObject selected;
    //當前被技能選擇的物品
    public GameObject skillTarget;
    //SelecteState Constant
    public bool hasOpen;
    //MoveState 
    public int MaxMove=1;
    //ActionState
    public int MaxAction=1;
   // public bool actionAlready = false;
   //public bool hasMove;
   // public bool hasAttack;
   // public bool selectPieceLock;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        int _tempRandom = Random.Range(0, 2);
        if (_tempRandom == 0)
        {
            turnOwner = TurnOwner.Player1;
            player1Mana = 0;
            player2Mana = 0;
        }
        else
        {
            turnOwner = TurnOwner.Player2;
            player1Mana = 0;
            player2Mana = 0;
        }
       // actionAlready = false;
        moveList = new List<GameObject>();
        attackList = new List<GameObject>();
        cells = GameObject.FindGameObjectsWithTag("Cell");
        allPieces = GameObject.FindGameObjectsWithTag("Piece");
        foreach(var _piece in allPieces)
        {
            Piece _pieceScript = _piece.GetComponent<Piece>();
            if (_pieceScript.owner == Piece.Owner.Player1 &&!_pieceScript.ScriptDisable)
                player1Pieces.Add(_piece);
            else if (_pieceScript.owner == Piece.Owner.Player2 && !_pieceScript.ScriptDisable)
                player2Pieces.Add(_piece);
            allPieceList.Add(_piece);
            allHidePieceList.Add(_piece);
        }
        turnState = TurnState.SelectState;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(1))
        {

            CloseAllRange();
        }
        */
    }
    public void CloseAllRange()
    {
        CloseAttackRange();
        CloseMoveRange();
        CloseSkillRange();
        UIManager.instance.SetSkillCastHintPanel(false, ""); 
    }
    #region -- (Show/Close) ActionRange --

    public void ShowMoveRange()
    {

        CloseAllRange();
        UIManager.instance.SetSkillCastHintPanel(true, "點擊地圖中可前進的格子或返回上階段");
        //foreach(var cell in cells)
        //{
        //    Cell _cellScript = cell.GetComponent<Cell>();
        //    if (Mathf.Abs(cell.transform.position.x- selected.transform.position.x)+ 
        //        Mathf.Abs(cell.transform.position.y - selected.transform.position.y)<=_range
        //        &&_cellScript.playerNumber==0)
        //    {
        //        _cellScript.movable = true;
        //        _cellScript.moveCell.SetActive(true);
        //        moveList.Add(cell);
        //    }
        //}
        //本回合探索的格子，此回合結束清空
        List<GameObject> _now = new List<GameObject>();
        //將這回合探索到的棋子加入下回合的_now
        List<GameObject> _open = new List<GameObject>();
        //已經被探索到的格子
        List<GameObject> _closed = new List<GameObject>();
       // Debug.Log(selected.GetComponent<Piece>().cellStandOn.name+" qq");
        _now.Add(selected.GetComponent<Piece>().cellStandOn);
        _closed.Add(selected.GetComponent<Piece>().cellStandOn);
        int _range = selected.GetComponent<Piece>().moveRange;
        for (int i = 0; i < _range; i++)
        {
            foreach (var current in _now)
            {
                //Debug.Log(current.name);
                //if(!_closed.Contains(current))
                //_closed.Add(current);
                List<GameObject> _neighbors = current.GetComponent<Cell>().GetNeighbor();
                foreach (var _nei in _neighbors)
                {
                    
                    if (_nei.GetComponent<Cell>().playerOnCell != Cell.PlayerOnCell.NoPlayer || _closed.Contains(_nei))
                    {
                     //   Debug.Log(_nei.name);
                        continue;
                    }
                    if (!_closed.Contains(_nei))
                    {
                        _open.Add(_nei);
                        _nei.GetComponent<Cell>().movable = true;
                        _nei.GetComponent<Cell>().moveCell.SetActive(true);
                        moveList.Add(_nei);
                        _closed.Add(_nei);
                    }
                }
            }
            _now.Clear();

            foreach (var _item in _open)
            {
                _now.Add(_item);
            }
            _open.Clear();
        }

    }
    public void CloseMoveRange()
    {
        foreach (var _cell in moveList)
        {
            _cell.GetComponent<Cell>().movable = false;
            _cell.GetComponent<Cell>().moveCell.SetActive(false);
        }
        moveList.Clear();
    }
    public void ShowAttackRange()
    {
        
        Debug.Log("Attack");
        CloseAllRange();
        UIManager.instance.SetSkillCastHintPanel(true, "選擇想攻擊的對象");
        int _range = selected.GetComponent<Piece>().attackRange;
        foreach (var _cell in cells)
        {
            Cell _cellscript = _cell.GetComponent<Cell>();
            if (Mathf.Abs(_cell.transform.position.x - selected.transform.position.x) +
                Mathf.Abs(_cell.transform.position.y - selected.transform.position.y) <= _range
                )
          
            {
               
                _cellscript.attackCell.SetActive(true);
                attackList.Add(_cell);
                if (_cellscript.playerOnCell == Cell.PlayerOnCell.Player1 && selected.GetComponent<Piece>().owner == Piece.Owner.Player2
                  || _cellscript.playerOnCell == Cell.PlayerOnCell.Player2 && selected.GetComponent<Piece>().owner == Piece.Owner.Player1)
                    _cellscript.SetAttackable(true);
            }
        }
        

    }
    public void CloseAttackRange()
    {
        foreach (var _cell in attackList)
        {
            // cell.GetComponent<Cell>().movable = false;
            _cell.GetComponent<Cell>().attackCell.SetActive(false);
            _cell.GetComponent<Cell>().SetAttackable(false);
        }
        attackList.Clear();
    }
    #endregion
    #region -- Skill Function --
    public void ShowEnemyTargetSkillRange(int _skillRange)
    {
       // Debug.Log("Skill");
        CloseAllRange();
        
        foreach (var _cell in cells)
        {
            Cell _cellscript = _cell.GetComponent<Cell>();
            if (Mathf.Abs(_cell.transform.position.x - selected.transform.position.x) +
                Mathf.Abs(_cell.transform.position.y - selected.transform.position.y) <= _skillRange
                )
            // && _cellscript.playernumber !=0)
            {
                //  _cellscript.movable = true;
                _cellscript.skillRangeCell.SetActive(true);
                skillRangeList.Add(_cell);
                if (_cellscript.playerOnCell == Cell.PlayerOnCell.Player1 && selected.GetComponent<Piece>().owner == Piece.Owner.Player2
                  || _cellscript.playerOnCell == Cell.PlayerOnCell.Player2 && selected.GetComponent<Piece>().owner == Piece.Owner.Player1)
                    _cellscript.SetPieceToSkillTarget(true);
            }
        }


    }
    public void ShowFriendlyTargetSkillRange(int _skillRange)
    {
        Debug.Log("Skill");
        CloseAllRange();

        foreach (var _cell in cells)
        {
            Cell _cellscript = _cell.GetComponent<Cell>();
            if (
                Mathf.Abs(_cell.transform.position.x - selected.transform.position.x) +
                Mathf.Abs(_cell.transform.position.y - selected.transform.position.y) <= _skillRange
                )
          
            {
              
                _cellscript.skillRangeCell.SetActive(true);
                skillRangeList.Add(_cell);
                if (_cellscript.playerOnCell == Cell.PlayerOnCell.Player1 && selected.GetComponent<Piece>().owner == Piece.Owner.Player1
                  || _cellscript.playerOnCell == Cell.PlayerOnCell.Player2 && selected.GetComponent<Piece>().owner == Piece.Owner.Player2)
                    _cellscript.SetPieceToSkillTarget(true);
            }
        }


    }
    public void ShowEnemyInAOERange(int _skillRange)
    {
        CloseAllRange();

        foreach (var _cell in cells)
        {
            Cell _cellscript = _cell.GetComponent<Cell>();
            if (Mathf.Abs(_cell.transform.position.x - selected.transform.position.x) +
                Mathf.Abs(_cell.transform.position.y - selected.transform.position.y) <= _skillRange
                )
            // && _cellscript.playernumber !=0)
            {
                //  _cellscript.movable = true;
                _cellscript.skillRangeCell.SetActive(true);
                skillRangeList.Add(_cell);
                if (
                    (
                    _cellscript.playerOnCell == Cell.PlayerOnCell.Player1 && selected.GetComponent<Piece>().owner == Piece.Owner.Player2
                  || _cellscript.playerOnCell == Cell.PlayerOnCell.Player2 && selected.GetComponent<Piece>().owner == Piece.Owner.Player1
                  )
                  &&!_cellscript.pieceOnCellScript.ScriptDisable
                  )
                {
                    _cellscript.SetPieceToAOETarget(true);
                    PiecesInSkillArea.Add(_cellscript.pieceOnCellScript);
                    
                }
            }
        }
    }
    public void ShowFriendlyInAOERange(int _skillRange)
    {
        CloseAllRange();

        foreach (var _cell in cells)
        {
            Cell _cellscript = _cell.GetComponent<Cell>();
            if (Mathf.Abs(_cell.transform.position.x - selected.transform.position.x) +
                Mathf.Abs(_cell.transform.position.y - selected.transform.position.y) <= _skillRange
                )
            // && _cellscript.playernumber !=0)
            {
                //  _cellscript.movable = true;
                _cellscript.skillRangeCell.SetActive(true);
                skillRangeList.Add(_cell);
                if (
                    (
                    _cellscript.playerOnCell == Cell.PlayerOnCell.Player1 && selected.GetComponent<Piece>().owner == Piece.Owner.Player1
                  || _cellscript.playerOnCell == Cell.PlayerOnCell.Player2 && selected.GetComponent<Piece>().owner == Piece.Owner.Player2
                  )
                  && !_cellscript.pieceOnCellScript.ScriptDisable
                  )
                {
                    _cellscript.SetPieceToAOETarget(true);
                    PiecesInSkillArea.Add(_cellscript.pieceOnCellScript);

                }
            }
        }
    }
    public void CloseSkillRange()
    {
        
        foreach (var _cell in skillRangeList)
        {
            _cell.GetComponent<Cell>().skillRangeCell.SetActive(false);
            _cell.GetComponent<Cell>().SetPieceToSkillTarget(false);
        }
        foreach(var _piece in PiecesInSkillArea)
        {
            _piece.CanBeSkillTargetIcon.SetActive(false);
        }
        skillRangeList.Clear();
        PiecesInSkillArea.Clear();
    }
    #endregion
    #region -- Switch Turn State --
    public void SwitchToSelectState()
    {
        if(turnState == TurnState.MoveState)
        {
            CloseAllRange();
            selected = null;
            turnState = TurnState.SelectState;
        }
    }
    public void SwitchToEndState()
    {
        if(turnState==TurnState.SelectState)
        {//Open
            hasOpen = false;
            selected = null;
            turnState = TurnState.EndState;
            PassSelecteStatePrepare();
            RefreshPieceList();
            UIManager.instance.SetConfirmSelectPanel(false);
        }
        if (turnState == TurnState.MoveState)
        {
            hasOpen = false;
            PassSelecteStatePrepare();
            UIManager.instance.SetSkillCastHintPanel(false, "");
            RefreshPieceList();
            UIManager.instance.SetBackPanel(false);
        }
        if (turnState == TurnState.ActionState)
        {
            MaxAction = 0;
            turnState = TurnState.EndState;
            UIManager.instance.SetPieceRepeatAction(false);
            UIManager.instance.SetSkillCastHintPanel(false, "");
            UIManager.instance.SetConfirmSelectPanel(false);
            UIManager.instance.SetConfirmCastPanel();
            CloseAllRange();
        }

    }
    public void SwitchToMoveState()
    {
        if (turnState == TurnState.SelectState)
        {//Open
            hasOpen = false;
            turnState = TurnState.MoveState;
            PassSelecteStatePrepare();
            RefreshPieceList();
            ShowMoveRange();
        }
    }
    public void SwitchToActionState()
    {
        if (turnState == TurnState.SelectState)
        {//Open
            hasOpen = false;
            turnState = TurnState.ActionState;
            PassSelecteStatePrepare();
            RefreshPieceList();
            UIManager.instance.SetPieceRepeatAction(true);
        }
        else if (turnState == TurnState.MoveState)
        {
            UIManager.instance.SetPieceRepeatAction(true);
            //Debug.Log("WW");
            CloseAllRange();
            turnState = TurnState.ActionState;
            PassMoveStatePrepare();
          

        }
    }
    #endregion
    #region -- Switch State Prepare --
    public void PassSelecteStatePrepare()
    {
        foreach(var _piece in player1Pieces)
        {
            Piece _pieceScript = _piece.GetComponent<Piece>();
            _pieceScript.CanBeSelectIcon.SetActive(false);
            _pieceScript.currentSelectedIcon.SetActive(false);
        }
        foreach (var _piece in player2Pieces)
        {
            Piece _pieceScript = _piece.GetComponent<Piece>();
            _pieceScript.CanBeSelectIcon.SetActive(false);
            _pieceScript.currentSelectedIcon.SetActive(false);
        }
    }
    public void PassMoveStatePrepare()
    {
        UIManager.instance.SetCancelRepeatMovePiece(false);
    }

    #endregion
    public void RefreshPieceList()
    {
        player1Pieces.Clear();
        player2Pieces.Clear();
        allPieces = GameObject.FindGameObjectsWithTag("Piece");
        foreach (var _piece in allPieces)
        {

            Piece _pieceScript = _piece.GetComponent<Piece>();
            if (_pieceScript.owner == Piece.Owner.Player1 && !_pieceScript.ScriptDisable)
                player1Pieces.Add(_piece);
            else if (_pieceScript.owner == Piece.Owner.Player2 && !_pieceScript.ScriptDisable)
                player2Pieces.Add(_piece);

        }
    }
    #region -- Mana --
   void ManaAdd(TurnOwner _turnOwner)
    { int _maxMana = 10;
        int _addManaAmount = 1;
     
        if(_turnOwner == TurnOwner.Player1)
        {
            if (player1Mana + _addManaAmount >= _maxMana)
                player1Mana = _maxMana;
            else
                player1Mana += _addManaAmount;
        }
        else if (_turnOwner == TurnOwner.Player2)
        {
            if (player2Mana + _addManaAmount >= _maxMana)
                player2Mana = _maxMana;
            else
                player2Mana += _addManaAmount;
        }
    }
  public void ManaSpend(TurnOwner _owner,int _spendAmount)
    {
        Debug.Log("Spend " + _spendAmount);
        if (_owner == TurnOwner.Player1)
        {
            if (_spendAmount <= player1Mana)
            {
                player1Mana -= _spendAmount;
            }
            
        }
        else if(_owner == TurnOwner.Player2)
        {
            if (_spendAmount <= player2Mana)
            {
                player2Mana -= _spendAmount;
            }
           
        }
    }
    #endregion
    public void EndTurn()
    {
        CloseAllRange();
        SwitchToEndState();

        selected = null;
        //hasMove = false;
       // hasAttack = false;
      //  selectPieceLock = false;
      //  allPieces = GameObject.FindGameObjectsWithTag("Piece");
        if (turnOwner == TurnOwner.Player1)
        {
            ManaAdd(TurnOwner.Player1);
            turnOwner = TurnOwner.Player2;
            foreach (var _piece in allPieceList)
            {
                Piece _pieceScript = _piece.GetComponent<Piece>();
                _pieceScript.Reset();
            }
        }
        else if (turnOwner == TurnOwner.Player2)
        {
            ManaAdd(TurnOwner.Player2 );
            turnOwner = TurnOwner.Player1;
            foreach (var _piece in allPieceList)
            {
                Piece _pieceScript = _piece.GetComponent<Piece>();
                _pieceScript.Reset();
            }
        }
        turnState = TurnState.SelectState;
        


    }
    
}
