using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadingController : MonoBehaviour
{
    [SerializeField]
    Image bar;

    float timer;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer < 45.0f)
            bar.fillAmount = timer/60.0f;

        
    }
}
