using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[ExecuteInEditMode]
public class WriteBakeTexture : MonoBehaviour {

    private Texture2D result_tex;
    public Material mat_EdgeFixed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ByteWrite(string _path, byte[] _bytes)
    {
        File.WriteAllBytes(_path, _bytes);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat_EdgeFixed, 0);
        if (this.result_tex == null)
        {
            result_tex = new Texture2D(Screen.width, Screen.height);
            result_tex.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
            ByteWrite(Application.dataPath + "/Textures/321" + ".png", result_tex.EncodeToPNG());
            Debug.Log("Well done...");
            AssetDatabase.Refresh();
        }

    }
}
