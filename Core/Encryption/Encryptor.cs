namespace Core.Encryption
{

    public sealed class Encryptor
    {

        

    }

    public enum EncryptionKind
    {
       
        None = 0,

       
        Up = 1,

       
        Down = 1 << 1,

      
        Ceil = 1 << 2,

       
        Floor = 1 << 3,

        
        HalfUp = 1 << 4,

       
        HalfDown = 1 << 5,

       
        HalfEven = 1 << 6
    }
}
