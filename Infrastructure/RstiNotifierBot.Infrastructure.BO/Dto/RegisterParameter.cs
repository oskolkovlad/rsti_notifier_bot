namespace RstiNotifierBot.Infrastructure.BO.Dto
{
    public class RegisterParameter
    {
        public RegisterParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public object Value { get; }
    }
}
