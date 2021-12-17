using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class permauı : MonoBehaviour
{
    public int cherries = 0;
    public int health = 5;
    public TextMeshProUGUI cherryText;
    public Text healthAmount;

    public static permauı perm;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        //singleton
        if (!perm)
        {
            perm = this;
        }
        else
            Destroy(gameObject);
    }

    public void Reset()
    {
        cherries = 0;
        cherryText.text = cherries.ToString();
    }
}
