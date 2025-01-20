namespace Italbytz.Adapters.Algorithms.AI.Search.CSP.Examples;

public class MapCSP : CSP<Variable, string>
{
    public static readonly Variable WA = new Variable("WA");
    public static readonly Variable NT = new Variable("NT");
    public static readonly Variable SA = new Variable("SA");
    public static readonly Variable Q = new Variable("Q");
    public static readonly Variable NSW = new Variable("NSW");
    public static readonly Variable V = new Variable("V");
    public static readonly Variable T = new Variable("T");
    
    public static readonly string RED = "red";
    public static readonly string GREEN = "green";
    public static readonly string BLUE = "blue";
    
    public static readonly Domain<string> Colors = new Domain<string>(RED, GREEN, BLUE);
    
    public MapCSP(): base([NT, SA, V, T, NSW, Q, WA])
    {
        foreach (var variable in Variables)
        {
            SetDomain(variable, Colors);
        }
        
        AddConstraint(new NotEqualConstraint<Variable, string>(WA, NT));
        AddConstraint(new NotEqualConstraint<Variable, string>(WA, SA));
        AddConstraint(new NotEqualConstraint<Variable, string>(NT, SA));
        AddConstraint(new NotEqualConstraint<Variable, string>(NT, Q));
        AddConstraint(new NotEqualConstraint<Variable, string>(SA, Q));
        AddConstraint(new NotEqualConstraint<Variable, string>(SA, NSW));
        AddConstraint(new NotEqualConstraint<Variable, string>(SA, V));
        AddConstraint(new NotEqualConstraint<Variable, string>(Q, NSW));
        AddConstraint(new NotEqualConstraint<Variable, string>(NSW, V));
    }
}