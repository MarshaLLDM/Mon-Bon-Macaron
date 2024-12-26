using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCallback : MonoBehaviour
{
    // Start is called before the first frame update
   private bool _isFirstUpdate = true;

    private void Update()
    {
        if (_isFirstUpdate)
        {
            _isFirstUpdate = false;

            Loading.LoadingCallback();
        }
    }
}
