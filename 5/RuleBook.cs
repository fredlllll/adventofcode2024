using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5
{
    internal class RuleBook
    {
        private readonly Dictionary<int, List<OrderingRule>> _rules = new();

        public void Add(OrderingRule rule)
        {
            if (!_rules.TryGetValue(rule.pageA, out List<OrderingRule>? rules))
            {
                rules = new List<OrderingRule>();
                _rules[rule.pageA] = rules;
            }
            rules.Add(rule);
        }

        public List<OrderingRule>? GetByPageA(int pageA)
        {
            if (_rules.TryGetValue(pageA, out List<OrderingRule>? rules))
            {
                return rules;
            }
            return null;
        }
    }
}
