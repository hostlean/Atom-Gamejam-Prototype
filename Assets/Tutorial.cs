using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject dialogue;
    [SerializeField] private float waitTime = 2.0f;
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        
    }

    public void UseDialogue()
    {
        StartCoroutine(ActivateDialogue());
    }


    IEnumerator ActivateDialogue()
    {
        dialogue.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        dialogue.SetActive(false);
    }

}
