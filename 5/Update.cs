using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5
{
    internal class Update
    {
        private readonly string input;
        public readonly List<int> pages = new List<int>();

        public Update(string input)
        {
            this.input = input;
            pages.AddRange(input.Split(',').Select(int.Parse));
        }

        public bool IsInOrder(RuleBook ruleBook)
        {
            for (int i = 0; i < pages.Count; i++)
            {
                var page = pages[i];
                var rules = ruleBook.GetByPageA(page);
                if (rules == null)
                {
                    continue;
                }
                foreach (var rule in rules)
                {
                    int indexB = pages.IndexOf(rule.pageB);
                    if (indexB >= 0 && indexB < i)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void PutInOrder(RuleBook ruleBook)
        {   
            while (true)
            {
                bool errorFound = false;
                for (int i = 0; i < pages.Count; i++)
                {
                    var page = pages[i];
                    var rules = ruleBook.GetByPageA(page);
                    if (rules == null)
                    {
                        continue;
                    }
                    foreach (var rule in rules)
                    {
                        int indexB = pages.IndexOf(rule.pageB);
                        if (indexB >= 0 && indexB < i)
                        {
                            errorFound = true;
                            pages.RemoveAt(indexB);
                            pages.Insert(i, rule.pageB);
                            break;
                        }
                    }
                }
                if (!errorFound)
                {
                    break;
                }
            }
        }

        public int GetMiddlePage()
        {
            return pages[pages.Count / 2];
        }

        public override string ToString()
        {
            return string.Join(", ", pages);
        }
    }
}
