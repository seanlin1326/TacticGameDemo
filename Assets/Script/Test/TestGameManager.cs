using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    public List<TestBaseSkill> testSkills;
    public static TestGameManager instance;
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
}
