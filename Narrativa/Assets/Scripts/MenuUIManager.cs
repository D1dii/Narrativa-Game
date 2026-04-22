using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuUIManager : MonoBehaviour
{
    
    public void LoadFirstScene()
    {
        SceneManager.LoadScene("InitialScene");
    }

}
