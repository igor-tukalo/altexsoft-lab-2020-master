using System.Collections.Generic;
using System.Linq;

namespace HomeTask4.SharedKernel
{
    public static class BuildTreeExtensions
    {
        public static IList<CategoryTree> BuildTree(this IEnumerable<CategoryTree> source)
        {
            IEnumerable<IGrouping<int?, CategoryTree>> groups = source.GroupBy(i => i.ParentId);

            List<CategoryTree> roots = groups.FirstOrDefault(g => g.Key.HasValue == false).ToList();

            if (roots.Count > 0)
            {
                Dictionary<int, List<CategoryTree>> dict = groups.Where(g => g.Key.HasValue).ToDictionary(g => g.Key.Value, g => g.ToList());
                for (int i = 0; i < roots.Count; i++)
                {
                    AddChildren(roots[i], dict);
                }
            }

            return roots;
        }

        private static void AddChildren(CategoryTree node, IDictionary<int, List<CategoryTree>> source)
        {
            if (source.ContainsKey(node.Id))
            {
                node.Childrens = source[node.Id];
                for (int i = 0; i < node.Childrens.Count; i++)
                {
                    AddChildren(node.Childrens[i], source);
                }
            }
            else
            {
                node.Childrens = new List<CategoryTree>();
            }
        }
    }
}
