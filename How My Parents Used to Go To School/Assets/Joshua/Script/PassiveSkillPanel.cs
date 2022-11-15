using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PassiveSkillPanel : MonoBehaviour
{
    public TextMeshProUGUI[] textList;
    public GameObject player;

    private PlayerAbilityManager playerAbilityManager;
    private List<Ability> abilityList =  new List<Ability>();

    // Start is called before the first frame update
    void Start()
    {
        playerAbilityManager = player.GetComponent<PlayerAbilityManager>();

        SetSkill();
        SetSKillName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // randomly generate skills
    private void SetSkill()
    {
        int index;
        List<Ability> tmpList = new List<Ability>();
        tmpList = playerAbilityManager.unactivedAbilityList;
        int stop = 0;

        if(tmpList.Count >= 3)
        {
            stop = 3;
        }
        else if(tmpList.Count > 0 && tmpList.Count < 3)
        {
            stop = tmpList.Count;
        }

        for (int i = 0; i < stop; i++)
        {
            index = Random.Range(0, tmpList.Count);
            abilityList.Add(tmpList[index]);
            tmpList.Remove(tmpList[index]);
        }
    }

    // set skill name to text component
    private void SetSKillName()
    {
        int times = 0;

        for(int i = 0; i < abilityList.Count; i++)
        {
            textList[i].text = abilityList[i].GetName();
            times++;

            if(times == 3)
            {
                break;
            }
        }
    }

    // call passive skill and hide passiveSkillPanel canvus when press button
    public void ActivePassiveSkillOne()
    {
        if(abilityList.Count >= 1)
        {
            abilityList[0].DisplayName();
            abilityList[0].Active();
        }
        this.gameObject.SetActive(false);
    }

    // call passive skill and hide passiveSkillPanel canvus when press button
    public void ActivePassiveSkillTwo()
    {
        if (abilityList.Count >= 2)
        {
            abilityList[1].DisplayName();
            abilityList[1].Active();
        }
        this.gameObject.SetActive(false);
    }

    // call passive skill and hide passiveSkillPanel canvus when press button
    public void ActivePassiveSkillThree()
    {
        if (abilityList.Count == 3)
        {
            abilityList[2].DisplayName();
            abilityList[2].Active();
        }
        this.gameObject.SetActive(false);
    }
}
