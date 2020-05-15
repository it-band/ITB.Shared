using System;
using ITB.ResultModel;

namespace ITB.ApiResultModel
{
    // Example of how to implement DerivedTypeJsonConverter
    public class FailureJsonConverter : DerivedTypeJsonConverter<Failure>
    {
        protected override Type NameToType(string typeName)
        {
            return typeName switch
            {
                nameof(Failure) => typeof(Failure),
                nameof(NotFoundFailure) => typeof(NotFoundFailure),
                nameof(ValidationFailure) => typeof(ValidationFailure),
                nameof(ExceptionFailure) => typeof(ExceptionFailure),
                nameof(UnauthorizedFailure) => typeof(UnauthorizedFailure),
                nameof(ForbiddenFailure) => typeof(ForbiddenFailure),

                // TODO: Create a case for each derived type
                _ => throw new NotImplementedException($"Unsupported type string \"{typeName}\".")
            };
        }

        protected override string TypeToName(Type type)
        {
            return type.Name;
        }
    }
}
