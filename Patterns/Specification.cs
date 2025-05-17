namespace Szkolenie_Kamsoft.Patterns
{
    public interface ISpecification<T> where T : class
    {
        bool IsSatisfiedBy(T input);
    }
    public abstract class Specification<T> : IStrategy
    {
        public abstract bool IsSatisfiedBy(T input);
        public ISpecification<T> And(Specification<T> other)
        {
            return new AndSpecification<T>(this, other);
        }
        public ISpecification<T> Or(Specification<T> other)
        {
            return new OrSpecification<T>(this, other);
        }
        //TODO NOT
    }
    public class AndSpecification<T> : Specification<T> where T : class
    {
        private readonly Specification<T> left;
        private readonly Specification<T> right;

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            this.left = left;
            this.right = right;
        }
        public override bool IsSatisfiedBy(T input)
        {
            return left.IsSatisfiedBy(input) && right.IsSatisfiedBy(input);
        }
    }
    public class Rule<T> where T : class
    {
        private readonly ISpecification<T> specification;

        public Rule(ISpecification<T> specification)
        {
            this.specification = specification;
        }

    }
    public abstract class Validator<T> where T : class
    {
        private List<Rule<T>> rules = new();
        public Validator<T> Add(Rule<T> rule) 
        {
            rules.Add(rule);
            return this;
        }
        public T Execute(T input)
        {
            foreach (var  rule in rules)
            {
                if (!rule.Specification.IsSatisfiedBy(input))
                {
                    throw new Exception("Validation failed");
                }
            }
            return input;
        }
    }
}
