using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActiveHole : MonoBehaviour
{

    public int hole_Number;
    public int par_Value;
    public bool active_Hole = false;

    public TMP_Text hole_Number_TMP;
    public TMP_Text par_Number_TMP;
    public TMP_Text putt_Count_TMP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHoleInfo()
    {
        active_Hole = true;
        if (active_Hole)
        {
            hole_Number_TMP.text = hole_Number.ToString();
            par_Number_TMP.text = par_Value.ToString();
        }
    }
}
