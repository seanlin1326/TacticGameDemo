using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowPieceInfoUIController : MonoBehaviour
{
    public static ShowPieceInfoUIController instance;
    public GameObject PieceInfoPanel;
    public GameObject PieceDetailPanel;
    // PieceInfoPanel
    public Text pieceOwnerNameText;
    public Text pieceNameText;
    public Text attackText;
    public Text attackRangeText;
    public Text HealthText;
    public Text moveRangeText;
    public Image piecePortrait;
    //PieceDetailPanel
    public Text pieceOwnerDetail;
    public Text PieceNameDetail;
    public Text attackDetail;
    public Text attackRangeDetail;
    public Text healthDetail;
    public Text moveRangeDetail;

    public Text skill1NameDetail;
    public Text skill1IntroDetail;
    public Text skill2NameDetail;
    public Text skill2IntroDetail;
    public Text skill1ManaCostDetail;
    public Text skill2ManaCostDetail;
    public Image piecePortraitDetail;

    private Piece DisplayPiece; 
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowPieceInfo(Piece _piece)
    {
        DisplayPiece = _piece;
        pieceNameText.text = _piece.pieceName;
        pieceOwnerNameText.text = _piece.owner.ToString();
        attackText.text = _piece.attackPower.ToString();
        attackRangeText.text = _piece.attackRange.ToString();
        HealthText.text = _piece.health.ToString();
        moveRangeText.text = _piece.moveRange.ToString();
        piecePortrait.sprite = _piece.pieceImage;
        if (!PieceInfoPanel.activeSelf)
            PieceInfoPanel.SetActive(true);

    }
    public void ShowOrDisableDetailPanel()
    {
        if (!PieceDetailPanel.activeSelf)
        {
            pieceOwnerDetail.text = DisplayPiece.owner.ToString();
            PieceNameDetail.text = DisplayPiece.pieceName;
            attackDetail.text = DisplayPiece.attackPower.ToString();
            attackRangeDetail.text = DisplayPiece.attackRange.ToString();
            healthDetail.text = DisplayPiece.health + "/" + DisplayPiece.MaxHealth;
            moveRangeDetail.text = DisplayPiece.moveRange.ToString();
            piecePortraitDetail.sprite = DisplayPiece.pieceImage;

            skill1NameDetail.text = DisplayPiece.skills[0].SkillName;
            skill1IntroDetail.text = DisplayPiece.skills[0].skillIntro;
            skill2NameDetail.text = DisplayPiece.skills[1].SkillName;
            skill2IntroDetail.text = DisplayPiece.skills[1].skillIntro;
            skill1ManaCostDetail.text = DisplayPiece.skills[0].manaCost.ToString();
            skill2ManaCostDetail.text = DisplayPiece.skills[1].manaCost.ToString();
            PieceDetailPanel.SetActive(true);
        }
        else
        {
            PieceDetailPanel.SetActive(false);
        }   
    }
}
