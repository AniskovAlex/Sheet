using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyController : MonoBehaviour
{
    [SerializeField] List<InputField> money;
    [SerializeField] List<InputField> moneyAdd;

    // Start is called before the first frame update
    void Start()
    {
        CharacterData.load += Init;
    }
    void Init()
    {
        SetMoney();

    }
    void SetMoney()
    {

        for(int i = 0;i<money.Count;i++)
        {
            money[i].text = CharacterData.GetMoney(i).ToString();        
        }
    }

    //Временно

    public void MoneyCon()
    {
        List<(int, int)> m = new List<(int, int)>();
        InputField x;
        for (int index = 0; index < money.Count; index++)
        {
            x = money[index];
            int moneyInt = 0;
            int moneyAddInt = 0;
            int.TryParse(x.text, out moneyInt);
            int.TryParse(moneyAdd[index].text, out moneyAddInt);
            m.Add((moneyInt, moneyAddInt));
            moneyAdd[index].text = "";
        }
        for (int index = 0; index < m.Count; index++)
        {
            int diver = m[index].Item1 - m[index].Item2;
            if (diver < 0)
            {
                if (index != 0)
                {
                    (int, int) buf2 = m[index];
                    buf2.Item1 += 10;
                    m[index] = buf2;
                    index--;
                    buf2 = m[index];
                    buf2.Item1 -= 1;
                    m[index] = buf2;
                    index--;
                }
                else
                {
                    return;
                }
            }
            else
            {
                (int, int) buf2 = m[index];
                buf2.Item1 = diver;
                buf2.Item2 = 0;
                m[index] = buf2;
            }

        }
        for (int index = 0; index < money.Count; index++)
        {
            x = money[index];
            x.text = m[index].Item1.ToString();
        }
        SaveMoney();
    }

    public void MoneyPlus()
    {
        List<(int, int)> m = new List<(int, int)>();
        InputField x;
        for (int index = 0; index < money.Count; index++)
        {
            x = money[index];
            int moneyInt = 0;
            int moneyAddInt = 0;
            int.TryParse(x.text, out moneyInt);
            int.TryParse(moneyAdd[index].text, out moneyAddInt);
            m.Add((moneyInt, moneyAddInt));
            moneyAdd[index].text = "";
        }
        for (int index = m.Count - 1; index >= 0; index--)
        {
            int diver = m[index].Item1 + m[index].Item2;
            if (diver >= 10 && index != 0)
            {
                int extra = diver / 10;
                diver %= 10;
                (int, int) buf2 = m[index];
                buf2.Item1 = diver;
                m[index] = buf2;
                buf2 = m[index - 1];
                buf2.Item2 += extra;
                m[index - 1] = buf2;
            }
            else
            {
                (int, int) buf2 = m[index];
                buf2.Item1 = diver;
                m[index] = buf2;
            }
            x = money[index];
            x.text = m[index].Item1.ToString();
        }
        SaveMoney();
    }

    public void SaveMoney()
    {
        CharacterData.SetMoney(money.ConvertAll<int>(x => int.Parse(x.text)).ToArray());
        //DataCloudeSave.Save();
    }
}
