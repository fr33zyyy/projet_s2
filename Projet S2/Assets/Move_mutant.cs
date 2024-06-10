using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouvement : MonoBehaviour
{
    
    public Animator animator;
    
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("IsMooving", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
