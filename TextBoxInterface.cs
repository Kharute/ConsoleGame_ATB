using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame_ATB
{
    internal class TextBoxInterface
    {
        public string[] boxStr = new string[Define.SizeY_TextBox];
        //스트링 하나를 쪼개서 올린다. 스트링의 길이까지.

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
            init();
            //개수 보여주는 건 이따 작성
            
            int lengthCtl = values.Count;
            if (lengthCtl > 4)
                lengthCtl = 4;
            // i부터 4개를 보여줘야하는데 커서가 일정 수 이상 넘어가면
            // 커서는 알아서 고정할테니, value값이 움직여주면 됨.

            Cussor(values, cussor);

            for (int i = 0; i < lengthCtl; i++)
            {
                if(cussor > 4)
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
        public void PrintBattleTextBox(bool turn, int dmg)
        {
            init();

            if(turn == true)
            {

            }
            boxStr[2] = "";

        }
        public void Cussor(List<string> strings, int cussor)
        {
            for (int i = 2; i < strings.Count+2; i++)
            {
                if(i < boxStr.Length)
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
        }
    }
}
