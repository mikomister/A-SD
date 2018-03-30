using System;
using System.Collections;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Text.RegularExpressions;
using System.CodeDom.Compiler;

namespace WordSet
{
    class Program
    {
        static void Main(string[] args)
        {
            var ws1 = new WordSet(new string[] { "a", "dasd", "bssae", "edfde", "asgsa", "var", "qwse", "ewter" });
            Console.WriteLine(@"WordSet by array: {0} Это исходный WordSet", ws1);

            var ws2 = new WordSet(new string[0]);
            var res = new WordSet(ws2, ws1);
            Console.WriteLine(@"Новый WordSet из двух: {0}", res);


            res.Delete("a");
            Console.WriteLine(@"Удаляем ""a"": {0}", res);

            res.Insert("a");
            Console.WriteLine(@"Вставляем ""a"": {0}", res);


            var r = res.newWordSetByWordLength(4);
            Console.WriteLine(@"Из имеющегося WordSet получить новый, где длины слов 4(исходный при этом не меняется): {0}", r);

            res.RemovePalindroms();
            Console.WriteLine(@"Удалеяем полиндромы:n {0}", res);

            var t = res.vowelDivide();
            Console.WriteLine(@"WordSet с словами начинающимися на гласные: {0}", t[0]);

            Console.WriteLine(@"WordSet с словами начинающимися на согласные: {0}", t[1]);


        }
    }
}