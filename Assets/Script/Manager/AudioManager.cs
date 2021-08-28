using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioClick;
    public AudioSource audioSelect;
    public AudioSource audioAttack;
    public AudioSource audioMove;
    public AudioSource audioCancel;
    public AudioSource audioDig;
    public AudioSource audioMagic1;
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
