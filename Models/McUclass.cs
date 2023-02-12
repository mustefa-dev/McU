using System.Text.Json.Serialization;

namespace McU.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum McUclass
{
    Marvel = 1,
    ScarletWitch = 2,
    AntMan = 3,
    IronMan = 4,
    CaptainAmerica = 5,
    Thor = 6,
    ShangChi = 7,
    Sersi = 8
}