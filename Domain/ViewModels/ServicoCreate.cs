namespace Domain.ViewModels
{
    public class ServicoCreate
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public int AlunoID { get; set; }
        public int MotoristaID { get; set; }
        public int ResponsavelID { get; set; }
    }
}