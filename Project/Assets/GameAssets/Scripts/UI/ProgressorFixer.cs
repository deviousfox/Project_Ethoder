using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressorFixer : MonoBehaviour
{
    public TMP_Text meshPro;
    

    private void Start()
    {
        meshPro = meshPro ?? GetComponent<TMP_Text>();
    }
}
