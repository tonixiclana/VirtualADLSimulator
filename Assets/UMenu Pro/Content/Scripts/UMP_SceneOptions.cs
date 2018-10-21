using UnityEngine;
using System.Collections;

public class UMP_SceneOptions : MonoBehaviour {

    void Awake()
    {
        GetInfoOptions();
    }

    /// <summary>
    /// Get saved info
    /// </summary>
    void GetInfoOptions()
    {
        int CurrentQuality = 0;
        int CurrentAS = 0;
        int CurrentBW = 0;
        int CurrentAA = 0;
        int CurrentVSC = 0;

        if (PlayerPrefs.HasKey(UMPKeys.Quality)) { CurrentQuality = PlayerPrefs.GetInt(UMPKeys.Quality); } else { CurrentQuality = QualitySettings.GetQualityLevel(); }
        if (PlayerPrefs.HasKey(UMPKeys.AnisoStropic)) { CurrentAS = PlayerPrefs.GetInt(UMPKeys.AnisoStropic); }
        if (PlayerPrefs.HasKey(UMPKeys.AntiAliasing)) { CurrentAA = PlayerPrefs.GetInt(UMPKeys.AntiAliasing); }
        if (PlayerPrefs.HasKey(UMPKeys.BlendWeight)) { CurrentBW = PlayerPrefs.GetInt(UMPKeys.BlendWeight); }
        if (PlayerPrefs.HasKey(UMPKeys.VSync)) { CurrentVSC = PlayerPrefs.GetInt(UMPKeys.VSync); }
        if (PlayerPrefs.HasKey(UMPKeys.Volumen)) { AudioListener.volume = PlayerPrefs.GetFloat(UMPKeys.Volumen); }

        QualitySettings.SetQualityLevel(CurrentQuality);
        switch (CurrentAS)
        {
            case 0:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
                break;
            case 1:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
                break;
            case 2:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                break;
        }
        //
        switch (CurrentAA)
        {
            case 0:
                QualitySettings.antiAliasing = 0;
                break;
            case 1:
                QualitySettings.antiAliasing = 2;
                break;
            case 2:
                QualitySettings.antiAliasing = 4;
                break;
            case 3:
                QualitySettings.antiAliasing = 8;
                break;
        }
        //
        switch (CurrentVSC)
        {
            case 0:
                QualitySettings.vSyncCount = 0;
                break;
            case 1:
                QualitySettings.vSyncCount = 1;
                break;
            case 2:
                QualitySettings.vSyncCount = 2;
                break;
        }
        //
        switch (CurrentBW)
        {
            case 0:
                QualitySettings.blendWeights = BlendWeights.OneBone;
                break;
            case 1:
                QualitySettings.blendWeights = BlendWeights.TwoBones;
                break;
            case 2:
                QualitySettings.blendWeights = BlendWeights.FourBones;
                break;
        }
    }
}