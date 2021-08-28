using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

    }
    public int skillMana=0;
   public event Action<Piece> OnBeingSkillTarget; 

    public void BeingSkillTarget(Piece _piece)
    {
        //Debug.Log("SSSSSKKKKILLLLLL");
        if (OnBeingSkillTarget != null)
            OnBeingSkillTarget(_piece);
    }
    public void CostMana()
    {
        GameManager.instance.ManaSpend(GameManager.instance.turnOwner, skillMana);
        skillMana = 0;
    }
    public void ClearAllEvent()
    {
        
        if (OnBeingSkillTarget != null)
        {
            Delegate[] dels = OnBeingSkillTarget.GetInvocationList();
            foreach(var del in dels)
            {
                OnBeingSkillTarget -= del as Action<Piece>;
            }
        }
    }
}
