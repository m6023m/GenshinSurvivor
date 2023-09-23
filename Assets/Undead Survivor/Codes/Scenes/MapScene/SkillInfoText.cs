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
        string name = "";
        if (artifactName != ArtifactName.Artifact_None)
        {
            InitArtifactInfo(artifactName);
        }
        else
        {
            InitSkillInfo(skillName);
        }
    }

    void InitSkillInfo(SkillName skillName)
    {
        SkillData.ParameterWithKey skillParameter = GameManager.instance.skillData.skills[skillName];
        skillImage = GetComponentInChildren<Image>();
        skillNameText = GetComponentInChildren<TextMeshProUGUI>();
        buttonInfo = GetComponentInChildren<InfoButton>();
        Sprite sprite = null;
        string name = "";
        sprite = skillParameter.icon;
        name = "Skill.".AddString(skillName.ToString()).Localize();

        skillImage.sprite = sprite;
        skillNameText.text = name;
        buttonInfo.Init(skillName, ArtifactName.Artifact_None);

    }
    void InitArtifactInfo(ArtifactName artifactName)
    {

        ArtifactData.ParameterWithKey artifactParameter = GameManager.instance.artifactData.artifacts[artifactName];
        skillImage = GetComponentInChildren<Image>();
        skillNameText = GetComponentInChildren<TextMeshProUGUI>();
        buttonInfo = GetComponentInChildren<InfoButton>();
        Sprite sprite = null;
        string name = "";
        sprite = artifactParameter.icon;
        name = "Artifact.".AddString(artifactName.ToString()).Localize();

        skillImage.sprite = sprite;
        skillNameText.text = name;
        buttonInfo.Init(SkillName.Skill_None, artifactName);
    }
}