public enum ObjectTypes
{
    Empty,
    Barrack,
    PowerPlant,
    Soldier
}

public static class ObjectFactory
{
    //produce desired type of object
    public static IObject GetObject(ObjectTypes objType)
    {
        switch (objType)
        {
            case ObjectTypes.Empty:
                return new Empty();
            case ObjectTypes.Barrack:
                return new Barrack();
            case ObjectTypes.PowerPlant:
                return new PowerPlant();
            case ObjectTypes.Soldier:
                return new Soldier();
            default:
                return new Empty();
        }
    }

    //produce desired type of object with position
    public static IObject GetObject(ObjectTypes objType, IntegerVector2 pos)
    {
        switch (objType)
        {
            case ObjectTypes.Barrack:
                return new Barrack(pos);
            case ObjectTypes.PowerPlant:
                return new PowerPlant(pos);
            default:
                return new Empty();
        }
    }

}