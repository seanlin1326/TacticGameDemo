using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Piece Data", menuName = "PieceData")]
public class PieceData :ScriptableObject
{
    public List<Skill> skills;
    public string pieceName;
    
    public int attackPower;
    public int MaxHealth;
    public int armor;

    public int moveRange;
    public int maxMoveTime=1;
    public int attackRange;
    public int maxAttackTime=1;
    public Sprite pieceSprite;

}
