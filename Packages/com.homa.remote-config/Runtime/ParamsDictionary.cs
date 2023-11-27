using System;
using Homa.RemoteConfig.Extensions;

/// <summary>
/// A Serializable dictionary with (key, value) types as (string, string);
/// </summary>

[Serializable]
public class ParamsDictionary : SerializableDictionary<string, string>
{
    public void ToDictionary()
    {
        
    }
}

public class ParamsDictionaryDynamic : SerializableDictionary<string, JellyBean> { }

//A Jellybean wrapper to allow Remote Config Params to have different types, and applies boxing/unboxing magically for you. 
public class JellyBean
{
    public JellyBean() //todo
    {
        //did not have time to finish this feature, and didnt prioritize it since not in requirements. 
    }
    
}