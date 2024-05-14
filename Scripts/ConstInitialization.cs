
using System;
using System.IO;
using UnityEngine;

namespace Chalk.Initialization
{
    public static class ConstInitialization 
    {
        public static event Action ConstsInitializationEvent = delegate { };

        public const string SerializationPath =
#if UNITY_EDITOR
            "Assets/Consts.json";
#else
            "Consts.json";
#endif
        
        public static void Initialize()
        {
            using(StreamReader reader=new StreamReader(SerializationPath))
            {
                string serializedConsts = reader.ReadToEnd();
                GlobalConsts.Consts consts = JsonUtility.FromJson<GlobalConsts.Consts>(serializedConsts);
                GlobalConsts.Initialize(consts);
            }
        }

#if UNITY_EDITOR
        public static void WriteGlobalConsts(GlobalConsts.Consts consts)
        {
            using(StreamWriter writer=new StreamWriter(SerializationPath, false))
            {
                writer.Write(JsonUtility.ToJson(consts,true));
            }
            Debug.Log("Writing complete");
        }
#endif
    }
}
