namespace JsonTest.NewtonsoftJson.Privatejson;

public class PublicData
{
    public PublicData(string publicValue)
    {
        PublicProperty = publicValue;
    }

    public string PublicProperty { get; set; } = "this is public set property";
}