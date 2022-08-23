using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision : MonoBehaviour
{
    public GameObject retry;
    public GameObject done;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DataQuiz.skor <= 30)
        {
            retry.SetActive(true);
        }
        else
        {
            done.SetActive(true);
        }
    }
}
