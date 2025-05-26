namespace Dominio.ValueObjects
{
    public class Email
    {
        public string Valor { get; }
        public Email(string valor) 
        {
            if(string.IsNullOrEmpty(valor))
            {
                throw new ArgumentException("El valor no puede ser nulo o vacío.");
            }
            if (valor.Length > 100)
            {
                throw new ArgumentException("El valor no puede ser mayor a 100 caracteres.");
            }
            if (!valor.Contains("@"))
            {
                throw new ArgumentException("El valor no es un correo electrónico válido.");
            }
            Valor = valor;
        }
        public override bool Equals(object obj)
        {
            if (obj is not Email otro) return false;
            return Valor == otro.Valor;
        }
        public override int GetHashCode()=>Valor.GetHashCode();
        public static implicit operator string(Email email) => email.Valor;
        public static explicit operator Email(string valor) => new Email(valor);
    }
}
