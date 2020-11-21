using HomeTask6.Web.Entities;
using System.Collections.Generic;
using System.Linq;

namespace HomeTask6.Web.TagHelpers
{
    public static class GroupEnumerable
    {
        public static IList<CategoryMenu> BuildTree(this IEnumerable<CategoryMenu> source)
        {
            IEnumerable<IGrouping<int?, CategoryMenu>> groups = source.GroupBy(i => i.ParentId);

            List<CategoryMenu> roots = groups.FirstOrDefault(g => g.Key.HasValue == false).ToList();

            if (roots.Count > 0)
            {
                Dictionary<int, List<CategoryMenu>> dict = groups.Where(g => g.Key.HasValue).ToDictionary(g => g.Key.Value, g => g.ToList());
                for (int i = 0; i < roots.Count; i++)
                {
                    AddChildren(roots[i], dict);
                }
            }

            return roots;
        }

        private static void AddChildren(CategoryMenu node, IDictionary<int, List<CategoryMenu>> source)
        {
            if (source.ContainsKey(node.Id))
            {
                node.Children = source[node.Id];
                for (int i = 0; i < node.Children.Count; i++)
                {
                    AddChildren(node.Children[i], source);
                }
            }
            else
            {
                node.Children = new List<CategoryMenu>();
            }
        }
    }
}
