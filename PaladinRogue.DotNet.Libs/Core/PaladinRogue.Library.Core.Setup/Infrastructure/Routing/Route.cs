namespace PaladinRogue.Library.Core.Setup.Infrastructure.Routing
{
    public class Route<T>
    {
        public string Template { get; }
        public T Restriction { get; }

        private Route(string template, T restriction)
        {
            Template = template;
            Restriction = restriction;
        }

        public static Route<T> Create(string template, T restriction)
        {
            return new Route<T>(template, restriction);
        }
    }
}