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
        public string[] boxStr = new string[7];
        //스트링 하나를 쪼개서 올린다. 스트링의 길이까지.

        public static string fillBlank(string str)
        {
            int sLength = str.Length;
            for (int i = 0; i < 53 - sLength; i++)
            {
                str += "  ";
            }
            return str;
        }
        public void init()
        {
            boxStr = textBox.ToArray();
        }

        // 자... 이제 이거 엔터 누르면 바꿀 수 있어야 함.
        List<string> textBox = new List<string>() {
            "□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□",
            "□                                                                                                                    □",
            "□        "+fillBlank("")+"  □",
            "□        "+fillBlank("")+"  □",
            "□        "+fillBlank("")+"  □",
            "□                                                                                                                    □",
            "□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□"
        };
        
        
        public void PrintTextBox()
        {
            foreach(string s in boxStr) 
            {
                for(int i = 0; i < s.Length; i++)
                {
                    Console.Write(s[i]);
                }
                Console.WriteLine();
            }
        }
        public void TextBoxChanged()
        {
            string[] str = new string[2];

            for (int i = 0; i < str.Length; i++)
            {
                str[i] = "  ";
                /*if (i == menu) { str[i] = "▷"; } else {}*/
            }

            boxStr[2] = $"□    {str[0]}" + fillBlank("구매한다") + "  ";
            boxStr[3] = $"□    {str[1]}" + fillBlank("나간다") + "  ";
        }

        public void CurssorMove(int select)
        {
            string[] str = new string[2];

            for (int i = 0; i < str.Length; i++)
            {
                if (i == select)
                {
                    str[i] = "▷";
                }
                else
                {
                    str[i] = "  ";
                }
            }
        }
    }
}
