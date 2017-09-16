namespace Common.Aws {

    public interface IAmazonService : IService {

        AmazonCredential Credential { get; set; }
        
    }

}