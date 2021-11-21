using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMeshRenderer : MonoBehaviour
{
   private MeshRenderer[] meshArray;

    private void Awake() {
        meshArray = GetComponentsInChildren<MeshRenderer>();
    }

    public void HideMesh(){
        foreach (var mesh in meshArray){
            mesh.enabled = false;
        }
    }

     public void ShowMesh(){
        foreach (var mesh in meshArray){
            mesh.enabled = true;
        }
    }
}
