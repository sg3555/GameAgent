﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeStage : MonoBehaviour
{
    public void ChangeScene(string scnName)
    {
        SceneManager.LoadScene(scnName);
    }
}
