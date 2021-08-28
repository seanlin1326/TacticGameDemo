
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public enum SkillType
    {
        OnePieceTargetBaseSkill,
        DefaultRangeBaseSkill,
        Other
    }
    
    public string SkillName;
    public int manaCost=1;
    [TextArea(3, 5)]
    public string skillIntro;
    [TextArea(3, 5)]
    public string skillCastHint;
    // public SkillType skillType = SkillType.OnePieceTargetBaseSkill;
    public abstract void Execute();
    
}
