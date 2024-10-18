using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScene : MonoBehaviour
{
    // Load Scene on trigger
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(2);
    }
}
