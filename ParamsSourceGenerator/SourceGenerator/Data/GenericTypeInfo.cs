using Foxy.Params.SourceGenerator.Helpers;
using SourceGeneratorTools;

namespace Foxy.Params.SourceGenerator.Data;

internal class GenericTypeInfo : IEquatable<GenericTypeInfo?>
{
    public required string Type { get; init; }

    public required ConstraintType ConstraintType { get; init; }
    public required string[] ConstraintTypes { get; init; }
    public required bool HasConstructorConstraint { get; init; }

    public bool HasConstraint()
    {
        return ConstraintType != ConstraintType.None
            || ConstraintTypes.Length > 0
            || HasConstructorConstraint;
    }

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

    public override bool Equals(object? obj)
    {
        return Equals(obj as GenericTypeInfo);
    }

    public bool Equals(GenericTypeInfo? other)
    {
        return other is not null &&
               Type == other.Type &&
               ConstraintType == other.ConstraintType &&
               ConstraintTypes.SequenceEqual(other.ConstraintTypes) &&
               HasConstructorConstraint == other.HasConstructorConstraint;
    }

    public override int GetHashCode()
    {
        int hashCode = 1142259888;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
        hashCode = hashCode * -1521134295 + ConstraintType.GetHashCode();
        hashCode = hashCode * -1521134295 + CollectionComparer.GetHashCode(ConstraintTypes);
        hashCode = hashCode * -1521134295 + HasConstructorConstraint.GetHashCode();
        return hashCode;
    }

    private IEnumerable<string> GenericTypeRestrictions()
    {switch (ConstraintType)
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

        if (ConstraintTypes.Length > 0)
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
