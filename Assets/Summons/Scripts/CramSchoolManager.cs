using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CramSchoolManager : Singleton<CramSchoolManager>
{
    // Start is called before the first frame update
    [SerializeField] private GameObject BallGameNPC;
    [SerializeField] private Button BallGameNPCSign;

    private bool BallGameFlag;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowBallGameNPCSign(bool status)
    {
        BallGameNPCSign.gameObject.SetActive(status);
    }

    public void StartBallGame()
    {
        BallGameManager.Instance.ShowBallGamePanel(true);
        ShowBallGameNPCSign(false);
    }

}
