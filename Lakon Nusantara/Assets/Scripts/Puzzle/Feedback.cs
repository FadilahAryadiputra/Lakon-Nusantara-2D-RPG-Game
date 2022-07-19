using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    public GameObject mission;
    public GameObject Trivia;
    bool selesai = false;
    public AudioSource win;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void cek()
    {
        for (int i = 0; i < 9; i++)
        {
            if(transform.GetChild(i).GetComponent<Drag>().on_tempel)
            {
                selesai = true;
            }
            else{
                selesai = false;
                i = 9;
            }
        }
        if (selesai) 
        {
            mission.SetActive(true);
            win.Play();
            Trivia.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!selesai)
        {
            cek ();
        }
    }
}
