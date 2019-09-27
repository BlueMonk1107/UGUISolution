using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TextureSetting : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        Debug.Log("OnPreprocessTexture");
        TextureImporter importer = (TextureImporter) assetImporter;
        importer.textureType = TextureImporterType.Sprite;
    }
}
