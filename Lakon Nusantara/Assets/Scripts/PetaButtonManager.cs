using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetaButtonManager : MonoBehaviour
{
    public GameObject areaDescPanelJateng;
    public GameObject areaDescPanelPapua;
    public GameObject areaDescPanelKalimantan;

    //Jateng
    public void OpenAreaDescPanelJateng() {
        areaDescPanelJateng.gameObject.SetActive(true);
    }

    public void CloseAreaDescPanelJateng() {
        areaDescPanelJateng.gameObject.SetActive(false);
    }

    //Papua
    public void OpenAreaDescPanelPapua() {
        areaDescPanelPapua.gameObject.SetActive(true);
    }

    public void CloseAreaDescPanelPapua() {
        areaDescPanelPapua.gameObject.SetActive(false);
    }

    //Kalimantan
    public void OpenAreaDescPanelKalimantan() {
        areaDescPanelKalimantan.gameObject.SetActive(true);
    }

    public void CloseAreaDescPanelKalimantan() {
        areaDescPanelKalimantan.gameObject.SetActive(false);
    }
}
