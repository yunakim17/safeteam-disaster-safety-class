using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void Main()
    {
        SceneManager.LoadScene("Main");
    }

    public void Eq_Main()
    {
        SceneManager.LoadScene("Eq_Main");
    }

    public void Eq_Step1_S1()
    {
        SceneManager.LoadScene("Eq_Step1_S1");
    }

    public void Eq_Step2_S1()
    {
        SceneManager.LoadScene("Eq_Step2_S1");
    }

    public void Eq_Step3_S1()
    {
        SceneManager.LoadScene("Eq_Step3_S1");
    }

    public void Eq_Step4_S1()
    {
        SceneManager.LoadScene("Eq_Step4_S1");
    }


}
