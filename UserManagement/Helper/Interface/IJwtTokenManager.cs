namespace Demo.Helper.Interface
{
    public interface IJwtTokenManager
    {
        public string Authenticate(string username, string password);
    }
}
