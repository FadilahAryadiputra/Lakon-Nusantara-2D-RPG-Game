using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feedback : MonoBehaviour
{
    // public GameObject mission;
    public GameObject Trivia;
    public GameObject Selesai;
    public GameObject puzzle;
    // public GameObject pos;
    public bool selesai = false;
    public AudioSource win;

    // public MainMenu papua;
    
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
            puzzle.SetActive(false);
            // pos.SetActive(false);
            // mission.SetActive(true);
            win.Play();
            Trivia.SetActive(true);
            Selesai.SetActive(true);
            // papua.btnPapua.SetActive(true);
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
