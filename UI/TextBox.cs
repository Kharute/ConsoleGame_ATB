using ConsoleGame_ATB.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleGame_ATB
{
    internal class TextBox
    {
        public string[] boxStr = new string[Define.SizeY_TextBox];
        //스트링 하나를 쪼개서 올린다. 스트링의 길이까지.
        /*int curCussor = 0;
        int curMenu = 0;*/
        public static string fillBlank(string str)
        {
            int sLength = str.Length;
            for (int i = 0; i < 55 - sLength; i++)
            {
                str += "  ";
            }
            return str;
        }
        public void init()
        {
            for (int k = 0; k < Define.SizeY_TextBox; k++)
            {
                boxStr[k] = "";
                for (int i = 0; i < Define.SizeX_Map + Define.SizeX_Menu; i++)
                {
                    if(k == 0 || k == Define.SizeY_TextBox-1)
                        boxStr[k] += "□";
                    else
                        boxStr[k] += "  ";
                }
            }
        }
        public void PrintTextBox(List<string> values, int cussor)
        {
            boxStr[1] = "";
            //개수 보여주는 건 이따 작성
            
            int lengthCtl = values.Count;
            if (lengthCtl > 7)
                lengthCtl = 7;
            // i부터 4개를 보여줘야하는데 커서가 일정 수 이상 넘어가면
            // 커서는 알아서 고정할테니, value값이 움직여주면 됨.

            Cussor(values.Count, cussor);

            for (int i = 0; i < lengthCtl; i++)
            {
                if(cussor > 7)
                {
                    boxStr[i + 1] += values[cussor + i];
                }
                else
                {
                    boxStr[i + 1] += values[i];
                }
            }
            foreach (string s in boxStr) 
            {
                Console.WriteLine(s);
            }
        }
        public void PrintBattleTextBox(int sanghwang, string name, int value)
        {
            string s = "";

            //데미지 계산 나1 상대2
            // 전투승리3, 전투패배4
            // 도망5 실패6
            switch (sanghwang)
            {
                case 1:
                    s = $"     {name}는 몬스터에게 {value} 의 데미지를 입혔다.";
                    break;
                case 2:
                    s = $"     몬스터에게 {name}은 {value} 의 데미지를 받았다.";
                    break;
                case 3:
                    s = $"     {name}은 {value}의 경험치를 얻었다.";
                    break;
                case 4:
                    s = $"     전투에서 패배했다....";
                    break;
                case 5:
                    s = $"     파티는 무사히 도망쳤다.";
                    break;
                case 6:
                    s = $"     파티는 도망치는 것에 실패했다.";
                    break;
                case 7:
                    s = $"     {name}에게 아이템을 사용하여 {value}만큼 회복했다.";
                    break;
                case 8:
                    s = $"     {name}에게 아이템을 사용하여 {value}만큼 공격력이 상승했다.";
                    break;
                case 9:
                    s = $"     {name}에게 아이템을 사용하여 {value}만큼 방어력이 상승했다.";
                    break;
                case 10:
                    s = $"     {name}에게 아이템을 사용하여 {value}만큼 이동속도가 회복했다.";
                    break;
            }
            boxStr[2] = s;
            boxStr[3] = "";
            boxStr[4] = "";
            boxStr[5] = "";

            foreach (string ss in boxStr)
            {
                Console.WriteLine(ss);
            }
        }
        public void Cussor(int count, int cussor)
        {
            for(int i = 1; i < boxStr.Length; i++)
            {
                if(i > 1 && i<count + 1)
                {
                    if (i < boxStr.Length)
                    {
                        if (i == cussor + 2)
                        {
                            boxStr[i] = "    ▷";
                        }
                        else
                        {
                            boxStr[i] = "      ";
                        }
                    }
                }
                else
                    boxStr[i] = "      ";
            }
            for (int i = 2; i < count + 1; i++)
            {
                
            }
        }

        // battle인지 상점인지는 체크해야되지만, 우선 배틀꺼 끌고옴
        // 1, battle 2. item 3.run
        // 1. buy 2. sell 3. run
        
    }
}
