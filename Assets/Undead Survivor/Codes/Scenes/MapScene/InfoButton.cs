using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour
{
    public InfoPanel infoPanel;
    public SkillName skillName;
    public ArtifactName artifactName;

    public void Init(SkillName skillName, ArtifactName artifactName)
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(() =>
        { 
            infoPanel.Init(skillName, artifactName);
            infoPanel.gameObject.SetActive(true);
        });
        this.skillName = skillName;
        this.artifactName = artifactName;
    }
}