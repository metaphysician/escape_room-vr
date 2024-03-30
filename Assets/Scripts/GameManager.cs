using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _renderers;
    [SerializeField] private GameObject[] _pointLights;
    [SerializeField] private GameObject _motherboardLights;

    public enum GameState{Start,CameraGaze,PowerOn,DisketteSearch,MasterDiskette,Finish};
    [SerializeField] private GameState currGameState;

    // Start is called before the first frame update
    void Start()
    {
        PowerCtrl(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PowerCtrl(bool powerIsOn)
    {
        //this only works if the Mesh Renderers have exactly three materials (or less)
        Material[] matList = new Material[3];
        //loop over Mesh Renderers
        int lightCount;
        for(int objCount = 0; objCount < _renderers.Length; objCount++)
        {
            //null check
            if(_renderers[objCount] != null)
            {
                GameObject tempObj = null;
                if(objCount < 3)
                {
                    lightCount = objCount;
                    tempObj = _pointLights[lightCount];
                }

                //loop over list
                for(int matCount = 0; matCount < _renderers[objCount].materials.Length; matCount++)
                {
                    //select material in list
                    Material tempMat = null;
                    //null check for materials list less than 3 
                    if(_renderers[objCount].materials[matCount] != null)
                         tempMat = _renderers[objCount].materials[matCount];
                    if(!powerIsOn)
                    {
                        if(tempMat.IsKeywordEnabled("_EMISSION"))
                            tempMat.DisableKeyword("_EMISSION");
                        if(tempObj != null)
                            tempObj.SetActive(false);
                        _motherboardLights.SetActive(true);
                    }
                    else if(powerIsOn)
                    {
                        if(!tempMat.IsKeywordEnabled("_EMISSION"))
                            tempMat.EnableKeyword("_EMISSION");
                        if(tempObj != null)
                            tempObj.SetActive(true);
                        _motherboardLights.SetActive(false);
                    }
                }
            }    
        }
    }
}
