using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveScene : MonoBehaviour
{
   void OnEnable()
   {
    SceneManager.LoadScene("OpenScene", LoadSceneMode.Single);
   }
}
