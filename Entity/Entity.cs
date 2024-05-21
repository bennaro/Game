
namespace Game
{
    public class Entity
    {
        private Dictionary<string, List<Component>> componentGroups;

        public Entity()
        {
            componentGroups = new Dictionary<string, List<Component>>();
        }

        public void Attach(string group, Component component)
        {
            if (!componentGroups.ContainsKey(group))
            {
                componentGroups[group] = new List<Component>();
            }

            componentGroups[group].Add(component);
        }

        public void Draw()
        {
            foreach (var group in componentGroups.Values)
            {
                foreach (var component in group)
                {
                    component.Draw();
                }
            }
        }
    }
}