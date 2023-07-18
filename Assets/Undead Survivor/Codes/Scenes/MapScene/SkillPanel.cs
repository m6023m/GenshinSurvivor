using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillPanel : MonoBehaviour
{
    SkillIcon[] icons;
    private void Awake()
    {
        icons = GetComponentsInChildren<SkillIcon>(true);
    }

    public void AddSkillData(SkillData.ParameterWithKey parameterWithKey, SkillObject skillObject)
    {
        foreach (SkillIcon icon in icons)
        {
            if (!icon.gameObject.activeSelf)
            {
                icon.gameObject.SetActive(true);
                icon.sprite = parameterWithKey.icon;
                icon.skillParameter = parameterWithKey;
                icon.isSkill = true;
                icon.skillObject = skillObject;
                break;
            }
        }

    }
    public void AddArtifactData(ArtifactData.ParameterWithKey parameterWithKey)
    {
        foreach (SkillIcon icon in icons)
        {
            if (!icon.gameObject.activeSelf)
            {
                icon.gameObject.SetActive(true);
                icon.artifactParameter = parameterWithKey;
                icon.isArtifact = true;
                icon.sprite = parameterWithKey.icon;
                break;
            }
        }

    }

}