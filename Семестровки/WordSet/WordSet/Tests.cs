using NUnit.Framework;
using System;
using System.Security.Cryptography;
namespace WordSet
{
    [TestFixture]
    public class Tests
    {
        //[Test]
        //public void TestCompare()
        //{
        //    var t = new Word { Data = "ab" };
        //    var b = new Word { Data = "a" };
        //    var res = WordSet._wordsComparator(b, t);
        //    Assert.AreEqual(res, -1);
        //}

        //[Test]
        //public void TestIsPolyndrom()
        //{
        //    var w = new Word { Data = "12321" };
        //    var res = w.IsPolyndrom;
        //    Assert.AreEqual(res, true);
        //}

        //[Test]
        //public void TestMerge()
        //{
        //    var ws1 = new WordSet(new string[] { "d", "b" });
        //    var ws2 = new WordSet(new string[] { "c", "a" });
        //    var res = (new WordSet(ws1, ws2)).ToString();
        //    Assert.AreEqual("a->b->c->d", res);
        //}

        [Test]
        public void TestToString()
        {
            Assert.AreEqual(new WordSet(new string[] { "a", "b", "c" }).ToString(), "a->b->c");
        }

        [Test]
        public void TestToStringByNull()
        {
            Assert.AreEqual(new WordSet(new string[0]).ToString(), "WordSet is empty");
        }

        [Test]
        public void TestInsert()
        {
            var tws = new WordSet(new string[] { "b", "d" });
            tws.Insert("a");
            tws.Insert("c");
            tws.Insert("e");
            tws.Insert("b");
            Assert.AreEqual(tws.ToString(), "a->b->c->d->e");
        }

        [Test]
        public void TestDelete()
        {
            var tws = new WordSet(new string[] { "b", "d" });
            tws.Delete("b");
            tws.Delete("a");
            Assert.AreEqual(tws.ToString(), "d");
        }

        [Test]
        public void TestNewWordSetByWordLength()
        {
            var tws = new WordSet(new string[] { "a", "dasd", "bssae", "edfde", "asgsa", "var", "qwse", "ewter" });
            Assert.AreEqual(tws.newWordSetByWordLength(4).ToString(), "dasd->qwse");
        }

        [Test]
        public void TestRemovePalindroms()
        {
            var tws = new WordSet(new string[] { "a", "dasd", "bssae", "edfde", "asgsa", "var", "qwse", "ewter" });
            tws.RemovePalindroms();
            Assert.AreEqual(tws.ToString(), "bssae->dasd->ewter->qwse->var");
        }

        [Test]
        public void TestVowelDivide()
        {
            var tws = new WordSet(new string[] { "a", "dasd", "bssae", "edfde", "asgsa", "var", "qwse", "ewter" });
            var t = tws.vowelDivide();
            Assert.AreEqual(t[0].ToString(), "a->asgsa->edfde->ewter");
            Assert.AreEqual(t[1].ToString(), "bssae->dasd->qwse->var");
        }

    }
}
