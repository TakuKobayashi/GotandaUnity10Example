using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using VRM;

public class LoadVRMSample : MonoBehaviour
{
    [SerializeField] private string vrmurl;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(downloadVRM(vrmurl));
    }

    private IEnumerator downloadVRM(string url){
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();
        LoadVRM(webRequest.downloadHandler.data, (vrm) => {
            
        });
    }

    private void LoadVRM(byte[] vrmFileBinary, Action<GameObject> onLoaded){
        VRMImporterContext context = new VRMImporterContext();
        context.ParseGlb(vrmFileBinary);
        context.LoadAsync((unit) =>
        {
            context.ShowMeshes();
            onLoaded(context.Root);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
