using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UMP_Options : MonoBehaviour
{

    public Animation Anim;
    public string OptionAnimation;
    //ChangeName
    public Animation AnimCN;
    public string CNAnimation;
    [Space(7)]
    public Text PlayerNameText = null;

    public Text QualityText = null;
    private int CurrentQuality = 0;

    public Text AntiStropicText = null;
    private int CurrentAS = 0;

    public Text AntiAliasingText = null;
    private int CurrentAA = 0;
    private string[] AAOptions = new string[] { "X0","X2","X4","X8"};

    public Text vSyncText = null;
    private int CurrentVSC = 0;
    private string[] VSCOptions = new string[] { "Don't Sync", "Every V Blank", "Every Second V Blank" };

    public Text blendWeightsText = null;
    private int CurrentBW = 0;

    public Text ResolutionText;
    private int CurrentRS = 0;

    public Slider VolumenSlider = null;

    public bool SaveFullcreen = true;
    private bool m_FullScreen = false;
    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        GetInfoOptions();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mas"></param>
    public void GameQuality(bool mas)
    {
        if (mas)
        {
            CurrentQuality = (CurrentQuality + 1) % QualitySettings.names.Length;
        }
        else
        {
            if (CurrentQuality != 0)
            {
                CurrentQuality = (CurrentQuality - 1) % QualitySettings.names.Length;
            }
            else
            {
                CurrentQuality = (QualitySettings.names.Length - 1);
            }
        }
        QualityText.text = QualitySettings.names[CurrentQuality];
        QualitySettings.SetQualityLevel(CurrentQuality);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="b"></param>
    public void AntiStropic(bool b)
    {
        if (b) { CurrentAS = (CurrentAS + 1) % 3; } else { if (CurrentAS != 0) { CurrentAS = (CurrentAS - 1) % 3; } else { CurrentAS = 2; } }

        switch (CurrentAS)
        {
            case 0 :
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
                AntiStropicText.text = AnisotropicFiltering.Disable.ToString();
                break;
            case 1:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
                AntiStropicText.text = AnisotropicFiltering.Enable.ToString();
                break;
            case 2:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                AntiStropicText.text = AnisotropicFiltering.ForceEnable.ToString();
                break;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="b"></param>
    public void AntiAliasing(bool b)
    {
        CurrentAA = (b) ? (CurrentAA + 1) % 4 : (CurrentAA != 0) ? (CurrentAA - 1) % 4 : CurrentAA = 3;
        AntiAliasingText.text = AAOptions[CurrentAA];
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
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="b"></param>
    public void VSyncCount(bool b)
    {
        CurrentVSC = (b) ? (CurrentVSC + 1) % 3 : (CurrentVSC != 0) ? (CurrentVSC - 1) % 3 : CurrentVSC = 2;
        vSyncText.text = VSCOptions[CurrentVSC];
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
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="b"></param>
    public void blendWeights(bool b)
    {
        CurrentBW = (b) ? (CurrentBW + 1) % 3 : (CurrentBW != 0) ? (CurrentBW - 1) % 3 : CurrentBW = 2;
        switch (CurrentBW)
        {
            case 0:
                QualitySettings.blendWeights = BlendWeights.OneBone;
                blendWeightsText.text = BlendWeights.OneBone.ToString();
                break;
            case 1:
                QualitySettings.blendWeights = BlendWeights.TwoBones;
                blendWeightsText.text = BlendWeights.TwoBones.ToString();
                break;
            case 2:
                QualitySettings.blendWeights = BlendWeights.FourBones;
                blendWeightsText.text = BlendWeights.FourBones.ToString();
                break;
        }
    }

    /// <summary>
    /// Change resolution of screen
    /// NOTE: this work only in build game, this not work in
    /// Unity Editor.
    /// </summary>
    /// <param name="b"></param>
    public void Resolution(bool b)
    {
        CurrentRS = (b) ? (CurrentRS + 1) % Screen.resolutions.Length : (CurrentRS != 0) ? (CurrentRS - 1) % Screen.resolutions.Length : CurrentRS = (Screen.resolutions.Length - 1);
        ResolutionText.text = Screen.resolutions[CurrentRS].width + " X " + Screen.resolutions[CurrentRS].height;
    }
    public void FullScreen(bool f) { m_FullScreen = f; }
    /// <summary>
    /// Change volumen
    /// </summary>
    /// <param name="v">volumen</param>
    public void Volumen(float v) { AudioListener.volume = v; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="forward"></param>
    bool mopen = false;
    public void WndowAnimation(bool forward)
    {
        if (forward)
        {
            if (forward == mopen)
                return;

            Anim[OptionAnimation].time = 0.0f;
            Anim[OptionAnimation].speed = 1.0f;
            Anim.CrossFade(OptionAnimation, 0.2f);
        }
        else
        {
            if (Anim[OptionAnimation].time == 0.0f)
            {
                Anim[OptionAnimation].time = Anim[OptionAnimation].length;
            }
            Anim[OptionAnimation].speed = -1.0f;
            Anim.CrossFade(OptionAnimation, 0.2f);
        }
        mopen = forward;
    }

    /// <summary>
    /// 
    /// </summary>
    bool mopencn = false;
    bool cnopem = false;
    public void CNAnimationWindow()
    {
        cnopem = !cnopem;
        if (cnopem)
        {
            if (cnopem == mopencn)
                return;

            if (!AnimCN.gameObject.activeSelf) { AnimCN.gameObject.SetActive(true); }

            AnimCN[CNAnimation].time = 0.0f;
            AnimCN[CNAnimation].speed = 1.0f;
            AnimCN.CrossFade(CNAnimation, 0.2f);
        }
        else
        {
            if (AnimCN[CNAnimation].time == 0.0f)
            {
                AnimCN[CNAnimation].time = AnimCN[CNAnimation].length;
            }
            AnimCN[CNAnimation].speed = -1.0f;
            AnimCN.CrossFade(CNAnimation, 0.2f);
        }
        mopencn = cnopem;
    }
    /// <summary>
    /// Saved Options
    /// </summary>
    public void Apply()
    {
        PlayerPrefs.SetInt(UMPKeys.Quality, CurrentQuality);
        PlayerPrefs.SetInt(UMPKeys.AnisoStropic, CurrentAS);
        PlayerPrefs.SetInt(UMPKeys.AntiAliasing, CurrentAA);
        PlayerPrefs.SetInt(UMPKeys.VSync, CurrentVSC);
        PlayerPrefs.SetInt(UMPKeys.BlendWeight, CurrentBW);
        PlayerPrefs.SetInt(UMPKeys.Resolution, CurrentRS);
        PlayerPrefs.SetFloat(UMPKeys.Volumen, AudioListener.volume);
        if (SaveFullcreen)
        {
            int f = (m_FullScreen) ? 1 : 0;
            PlayerPrefs.SetInt(UMPKeys.FullScreen, f);
        }

        Screen.SetResolution(Screen.resolutions[CurrentRS].width, Screen.resolutions[CurrentRS].height,false);
        Screen.fullScreen = m_FullScreen;
    }

    public void ChangeName(InputField field) { if (field == null || PlayerNameText == null) return; PlayerNameText.text = field.text; field.text = string.Empty; CNAnimationWindow(); }
    /// <summary>
    /// Get saved info
    /// </summary>
    void GetInfoOptions()
    {
        if (PlayerPrefs.HasKey(UMPKeys.Quality)) { CurrentQuality = PlayerPrefs.GetInt(UMPKeys.Quality); } else { CurrentQuality = QualitySettings.GetQualityLevel(); }
        if (PlayerPrefs.HasKey(UMPKeys.AnisoStropic)) { CurrentAS = PlayerPrefs.GetInt(UMPKeys.AnisoStropic); }
        if (PlayerPrefs.HasKey(UMPKeys.AntiAliasing)) { CurrentAA = PlayerPrefs.GetInt(UMPKeys.AntiAliasing); }
        if (PlayerPrefs.HasKey(UMPKeys.BlendWeight)) { CurrentBW = PlayerPrefs.GetInt(UMPKeys.BlendWeight); }
        if (PlayerPrefs.HasKey(UMPKeys.VSync)) { CurrentVSC = PlayerPrefs.GetInt(UMPKeys.VSync); }
        if (PlayerPrefs.HasKey(UMPKeys.Resolution)) { CurrentRS = PlayerPrefs.GetInt(UMPKeys.Resolution); }
        if (PlayerPrefs.HasKey(UMPKeys.Volumen)) { AudioListener.volume = PlayerPrefs.GetFloat(UMPKeys.Volumen); }
        if (SaveFullcreen && PlayerPrefs.HasKey(UMPKeys.FullScreen))
        {
            bool f = (PlayerPrefs.GetInt(UMPKeys.FullScreen) == 1) ? true : false;
            Screen.fullScreen = f;
        }
        VolumenSlider.value = AudioListener.volume;
        QualityText.text = QualitySettings.names[CurrentQuality];
        //
        switch (CurrentAS)
        {
            case 0:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
                AntiStropicText.text = AnisotropicFiltering.Disable.ToString();
                break;
            case 1:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
                AntiStropicText.text = AnisotropicFiltering.Enable.ToString();
                break;
            case 2:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                AntiStropicText.text = AnisotropicFiltering.ForceEnable.ToString();
                break;
        }
        //
        AntiAliasingText.text = AAOptions[CurrentAA];
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
        vSyncText.text = VSCOptions[CurrentVSC];
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
                blendWeightsText.text = BlendWeights.OneBone.ToString();
                break;
            case 1:
                QualitySettings.blendWeights = BlendWeights.TwoBones;
                blendWeightsText.text = BlendWeights.TwoBones.ToString();
                break;
            case 2:
                QualitySettings.blendWeights = BlendWeights.FourBones;
                blendWeightsText.text = BlendWeights.FourBones.ToString();
                break;
        }
        //
        ResolutionText.text = Screen.resolutions[CurrentRS].width + " X " + Screen.resolutions[CurrentRS].height;
    }
}