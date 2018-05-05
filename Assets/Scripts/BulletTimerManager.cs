﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.PostProcessing;

public class BulletTimerManager : MonoBehaviour
{

    public List<PlayerController> allPlayers;
    public float distance;

    public PostProcessingBehaviour myCamProf;
    public PostProcessingProfile slowTimeSO;

    private bool slowTime;
    private float timer;

    private ChromaticAberrationModel.Settings chromaticSettings;

    private void Start()
    {
        myCamProf = Camera.main.GetComponent<PostProcessingBehaviour>();
        chromaticSettings = slowTimeSO.chromaticAberration.settings;
    }

    void Update()
    {
        var stunnedPlayers = allPlayers.Where(x => x.stunned).Where(x => x.impactSpeed > 25);
        var anyStunned = allPlayers.Where(x => !x.stunned).Any(x => stunnedPlayers.Any(y => Vector3.Distance(y.transform.position, x.transform.position) < distance));
        slowTime = anyStunned;

        if (slowTime)
            SlowTime();
        else
            NormalTime();
    }

    void SlowTime()
    {
        timer += Time.deltaTime * 4;
        if (timer > 1)
            timer = 1;
        chromaticSettings.intensity = Mathf.Lerp(0, 0.5f, timer);
        slowTimeSO.chromaticAberration.settings = chromaticSettings;
        myCamProf.profile = slowTimeSO;
        Time.timeScale = 0.2f;
    }

    void NormalTime()
    {
        timer = 0;
        chromaticSettings.intensity = 0;
        slowTimeSO.chromaticAberration.settings = chromaticSettings;
        myCamProf.profile = slowTimeSO;
        Time.timeScale = 1;
    }
}
