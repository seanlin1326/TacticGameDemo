using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
   public enum PlayerOnCell
    {
        Player1,
        Player2,
        NoPlayer
    }
    public PlayerOnCell playerOnCell=PlayerOnCell.NoPlayer;
    public bool movable;
    public GameObject moveCell;
    public GameObject attackCell;
    public GameObject skillRangeCell;

    public Piece pieceOnCellScript;

    //public int GCost;
    //public int HCost;
    //public int FCost;
    //public GameObject parentNode;
    //public PathDisplayer pathDisplayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {

        // Debug.Log("cell");
        Debug.Log("this is a " + this.gameObject.tag);
        // GetNeighbor();
        if (movable)
        {
            
            UIManager.instance.SetBackPanel(false);
            AudioManager.instance.audioMove.Play();
            GameManager.instance.selected.GetComponent<Piece>().Move(transform.position.x, transform.position.y);
            GameManager.instance.MaxMove--;
            if (GameManager.instance.MaxMove <= 0)
                GameManager.instance.SwitchToActionState();
            else
            {
                UIManager.instance.SetCancelRepeatMovePiece(true);
                UIManager.instance.SetBackPanel(false);

                //GameManager.instance.ShowMoveRange();
            }
        }

    }
    public List<GameObject> GetNeighbor()
    {
      //  Debug.Log("Start");
        List<GameObject> _neighbors = new List<GameObject>();
        Vector2 _rayPoint = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(_rayPoint + Vector2.up, Vector2.up);
        if (hit.collider != null && hit.collider.CompareTag("Cell"))
        {
         //   Debug.Log(hit.collider.transform.position.x + " " + hit.collider.transform.position.y+" "+ hit.collider.gameObject.name);
            _neighbors.Add(hit.collider.gameObject);
        }

        _rayPoint = new Vector2(transform.position.x, transform.position.y);
        hit = Physics2D.Raycast(_rayPoint + Vector2.right, Vector2.right);
        if (hit.collider != null && hit.collider.CompareTag("Cell"))
        {
          //   Debug.Log(hit.collider.transform.position.x + " " + hit.collider.transform.position.y + " " + hit.collider.gameObject.name);
            _neighbors.Add(hit.collider.gameObject);
        }

        _rayPoint = new Vector2(transform.position.x, transform.position.y);
        hit = Physics2D.Raycast(_rayPoint + Vector2.left, Vector2.left);
        if (hit.collider != null && hit.collider.CompareTag("Cell"))
        {
         //    Debug.Log(hit.collider.transform.position.x + " " + hit.collider.transform.position.y + " " + hit.collider.gameObject.name);
            _neighbors.Add(hit.collider.gameObject);
        }

        _rayPoint = new Vector2(transform.position.x, transform.position.y);
        hit = Physics2D.Raycast(_rayPoint + Vector2.down, Vector2.down);
        if (hit.collider != null && hit.collider.CompareTag("Cell"))
        {
          //   Debug.Log(hit.collider.transform.position.x + " " + hit.collider.transform.position.y + " " + hit.collider.gameObject.name);
            _neighbors.Add(hit.collider.gameObject);
        }
        return _neighbors;
    }

 
    public void SetAttackable(bool _attackable)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, new Vector3(0, 0, -1));
        foreach (var hit in hits)
        {
            if (hit.collider.tag == "Piece" && !hit.collider.GetComponent<Piece>().ScriptDisable)
            {

                hit.collider.GetComponent<Piece>().beingAttackable = _attackable;
                hit.collider.GetComponent<Piece>().CanBeAttackIcon.SetActive(_attackable);
            }
        }
    }

    public void SetPieceToSkillTarget(bool _skillable)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, new Vector3(0, 0, -1));
        foreach (var hit in hits)
        {
            if (hit.collider.tag == "Piece" && !hit.collider.GetComponent<Piece>().ScriptDisable)
            {

                hit.collider.GetComponent<Piece>().beingSkillable = _skillable;
                hit.collider.GetComponent<Piece>().CanBeSkillTargetIcon.SetActive(_skillable);
            }
        }
    }
    public void SetPieceToAOETarget(bool _skillable)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, new Vector3(0, 0, -1));
        foreach (var hit in hits)
        {
            if (hit.collider.tag == "Piece" && !hit.collider.GetComponent<Piece>().ScriptDisable)
            {

           
                hit.collider.GetComponent<Piece>().CanBeSkillTargetIcon.SetActive(_skillable);
            }
        }
    }
}
