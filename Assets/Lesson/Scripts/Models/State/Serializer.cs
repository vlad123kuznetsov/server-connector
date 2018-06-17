using UnityEngine;

namespace Models.State
{
    public class Serializer
    {
        public T Load<T>(string key) where T : class
        {
            var str = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<T>(str);
        }

        public void Save<T>(string key, T data) where T : class 
        {
            var json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }
    }
}