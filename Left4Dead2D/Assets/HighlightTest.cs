using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightTest : MonoBehaviour
{
    
    void OnMouseEnter()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
