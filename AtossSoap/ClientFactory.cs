using Microsoft.Extensions.Logging;

namespace AtossSoap;

public static class ClientFactory {
    public static IAtossClient Create(string username, string password, string serverAddress) {
        return new AtossClient(username, password, serverAddress);
    }

    public static IAtossClient Create(string username, string password, string serverAddress, ILogger logger) {
        return new AtossClient(username, password, serverAddress, logger);
    }
}
