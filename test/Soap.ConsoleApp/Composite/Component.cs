using System.Collections.Generic;

namespace Soap.ConsoleApp.Composite
{
    public abstract class Component
    {
        protected List<Component> Children = new List<Component>();

        public string Name { get; set; }

        public abstract void Add(Component component);

        public abstract void Remove(Component component);

        public virtual void Display(int depth=0)
        {
            System.Console.WriteLine($"--{Name}");
            foreach (var child in Children)
                child.Display(depth + 2);
        }
    }
}
