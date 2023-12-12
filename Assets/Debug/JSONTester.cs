using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Utility.Scripts;

#pragma warning disable 0414
public class JSONTester : MonoBehaviour
{
    [SerializeField] private Tester tester;
    
    [ContextMenu("TestJSON")]
    private void TestJSON()
    {
        tester.typeReference.SetTargetType(typeof(RendererMaterialModificationComponent));
        var json = JsonUtility.ToJson(tester, true);
        Debug.Log(json);
        var fromJson = JsonUtility.FromJson<Tester>(json);
    }

    [Serializable]
    private class Tester
    {
        public SaveData SaveData;
        [SerializeField] public TypeReference typeReference;
        [SerializeField] public SignedFloatSelector signedFloatSelector;
        [SerializeField] public int number = 5;
        [SerializeField] private string hello = "hello!";
        [SerializeField] public BabyTester babyTester;
        [SerializeField] public List<BabyTester> numbers = new() {new BabyTester(), new BabyTester()};
    }

    [Serializable]
    private class BabyTester
    {
        [SerializeField] private int babyNumber = 6;
        [SerializeField] public float floatyNumber = 5.5f;
        [SerializeField] private string ohNo = "oh no...";
    }
}
#pragma warning restore 0414