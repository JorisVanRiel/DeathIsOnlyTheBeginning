using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class SkillPanel : MonoBehaviour
{
    [SerializeField] CharacterSheet characterSheet;
    public TMPro.TextMeshProUGUI availableXpText;
    public TMPro.TextMeshProUGUI attackText;
    public TMPro.TextMeshProUGUI defenceText;


    private void Start()
    {
        availableXpText.text = $"{characterSheet.ExperiencePoints}";
        attackText.text = $"{characterSheet.Attack}";
        defenceText.text = $"{characterSheet.Defence}";
    }

    private void Update()
    {
        availableXpText.text = $"{characterSheet.ExperiencePoints}";
        attackText.text = $"{characterSheet.Attack}";
        defenceText.text = $"{characterSheet.Defence}";
    }
}
