using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PersistenceComponent<T> {

    [JsonProperty("Type")]
    public string typeOfComponent = typeof(T).ToString();

}
