using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Personality : MonoBehaviour
{
    List<(int, PlayersClass)> classes;
    [SerializeField] GameObject form;
    [SerializeField] GameObject content;

    // Start is called before the first frame update
    void Start()
    {
        classes = CharacterData.GetClasses();
        foreach ((int, PlayersClass) playersClass in classes)
            if (playersClass.Item2 != null)
            {
                GameObject classBody = Instantiate(form, content.transform);
                classBody.GetComponent<FormShower>().SetHead(playersClass.Item2.name);
                //Ability[] abilityArr = playersClass.Item2.ChooseSubClass();
                if (playersClass.Item2 != null)
                {
                    Ability[] abilitieSubClassArr = playersClass.Item2.ChooseSubClass(DataSaverAndLoader.LoadSubClass(playersClass.Item2));
                    Ability[] abilityArr = playersClass.Item2.GetAbilities();
                    if (abilitieSubClassArr != null)
                        abilityArr.Concat(abilitieSubClassArr).ToArray();
                    foreach (Ability x in abilityArr)
                    {
                        if (x.level <= playersClass.Item1)
                            Instantiate(form, classBody.GetComponent<FormShower>().GetDiscription().transform).GetComponent<FormShower>().CreateAbility(x);
                    }
                }
            }
    }
}
