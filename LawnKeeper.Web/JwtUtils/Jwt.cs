namespace LawnKeeper.Web.JwtUtils
{
    public class Jwt
    {
        public Jwt(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}