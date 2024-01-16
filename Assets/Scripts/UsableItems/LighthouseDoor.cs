using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LighthouseDoor : MonoBehaviour, IUsable
{
    public string name;

    public void Use()
    {
        SceneManager.LoadScene(name);
    }
}
