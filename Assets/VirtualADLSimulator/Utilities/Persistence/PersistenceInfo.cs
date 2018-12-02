using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

[System.Serializable]
public class PersistenceInfo
{
    public int id = -1;
    public string name;

    [JsonProperty("serializableComponents")]
    public List<object> serializableComponents = new List<object>();

}
