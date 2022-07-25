using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeteksiGambar : MonoBehaviour
{
    public string nameTag;
    public AudioClip audioBenar;
    public AudioClip audioSalah;
    private AudioSource MediaPlayerBenar;
    private AudioSource MediaPlayerSalah;
    public GameObject benar;
    public GameObject salah;
    public GameObject Next;
    public GameObject Trivia;
    // public GameObject pilihan1;
    // public GameObject pilihan2;
    // public GameObject pilihan3;
    // Start is called before the first frame update
    void Start()
    {
        MediaPlayerBenar = gameObject.AddComponent<AudioSource>();
        MediaPlayerBenar.clip = audioBenar;
        MediaPlayerSalah = gameObject.AddComponent<AudioSource>();
        MediaPlayerSalah.clip = audioSalah;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(nameTag))
        {
            Destroy(collision.gameObject);
            MediaPlayerBenar.Play();
            benar.SetActive(true);
            Next.SetActive(true);
            Trivia.SetActive(true);
            Data.score += 10;
            // pilihan1.SetActive(false);
            // pilihan2.SetActive(false);
            // pilihan3.SetActive(false);
        }
        else
        {
            Destroy(collision.gameObject);
            MediaPlayerSalah.Play();
            salah.SetActive(true);
            Data.score -= 5;
        }
    }
}
