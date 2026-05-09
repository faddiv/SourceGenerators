using Foxy.Params.SourceGenerator.Data;
using Microsoft.CodeAnalysis;
using GenericTypeInfo = Foxy.Params.SourceGenerator.Data.GenericTypeInfo;
using TypeInfo = Foxy.Params.SourceGenerator.Data.TypeInfo;

namespace SourceGeneratorTests.TestInfrastructure
{
    public static class TestData
    {
        public static CandidateTypeInfo CreateCandidateTypeInfo(
            bool inGlobalNamespace = true,
            string namespaceValue = "Something",
            string[]? typeHierarchy = null,
            string typeName = "Baz")
        {
            return new CandidateTypeInfo(InGlobalNamespace: inGlobalNamespace, Namespace: namespaceValue,
                TypeHierarchy: typeHierarchy ?? ["Foo", "Bar"], TypeName: typeName);
        }

        public static MethodInfo CreateDerivedData(
            bool isStatic = true,
            string methodName = "Format",
            ParameterInfo[]? parameters = null,
            ReturnKind returnsKind = ReturnKind.ReturnsType,
            string returnType = "object",
            GenericTypeInfo[]? typeConstraints = null)
        {
            return new MethodInfo(
                IsStatic: isStatic,
                MethodName: methodName,
                Parameters: parameters ??
                [
                    new ParameterInfo(new TypeInfo("string"), "foo", RefKind.Ref, true),
                    new ParameterInfo(new TypeInfo("int"), "baz", RefKind.Out, false),
                    new ParameterInfo(new TypeInfo("ReadOnlySpan", ["T"]), "args", RefKind.None, false),
                ],
                ReturnsKind: returnsKind,
                ReturnType: returnType,
                TypeConstraints: typeConstraints ??
                [
                    new GenericTypeInfo(Type: "T1", ConstraintType: ConstraintType.Class,
                        HasConstructorConstraint: true, ConstraintTypes: [])
                ]);
        }

        public static SuccessfulParamsCandidate CreateSuccessfulParamsCandidate(
            CandidateTypeInfo? typeInfo = null,
            MethodInfo? derivedData = null,
            int maxOverrides = 5,
            bool hasParams = true)
        {
            return new SuccessfulParamsCandidate(TypeInfo: typeInfo ?? CreateCandidateTypeInfo(),
                MethodInfo: derivedData ?? CreateDerivedData(), MaxOverrides: maxOverrides, HasParams: hasParams);
        }
    }
}
