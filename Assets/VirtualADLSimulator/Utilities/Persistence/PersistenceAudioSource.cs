using UnityEngine;
using System.Collections;

public class PersistenceAudioSource : PersistenceComponent<PersistenceAudioSource>, IPersistenceComponent<PersistenceAudioSource>
{
    public bool enable;
    public string audioClip;
    public bool playOnAwake;

    public void addComponentInGameobject(GameObject gm)
    {
        gm.AddComponent<AudioSource>().enabled = enable;
        gm.GetComponent<AudioSource>().clip = PersistenceGameobject.findObjectInResources<AudioClip>(audioClip, "Audio");
        gm.GetComponent<AudioSource>().playOnAwake = playOnAwake;

    }

    public PersistenceAudioSource loadComponentInfo(GameObject gm)
    {
        enable = gm.GetComponent<AudioSource>().enabled;

        if(gm.GetComponent<AudioSource>().clip != null)
            audioClip = gm.GetComponent<AudioSource>().clip.name;

        playOnAwake = gm.GetComponent<AudioSource>().playOnAwake;




        return this;
    }


}
