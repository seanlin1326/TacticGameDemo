using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PieceUI : MonoBehaviour
{
    public Text attackText;
    public Text healthText;
    public Piece pieceScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPieceUI()
    {
        attackText.text = pieceScript.attackPower.ToString();
        healthText.text = pieceScript.health.ToString();
    }
}
