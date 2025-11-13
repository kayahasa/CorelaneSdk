using System.Text.Json.Serialization;

namespace CorelaneSdk.Enums.Common;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GenderEnum
{
    Male,
    Female,
}
