namespace Soap.ConsoleApp.Composite
{
    public class CompositeComponent
        : Component
    {
        public override void Add(Component component)
        {
            Children.Add(component);
        }

        public override void Remove(Component component)
        {
            Children.Remove(component);
        }
    }
}
