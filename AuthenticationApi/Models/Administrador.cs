namespace AuthenticationApi.Models
{
    public class Administrador
    {
        public int Id { get; set; } = default!;
        public string Nome { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Senha { get; set; } = default!;

        public EnumPermissao Permissao { get; set; }

    }
}
