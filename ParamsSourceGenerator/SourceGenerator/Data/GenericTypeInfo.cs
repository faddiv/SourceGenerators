using SourceGeneratorTools;

namespace Foxy.Params.SourceGenerator.Data;

public sealed record GenericTypeInfo(
    string Type,
    ConstraintType ConstraintType,
    ComparableArray<string> ConstraintTypes,
    bool HasConstructorConstraint)
{
    public void WriteTo(SourceBuilder builder)
    {
        if (!HasConstraint())
        {
            return;
        }

        builder.AppendLine($"where {Type} : {GenericTypeRestrictions()}");
    }

    public override string ToString()
    {
        return Type;
    }

    private bool HasConstraint()
    {
        return ConstraintType != ConstraintType.None
               || ConstraintTypes.Count > 0
               || HasConstructorConstraint;
    }

    private IEnumerable<string> GenericTypeRestrictions()
    {
        switch (ConstraintType)
        {
            case ConstraintType.Unmanaged:
                yield return "unmanaged";
                break;
            case ConstraintType.Struct:
                yield return "struct";
                break;
            case ConstraintType.Class:
                yield return "class";
                break;
            case ConstraintType.NotNull:
                yield return "notnull";
                break;
        }

        if (ConstraintTypes.Count > 0)
        {
            foreach (var item in ConstraintTypes)
            {
                yield return item;
            }
        }

        if (HasConstructorConstraint)
        {
            yield return "new()";
        }
    }
}
