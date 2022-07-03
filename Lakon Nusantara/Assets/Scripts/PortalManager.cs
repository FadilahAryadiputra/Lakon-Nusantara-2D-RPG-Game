using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PortalManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButtonDown(0))
         {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "PortJateng")
                {
                    SceneManager.LoadScene("SplashJateng");
                }
            }
            
         }
    }
}
