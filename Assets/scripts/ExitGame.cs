﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitGame : MonoBehaviour
{
    public void Exit_Game()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
