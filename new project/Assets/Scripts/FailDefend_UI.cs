using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailDefend_UI : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject button;

    public void ContinueButton()
    {
        background.SetActive(false);
        text.SetActive(false);
        button.SetActive(false);
    }
}
