using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hologram : MonoBehaviour
{
    [SerializeField] string mainScene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("ChangeMainScene", 1.5f);
        }
    }
    void ChangeMainScene()
    {
        SceneManager.LoadScene(mainScene);
    }
}
