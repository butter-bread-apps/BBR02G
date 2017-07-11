using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMaterialSetter : MonoBehaviour
{

    int playerChoice;
    public Material[] characterMaterials;
    private void OnEnable()
    {
        UpdateSkin();
    }

    public void UpdateSkin()
    {
        if (ProtectedPrefs.HasKey("PlayerChoice")){
            playerChoice = ProtectedPrefs.GetInt("PlayerChoice");
            Debug.Log(playerChoice);
            GetComponent<SkinnedMeshRenderer>().sharedMaterial = characterMaterials[playerChoice];
        }


    }
    public void UpdateSkin(int skin)
    {
        GetComponent<SkinnedMeshRenderer>().sharedMaterial = characterMaterials[skin];

    }
}
