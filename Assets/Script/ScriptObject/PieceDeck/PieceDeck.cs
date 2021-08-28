using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "PieceDeck")]
public class PieceDeck : ScriptableObject
{
    public List<PieceData> PieceList;
}
