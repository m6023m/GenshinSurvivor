using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillInfoText : MonoBehaviour
{
    Image skillImage;
    TextMeshProUGUI skillNameText;
    InfoButton buttonInfo;

    public void Init(SkillName skillName, ArtifactName artifactName)
    {
        SkillData.ParameterWithKey skillParameter = GameManager.instance.skillData.skillsDictionary[skillName];
        ArtifactData.ParameterWithKey artifactParameter = GameManager.instance.artifactData.Get(artifactName);
        skillImage = GetComponentInChildren<Image>();
        skillNameText = GetComponentInChildren<TextMeshProUGUI>();
        buttonInfo = GetComponentInChildren<InfoButton>();
        Sprite sprite = null;
        string name = "";
        if (artifactName != ArtifactName.Artifact_None)
        {
            sprite = artifactParameter.icon;
            name = "Artifact.".AddString(artifactName.ToString()).Localize();
        }
        else
        {
            sprite = skillParameter.icon;
            name = "Skill.".AddString(skillName.ToString()).Localize();
        }

        skillImage.sprite = sprite;
        skillNameText.text = name;
        buttonInfo.Init(skillName, artifactName);
    }
}