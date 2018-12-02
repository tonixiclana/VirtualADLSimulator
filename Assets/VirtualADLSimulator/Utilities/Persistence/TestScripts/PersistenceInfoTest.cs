using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PersistenceInfoTest{

    [JsonProperty("serializableComponents")]
    public List<object> serializableComponents = new List<object>();

}
