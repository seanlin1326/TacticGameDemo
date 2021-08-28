using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    public enum PieceState
    {
        FaceOn,
        FaceDown
    }
    public PieceState pieceState=PieceState.FaceDown;
    public Piece pieceScript;
    public HidePiece hidePieceScript;
    // Start is called before the first frame update
    void Start()
    {
        pieceState = PieceState.FaceDown;
        hidePieceScript.ScriptDisable = false;
        pieceScript.ScriptDisable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void SwitchToFaceOn()
    {
        AudioManager.instance.audioDig.Play();
        pieceState = PieceState.FaceOn;
        if (!GameManager.instance.allOpenPieceList.Contains(gameObject))
            GameManager.instance.allOpenPieceList.Add(gameObject);
        if (GameManager.instance.allHidePieceList.Contains(gameObject))
            GameManager.instance.allHidePieceList.Remove(gameObject);
        pieceScript.ScriptDisable = false;
        hidePieceScript.ScriptDisable = true; 
    }
}
