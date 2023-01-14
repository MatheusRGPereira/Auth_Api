namespace AuthenticationApi.Dtos
{
    public class AdministradorJwtDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!;
        public string Permissao { get; set; } = default!;
        public DateTime Expiracao { get; set; } = default!;

    }
}
