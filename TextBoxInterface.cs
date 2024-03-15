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
        //스트링 하나를 쪼개서 올린다. 스트링의 길이까지.
        static string str1 = "공격한다";

        public static string fillBlank(string str)
        {
            int sLength = str.Length;
            for (int i = 0; i < 55 - sLength; i++)
            {
                str += "  ";
            }
            return str;
        }

        // 자... 이제 이거 엔터 누르면 바꿀 수 있어야 함.
        List<string> list = new List<string>() {
            "□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□",
            "□                                                                                                                    □",
            "□    "+fillBlank("공격한다")+"  □",
            "□    "+fillBlank("공격한다")+"  □",
            "□    "+fillBlank("공격한다")+"  □",
            "□                                                                                                                    □",
            "□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□"
        };
        
        public void PrintTextBox()
        {
            foreach(string s in list) 
            {
                for(int i = 0; i < s.Length; i++)
                {
                    Console.Write(s[i]);
                }
                Console.WriteLine();
            }
        }
    }
}
