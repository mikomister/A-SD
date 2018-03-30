using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WordSet
{
    public class Word
    {
        public Word Next { get; set; }
        public string Data { get; set; }
        public bool IsPolyndrom { get; set; }

        /// <summary>
        /// Если слово полиндром вернет true, иначе false
        /// </summary>
        private void CheckPolyndrom()
        {
            IsPolyndrom = true;
            int i = 0, k = Data.Length - 1;
            for (; i < Data.Length && k > i; i++, k--)
                if (Data[i] != Data[k])
                {
                    IsPolyndrom = false;
                    break;
                }

        }

        public Word()
        {
        }

        public Word(string data, Word next = null)
        {
            Data = data;
            Next = next;
            CheckPolyndrom();
        }

        public Word Copy() => new Word() { Data = this.Data, Next = this.Next, IsPolyndrom = this.IsPolyndrom };

        public override string ToString() => Data;

    }

    public class WordSet : IEnumerable
    {
        private static Regex _vowels = new Regex(@"^[aeiouyауоыиэяюёе]", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

        private Word _head;

        public bool IsEmpty() => _head == null;

        public Word Peek() => _head;

        public WordSet()
        {
            _head = new Word();
        }

        public WordSet(Word head)
        {
            _head = head;
        }

        public WordSet(string[] array)
        {
            if (array.Length > 0)
            {
                Array.Sort(array, WordsComparator);
                _head = new Word(array[0]);
                Word temp = _head;
                for (var i = 1; i < array.Length; i++)
                {
                    temp.Next = new Word(array[i]);
                    temp = temp.Next;
                }
            }
            else _head = null;

        }

        public WordSet(WordSet w1, WordSet w2)
        {
            _head = w1.IsEmpty() ? CopyWordSet(w2) :
                      w2.IsEmpty() ? CopyWordSet(w1) : MergeWordSets(w1.Peek(), w2.Peek());

        }

        /// <summary>
        /// Вставляет слово в упорядоченное множество
        /// </summary>
        /// <returns>The insert.</returns>
        /// <param name="word">Word.</param>
        public void Insert(string word)
        {
            if (IsEmpty() || WordsComparator(_head.Data, word) > 0) _head = new Word(word, _head);
            if (FindPrevWord(word) != null) return;
            else
            {
                Word prev = _head, cur = _head.Next, newWord = new Word(word);
                while (cur != null && WordsComparator(cur, newWord) < 0)
                {
                    prev = cur;
                    cur = cur.Next;
                }
                InsertAfter(prev, newWord);
            }
        }

        /// <summary>
        ///     Удаляет слова являющиеся полиндромами
        /// </summary>
        public void RemovePalindroms()
        {
            if (!IsEmpty())
            {
                while (_head.IsPolyndrom) _head = _head.Next;
                if (!IsEmpty())
                {
                    Word curWord = _head.Next, prevWord = _head;

                    while (curWord != null)
                    {
                        if (curWord.IsPolyndrom)
                        {
                            RemoveNextWord(prevWord);
                            curWord = prevWord.Next;
                            continue;
                        }

                        prevWord = curWord;
                        curWord = curWord.Next;
                    }
                }
            }
        }

        /// <summary>
        ///     Удаляет указанное слово, если оно есть.
        /// </summary>
        /// <param name="word"></param>
        public void Delete(string word)
        {
            if (!IsEmpty())
                if (_head.Data.Equals(word))
                    _head = _head.Next;
                else
                    RemoveNextWord(FindPrevWord(word));
        }

        public WordSet newWordSetByWordLength(int l)
        {
            var result = new WordSet();
            Word temp = result.Peek();
            foreach (Word e in this)
            {
                if (e.Data.Length == l)
                {
                    InsertAfter(temp, e.Copy());
                    temp = temp.Next;
                }
            }
            result._head = result._head.Next;
            return result;
        }

        public WordSet[] vowelDivide()
        {
            WordSet w1 = new WordSet(), w2 = new WordSet();
            Word temp = _head, t1 = w1.Peek(), t2 = w2.Peek();
            while (temp != null)
            {
                if (IsVowels(temp.Data))
                {
                    InsertAfter(t1, temp.Copy());
                    t1 = t1.Next;
                }
                else
                {
                    InsertAfter(t2, temp.Copy());
                    t2 = t2.Next;
                }
                temp = temp.Next;
            }
            w1._head = w1._head.Next;
            w2._head = w2._head.Next;
            return new WordSet[] { w1, w2 };
        }


        public IEnumerator GetEnumerator()
        {
            var current = _head;
            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
        }

        public override string ToString()
        {
            if (IsEmpty())
                return "WordSet is empty";

            var sb = new StringBuilder();
            var curWord = _head;

            while (curWord.Next != null)
            {
                sb.Append(curWord);
                sb.Append("->");
                curWord = curWord.Next;
            }

            sb.Append(curWord);

            return sb.ToString();
        }

        private bool IsVowels(string str) => _vowels.IsMatch(str);

        /// <summary>
        ///     Возвращает ссылку на слово предшествующее искомому, если искомое слово не найдено или множесто слов пустое вернет
        ///     null.
        ///     Если искомое слово является Head то возвращает ссылку на него.
        /// </summary>
        /// <param name="word">Искомое слово</param>
        /// <returns></returns>
        private Word FindPrevWord(string word)
        {
            if (IsEmpty()) return null;
            var curWord = _head;
            if (curWord.Data.Equals(word)) return curWord;
            while (curWord.Next != null)
            {
                if (curWord.Next.Data.Equals(word)) return curWord;
                curWord = curWord.Next;
            }

            return null;
        }

        /// <summary>
        ///     Удаляет слово следующее за переданным.
        /// </summary>
        /// <param name="prev"></param>
        private void RemoveNextWord(Word prev)
        {
            if (prev != null)
                prev.Next = prev.Next.Next;
        }

        private static int WordsComparator(object x, object y)
        {
            string word1, word2;
            if (x is Word && y is Word)
            {
                word1 = ((Word)x).Data;
                word2 = ((Word)y).Data;
            }
            else
            {
                word1 = x as string;
                word2 = y as string;
            }
            if (word1 == null || word2 == null) throw new NullReferenceException();
            var minLength = Math.Min(word1.Length, word2.Length);
            var t = 0;
            for (var i = 0; i < minLength; i++)
            {
                t = word1[i].CompareTo(word2[i]);
                if (t != 0) return t;
            }

            return word1.Length < word2.Length ? -1 : 1;
        }

        private void Fill(Word filling, Word source)
        {
            while (source != null)
            {
                InsertAfter(filling, source.Copy());
                filling = filling.Next;
                source = source.Next;
            }
        }

        private void MergeHelper(ref Word cur1, ref Word cur2, ref Word prev)
        {
            if (WordsComparator(cur1, cur2) < 0)
            {
                InsertAfter(prev, cur1.Copy());
                cur1 = cur1.Next;
            }
            else
            {
                InsertAfter(prev, cur2.Copy());
                cur2 = cur2.Next;
            }
            prev = prev.Next;
        }

        private Word MergeWordSets(Word cur1, Word cur2)
        {
            Word head = new Word(), prev;
            MergeHelper(ref cur1, ref cur2, ref head);
            prev = head;
            while (cur1 != null && cur2 != null)
            {
                MergeHelper(ref cur1, ref cur2, ref prev);
            }
            if (cur1 == null) Fill(prev, cur2);
            else if (cur2 == null) Fill(prev, cur1);
            return head;
        }

        private Word CopyWordSet(WordSet ws)
        {
            var h = ws.Peek().Copy();
            h.Next = null;
            Fill(h, ws.Peek().Next);
            return h;
        }

        private void InsertAfter(Word cur, Word word)
        {
            word.Next = cur.Next;
            cur.Next = word;
        }

    }

}
