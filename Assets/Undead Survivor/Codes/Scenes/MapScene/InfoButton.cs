using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour
{
    public InfoPanel infoPanel;
    public SkillName skillName;
    public ArtifactName artifactName;
    Button _button;
    Button button
    {
        get
        {
            if (_button == null) _button = GetComponent<Button>();
            return _button;
        }
    }

    public void Init(SkillName skillName, ArtifactName artifactName)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            infoPanel.Init(skillName, artifactName);
            infoPanel.gameObject.SetActive(true);
        });
        this.skillName = skillName;
        this.artifactName = artifactName;
    }

    public void Click()
    {
        button.onClick.Invoke();
    }
}